using System;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ZXing;
using ZXing.Common;
using ZXing.CoreCompat.System.Drawing;

namespace Itminus.Barcode.TagHelpers
{


    [HtmlTargetElement("BitmapBarcode")]
    public class BitmapBarcodeTagHelper : TagHelper
    {
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public int Margin { get; set; }
        public string Alt { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// indicates the barcode foramt , i.e., qrcode , e.t.c.
        /// </summary>
        public Itminus.Barcode.TagHelpers.BarcodeFormat BarcodeFormat { get; set; }

        /// <summary>
        /// indicates the image format , i.e.  png , jpg, etc 
        /// </summary>
        public virtual ImageFormat ImageFormat { get; set; }

        public bool PureBarcode { get; set; } = false;

        internal ZXing.BarcodeFormat ConvertBarcodeFormat(BarcodeFormat barcodeFormat)
        {
            return (ZXing.BarcodeFormat) barcodeFormat;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (String.IsNullOrEmpty(this.Content))
                return;
            BarcodeWriter writer = new BarcodeWriter()
            {
                Format = this.ConvertBarcodeFormat(this.BarcodeFormat),
                Options = new EncodingOptions
                {
                    Height = this.Height,
                    Width = this.Width,
                    PureBarcode = this.PureBarcode,
                    Margin = this.Margin,
                },
            };
            var bitmap = writer.Write(this.Content);
            string base64 = null;
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, this.ImageFormat);
                base64 = Convert.ToBase64String(stream.ToArray());
            }
            var src = $"data:image/{this.ImageFormat.ToString().ToLower()};base64,{base64}";
            output.TagName = "img";
            output.Attributes.Clear();
            output.Attributes.Add("width", this.Width);
            output.Attributes.Add("height", this.Height);
            output.Attributes.Add("alt", this.Alt);
            output.Attributes.Add("src", src);
        }
    }
}
