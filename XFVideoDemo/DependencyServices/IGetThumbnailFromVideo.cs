using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFVideoDemo.DependencyServices
{
    public interface IGetThumbnailFromVideo
    {
        ImageSource GetImageStreamAsync(string url, long usecond);

        bool SaveImage(string fileName, String contentType, MemoryStream stream);
    }
}
