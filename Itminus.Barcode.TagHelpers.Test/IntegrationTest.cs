using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Itminus.Barcode.TagHelpers.WebTestApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using WebTestApp;
using Xunit;

namespace Itminus.Barcode.TagHelpers.Test
{
    public class IntegrationTest
    {
        private TestServer _testServer;
        private HttpClient _client;

        public IntegrationTest() {
            var hb = new WebHostBuilder()
                .UseStartup<Startup>()
                ;
            this._testServer = new TestServer(hb);
            this._client = _testServer.CreateClient();
        }

        [Theory]
        [InlineData("1234567890",null,100,200,10)]
        [InlineData("Abcd1208sdf","",100,200,10)]
        [InlineData("++84-*!","",100,200,10)]
        [InlineData("++84-*!","UTF-8",100,200,10)]
        [InlineData("https://github.com","UTF-8",100,200,10)]
        public async Task TestQR128(string content,string charset, int width, int height, int margin)
        {
            var opts = new BarcodeOptions() {
                Width= width,
                Height = height,
                Margin = margin,
                Content = content,
                Charset = charset,
                Alt ="test",
            };
            var resp= await this._client.PostAsJsonAsync("/home/code_128",opts);
            //var resp = this._client.GetAsync("/home/index?").Result;
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
            var respContent= await resp.Content.ReadAsStringAsync();
            var parser= new HtmlParser();
            var doc =parser.ParseDocument(respContent);
            var img =doc.QuerySelector("img");
            Assert.Equal(Convert.ToInt32(img.GetAttribute("width")),opts.Width);
            Assert.Equal(Convert.ToInt32(img.GetAttribute("height")),opts.Height);
        }


        [Theory]
        [InlineData("https://www.itminus.com",null,100,200,10)]
        [InlineData("https://www.itminus.com","",100,200,10)]
        [InlineData("学习雷锋好榜样","UTF-8",100,200,10)]
        [InlineData("https://github.com","UTF-8",100,200,10)]
        public async Task TestQRCode(string content,string charset, int width, int height, int margin)
        {
            var opts = new BarcodeOptions() {
                Width= width,
                Height = height,
                Margin = margin,
                Content = content,
                Charset = charset,
                Alt ="test",
            };
            var resp= await this._client.PostAsJsonAsync("/home/qr_code",opts);
            //var resp = this._client.GetAsync("/home/index?").Result;
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
            var respContent=await resp.Content.ReadAsStringAsync();
            var parser= new HtmlParser();
            var doc =parser.ParseDocument(respContent);
            var img =doc.QuerySelector("img");
            Assert.Equal(Convert.ToInt32(img.GetAttribute("width")),opts.Width);
            Assert.Equal(Convert.ToInt32(img.GetAttribute("height")),opts.Height);
        }

    }
}
