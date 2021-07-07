using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using XFVideoDemo.DependencyServices;
using XFVideoDemo.ViewModels;

namespace XFVideoDemo.Views
{
    public partial class VideoListView : ContentPage
    {
        private VideoListViewModel _videoListViewModel;

        public VideoListView()
        {
            InitializeComponent();

            BindingContext = _videoListViewModel = new VideoListViewModel();

            //ImageSource imageSource =  DependencyService.Get<IGetThumbnailFromVideo>().GetImageStreamAsync("https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4",1000);

            //img.Source = imageSource;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var listView = ls.ItemTemplate;
        }

        
    }
}
