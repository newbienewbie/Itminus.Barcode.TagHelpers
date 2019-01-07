using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itminus.Barcode.TagHelpers.WebTestApp
{
    public class BarcodeOptions
    {
        public int Width { get; set; }
        public int Height{ get; set; }
        public int Margin{ get; set; }
        public string Content{ get; set; }
        public string Alt{ get; set; }
    }
}
