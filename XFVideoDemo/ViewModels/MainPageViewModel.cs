using System;
using System.Windows.Input;
using Xamarin.Forms;
using XFVideoDemo.Views;

namespace XFVideoDemo.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel()
        {
            OnVideoListCommand = new Command(OnVideoListClick);
            OnScreenshotsCommand = new Command(OnScreenshotsListClick);
        }


        #region Handle Clicke Events

        private void OnVideoListClick()
        {
            App.Current.MainPage.Navigation.PushAsync(new VideoListView());
        }

        private void OnScreenshotsListClick()
        {
            App.Current.MainPage.Navigation.PushAsync(new ScreenShotsView());
        }

        #endregion


        #region Properties

        public ICommand OnVideoListCommand { get; set; }
        public ICommand OnScreenshotsCommand { get; set; }

        #endregion
    }
}
