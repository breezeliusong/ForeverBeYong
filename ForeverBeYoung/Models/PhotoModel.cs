using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ForeverBeYoung.Models
{
    public class PhotoModel
    {
        public BitmapImage ImaSource { get; set; }
        public string ImageInfo { get; set; }
        public string DateTime { get; set; }
        public BitmapImage OriginalImage { get; set; }
    }
}
