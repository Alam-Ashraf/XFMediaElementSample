using System;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Webkit;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Java.IO;
using Xamarin.Forms;
using XFVideoDemo.DependencyServices;
using XFVideoDemo.Droid.DependencyServices;

[assembly: Dependency(typeof(ThumbnailService))]
namespace XFVideoDemo.Droid.DependencyServices
{
    public class ThumbnailService : IGetThumbnailFromVideo
    {
        public ImageSource GetImageStreamAsync(string url, long usecond)
        {
            return null;
        }


        public bool SaveImage(string fileName, String contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;

            if (ContextCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)MainActivity.Instance, new String[] { Manifest.Permission.WriteExternalStorage }, 1);

                return false;
            }

            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/XamarinScreenshots");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();

                return true;
            }
            catch (Exception e)
            {
                exception = e.ToString();

                return false;
            }

            // Opening File/Image
            if (file.Exists())
            {
                string extension = MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                Android.Net.Uri path = FileProvider.GetUriForFile(MainActivity.Instance, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType(path, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);

                MainActivity.Instance.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            }

        }

        
    }
}
