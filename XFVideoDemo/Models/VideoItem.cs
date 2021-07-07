using System;
using XFVideoDemo.ViewModels;

namespace XFVideoDemo.Models
{
    public class VideoItem : BaseViewModel
    {
        public string Title { get; set; }
        public string VideoURL { get; set; } = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4";

        //public string VideoURL { get; set; } =    "https://multiplatform-" +"f.akamaihd.net/i/multi/will/bunny/big_buck_bunny_,640x360_400,640x36" +"0_700,640x360_1000,950x540_1500,.f4v.csmil/master.m3u8";


        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;

                OnPropertyChanged();
            }
        }

        private bool _isStopMedia;

        public bool IsStopMedia
        {
            get => _isStopMedia;

            set
            {
                _isStopMedia = value;

                OnPropertyChanged();
            }
        }

        public VideoItem(string title)
        {
            Title = title;
        }
    }
}
