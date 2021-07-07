using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XFVideoDemo.ViewModels;

namespace XFVideoDemo.Views
{
    public partial class ScreenShotsView : ContentPage
    {
        private ScreenShotsViewModel _screenShotsViewModel;

        public ScreenShotsView()
        {
            InitializeComponent();

            BindingContext = _screenShotsViewModel = new ScreenShotsViewModel();
        }
    }
}
