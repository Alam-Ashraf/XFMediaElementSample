using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFVideoDemo.Models;
using XFVideoDemo.Utiility;

namespace XFVideoDemo.ViewModels
{
    public class ScreenShotsViewModel : BaseViewModel
    {

        #region Constructor

        public ScreenShotsViewModel()
        {
            OnBackCommand = new Command(async () => await OnBackClick());

            _ = GetScreenShots();
        }

        #endregion


        #region Handle Click Events

        private async Task OnBackClick()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        #endregion


        #region Properties

        public ICommand OnBackCommand { get; set; }

        private ObservableCollection<ScreenShotItem> _screenShots = new ObservableCollection<ScreenShotItem>();

        public ObservableCollection<ScreenShotItem> ScreenShots
        {
            get => _screenShots;

            set
            {
                _screenShots = value;

                OnPropertyChanged();
            }
        }



        #endregion


        #region Get Screen Shots

        private async Task GetScreenShots()
        {
            ScreenShots = await FileHelper.LoadAllImage();
        }

        #endregion
    }
}
