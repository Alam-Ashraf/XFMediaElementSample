using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFVideoDemo.Views;

namespace XFVideoDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
