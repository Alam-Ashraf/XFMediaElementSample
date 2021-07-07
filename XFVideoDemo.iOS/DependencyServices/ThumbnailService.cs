using System;
using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XFVideoDemo.DependencyServices;
using XFVideoDemo.iOS.DependencyServices;

[assembly: Dependency(typeof(ThumbnailService))]
namespace XFVideoDemo.iOS.DependencyServices
{
    public class ThumbnailService : IGetThumbnailFromVideo
    {
        public ThumbnailService()
        {
        }

        public ImageSource GetImageStreamAsync(string url, long usecond)
        {
            AVAssetImageGenerator imageGenerator = new AVAssetImageGenerator(AVAsset.FromUrl((new Foundation.NSUrl(url))));
            imageGenerator.AppliesPreferredTrackTransform = true;
            CMTime actualTime;
            NSError error;
            CGImage cgImage = imageGenerator.CopyCGImageAtTime(new CMTime(usecond, 1000000), out actualTime, out error);
            return ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream()); ImageSource.FromStream(() => new UIImage(cgImage).AsPNG().AsStream());
        }

        public bool SaveImage(string fileName, string contentType, MemoryStream stream)
        {
            return false;
        }
    }
}
