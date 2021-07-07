using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace XFVideoDemo.Views
{
    public partial class CustomMediaElement : ContentView
    {
        private static MediaElement media;
        public static readonly BindableProperty IsStopMediaProperty =
                              BindableProperty.Create(
                                  "IsStopMedia",
                                  typeof(bool),
                                  typeof(CustomMediaElement),
                                  false,
                                  BindingMode.TwoWay,
                                  propertyChanged: OnEventNameChanged);

        public bool IsStopMedia
        {
            get { return (bool)GetValue(IsStopMediaProperty); }
            set { SetValue(IsStopMediaProperty, value); }
        }

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bool)newValue)
                media.Play();
            else
                media.Stop();
        }

        public static readonly BindableProperty MediaUrlProperty =
                              BindableProperty.Create(
                                  "MediaUrl",
                                  typeof(string),
                                  typeof(CustomMediaElement),
                                  string.Empty,
                                  BindingMode.TwoWay,
                                  propertyChanged: OnMediaUrlEventNameChanged);

        public bool MediaUrl
        {
            get { return (bool)GetValue(MediaUrlProperty); }
            set { SetValue(MediaUrlProperty, value); }
        }

        static void OnMediaUrlEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            string url = ((string)newValue);

            if (!string.IsNullOrEmpty(url))
            {
                media.Source = MediaSource.FromUri(url);
            }
        }

        public CustomMediaElement()
        {
            InitializeComponent();

            CustomMediaElement.media = mediaElement;
        }
    }
}
