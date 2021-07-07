using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using XFVideoDemo.Models;
using FileAccess = PCLStorage.FileAccess;

namespace XFVideoDemo.Utiility
{
    public static class FileHelper
    {
        public const string FOLDER_NAME = "XamarinScreenshots";

        public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<bool> IsFolderExistAsync(this string folderName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FolderExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<IFolder> CreateFolder(this string folderName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        public async static Task<IFile> CreateFile(this string filename, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return file;
        }
        public async static Task<bool> WriteTextAllAsync(this string filename, string content = "", IFolder rootFolder = null)
        {
            IFile file = await filename.CreateFile(rootFolder);
            await file.WriteAllTextAsync(content);
            return true;
        }

        public async static Task<string> ReadAllTextAsync(this string fileName, IFolder rootFolder = null)
        {
            string content = "";
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync(folder);
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                content = await file.ReadAllTextAsync();
            }
            return content;
        }
        public async static Task<bool> DeleteFile(this string fileName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync(folder);
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            return false;
        }
        public async static Task<bool> SaveImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            try
            {
                if (!await IsFolderExistAsync(FOLDER_NAME))
                {
                    await CreateFolder(FOLDER_NAME);
                }

                // get hold of the file system  
                IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(FOLDER_NAME);

                // create a file, overwriting any existing file  
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                // populate the file with image data  
                using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
                {
                    stream.Write(image, 0, image.Length);
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public async static Task<byte[]> LoadImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            //open file if exists  
            IFile file = await folder.GetFileAsync(fileName);

            //load stream to buffer  
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }
        }

        public async static Task<ObservableCollection<ScreenShotItem>> LoadAllImage(IFolder rootFolder = null)
        {
            if (!await IsFolderExistAsync(FOLDER_NAME))
            {
                await CreateFolder(FOLDER_NAME);
            }

            // get hold of the file system  
            IFolder folder = await FileSystem.Current.LocalStorage.GetFolderAsync(FOLDER_NAME);

            //open file if exists  
            var files = await folder.GetFilesAsync();

            ObservableCollection<ScreenShotItem> screenShots = new ObservableCollection<ScreenShotItem>();

            foreach (IFile file in files)
            {
                //load stream to buffer
                using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
                {
                    long length = stream.Length;
                    byte[] streamBuffer = new byte[length];
                    stream.Read(streamBuffer, 0, (int)length);

                    screenShots.Add(new ScreenShotItem() { Title = file.Name, Screenshots = ImageSource.FromStream(() => new MemoryStream(streamBuffer)) });

                    //screenShots.Add(new ScreenShotItem() { Title = file.Name, Screenshots = ImageSource.FromFile(file.Path) });

                }

            }

            return screenShots;
        }


        public static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}