using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;
using XFVideoDemo.DependencyServices;
using XFVideoDemo.Models;
using XFVideoDemo.Utiility;

namespace XFVideoDemo.ViewModels
{
    public class VideoListViewModel : BaseViewModel
    {

        #region Constructor

        public VideoListViewModel()
        {
            GetVideoList();

            OnVideoSelectedCommand = new Command<VideoItem>(t=> OnVideoSelected(t));
            OnCaptureScreenCommand = new Command(async() => await OnCaptureImageClick());
            OnBackCommand = new Command(async () => await OnBackClick());
        }

        #endregion


        #region Handle Click Event

        private void OnVideoSelected(VideoItem videoItem)
        {
            var selectedVideoList = VideoList.Where<VideoItem>(t => t.IsSelected==true).ToList();

            if ((videoItem!=null && selectedVideoList.Count<=3) || (videoItem.IsSelected==true && selectedVideoList.Count == 4))
            {
                videoItem.IsSelected = !videoItem.IsSelected;

                if(SelectedVideoList!=null)
                {
                    var video = SelectedVideoList.Where<VideoItem>(t => t.Title.Equals(videoItem.Title)).FirstOrDefault();

                    if(videoItem.IsSelected && video==null)
                    {
                        videoItem.IsStopMedia = false;
                        SelectedVideoList.Add(videoItem);
                    }
                    else if(videoItem.IsSelected==false && video!=null)
                    {
                        videoItem.IsStopMedia = true;
                        SelectedVideoList.Remove(video);
                    }
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("", "You can't select more than 4 videos", "ok");
            }
        }

        private async Task OnCaptureImageClick()
        {
            var screenshot = await Xamarin.Essentials.Screenshot.CaptureAsync();
            var stream = await screenshot.OpenReadAsync();

            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            bool isSaved = await FileHelper.SaveImage(memoryStream.ToArray(), "screen_" + DateTime.Now.Ticks + ".png");

            if (isSaved)
                await App.Current.MainPage.DisplayAlert("", "Image captured and saved.", "ok");
        }

        private async Task OnBackClick()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        #endregion


        #region Properties

        private ObservableCollection<VideoItem> _selectedVideoList = new ObservableCollection<VideoItem>();

        public ObservableCollection<VideoItem> SelectedVideoList
        {
            get => _selectedVideoList;

            set
            {
                _selectedVideoList = value;

                OnPropertyChanged();
            }
        }


        private ObservableCollection<VideoItem> _videoList = new ObservableCollection<VideoItem>();

        public ObservableCollection<VideoItem> VideoList
        {
            get => _videoList;

            set
            {
                _videoList = value;

                OnPropertyChanged();
            }
        }

        public ICommand OnVideoSelectedCommand { get; set; }
        public ICommand OnCaptureScreenCommand { get; set; }
        public ICommand OnBackCommand { get; set; }


        #endregion


        #region Making Dummy Video List

        private void GetVideoList()
        {
            VideoList = new ObservableCollection<VideoItem>();

            VideoList.Add(new VideoItem("Cam_01"));
            VideoList.Add(new VideoItem("Cam_02"));
            VideoList.Add(new VideoItem("Cam_03"));
            VideoList.Add(new VideoItem("Cam_04"));
            VideoList.Add(new VideoItem("Cam_05"));
            VideoList.Add(new VideoItem("Cam_06"));
            VideoList.Add(new VideoItem("Cam_07"));
            VideoList.Add(new VideoItem("Cam_08"));
        }




        #endregion

    }
}
