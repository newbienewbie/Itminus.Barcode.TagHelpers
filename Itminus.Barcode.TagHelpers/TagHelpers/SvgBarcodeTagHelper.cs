using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ZXing;
using ZXing.Common;
using ZXing.CoreCompat.System.Drawing;


namespace Itminus.Barcode.TagHelpers
{

    [HtmlTargetElement("SvgBarcode")]
    public class SvgBarcodeTagHelper : BitmapBarcodeTagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (String.IsNullOrEmpty(this.Content))
                return;
            var encodingOpts = this.CreateOptions();
            var qrWriter = new ZXing.BarcodeWriterSvg
            {
                Format = this.ConvertBarcodeFormat(this.BarcodeFormat),
                Options = encodingOpts,             
            };
            var svgImage = qrWriter.Write(this.Content);
            var base64 = Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(svgImage.Content));
            var src = $"data:image/svg+xml;base64,{base64}";
            output.TagName = "img";
            output.Attributes.Clear();
            output.Attributes.Add("width", this.Width);
            output.Attributes.Add("height", this.Height);
            output.Attributes.Add("alt", this.Alt);
            output.Attributes.Add("src", src);
        }


    }
}
