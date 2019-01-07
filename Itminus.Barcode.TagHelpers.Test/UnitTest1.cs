using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Itminus.Barcode.TagHelpers.Test
{
    public class UnitTest1
    {


        [Theory]
        [InlineData(400,500,5,"https://www.itminus.com","hello,itminus")]
        [InlineData(200,300,5,"https://stackoverflow.com","hello,SO")]
        public void TestJpeg(int width, int height,int margin, string content, string alt)
        {
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N")
            );
            var tagHelperOutput = new TagHelperOutput(
                "bitmapTagHelepr",
                new TagHelperAttributeList(), 
                (result, encoder) => {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
            );
            var imageFormat = ImageFormat.Jpeg;
            var bitmapTagHelper = new BitmapBarcodeTagHelper {
                Content = content,
                Alt = alt,
                BarcodeFormat = BarcodeFormat.QR_CODE,
                ImageFormat = imageFormat, 
                Width = width,
                Height = height,
                Margin = margin,
                PureBarcode = false,
            };
            bitmapTagHelper.Process(tagHelperContext, tagHelperOutput);
            var tagAttrs = tagHelperOutput.Attributes;
            var tagcontent= tagHelperOutput.Content.GetContent();
            Assert.Equal(Convert.ToInt32(tagAttrs["width"].Value),width);
            Assert.Equal(Convert.ToInt32(tagAttrs["height"].Value),height);
            Assert.Equal(tagAttrs["alt"].Value.ToString(),alt);
            Assert.Equal("",tagcontent);

            var imageFormatName = imageFormat.ToString().ToLower();
            var tagSrc = tagAttrs["src"].Value.ToString().ToLower();
            Assert.Contains(imageFormatName, tagSrc);
            Assert.StartsWith($"data:image/{imageFormatName};base64,",tagSrc);
        }

        [Theory]
        [InlineData(400, 500, 5, "https://www.itminus.com", "hello,itminus")]
        [InlineData(200, 300, 5, "https://stackoverflow.com", "hello,SO")]
        public void TestSvg(int width, int height, int margin, string content, string alt)
        {
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N")
            );
            var tagHelperOutput = new TagHelperOutput(
                "SvgBarcodeTagHelper",
                new TagHelperAttributeList(),
                (result, encoder) => {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
            );
            var bitmapTagHelper = new SvgBarcodeTagHelper
            {
                Content = content,
                Alt = alt,
                BarcodeFormat = BarcodeFormat.QR_CODE,
                Width = width,
                Height = height,
                Margin = margin,
                PureBarcode = false,
            };
            bitmapTagHelper.Process(tagHelperContext, tagHelperOutput);
            var tagAttrs = tagHelperOutput.Attributes;
            var tagcontent = tagHelperOutput.Content.GetContent();
            Assert.Equal(Convert.ToInt32(tagAttrs["width"].Value), width);
            Assert.Equal(Convert.ToInt32(tagAttrs["height"].Value), height);
            Assert.Equal(tagAttrs["alt"].Value.ToString(), alt);
            Assert.Equal("", tagcontent);

            var tagSrc = tagAttrs["src"].Value.ToString().ToLower();
            Assert.Contains("svg", tagSrc);
            Assert.StartsWith($"data:image/svg+xml;base64,", tagSrc);
        }



    }
}
