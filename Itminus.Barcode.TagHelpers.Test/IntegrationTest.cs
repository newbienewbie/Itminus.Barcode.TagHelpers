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
        private IConfigurationRoot _config;
        private BarcodeOptions _barcodeOpts;

        public IntegrationTest() {
            var p = Path.GetDirectoryName((typeof(Startup).Assembly.Location));
            this._config= new ConfigurationBuilder()
                .SetBasePath(p)
                .AddJsonFile("appsettings.json")
                .Build();
            this._barcodeOpts= this._config.GetSection("Barcode").Get<BarcodeOptions>();
            var hb = new WebHostBuilder()
                .UseConfiguration(this._config)
                .UseStartup<Startup>()
                ;
            this._testServer = new TestServer(hb);
            this._client = _testServer.CreateClient();
        }


        [Fact]
        public void TestJpeg()
        {
            var resp = this._client.GetAsync("/home/index").Result;
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
            var content=resp.Content.ReadAsStringAsync().Result;
            var parser= new HtmlParser();
            var doc =parser.ParseDocument(content);
            var img =doc.QuerySelector("img");
            Assert.Equal(Convert.ToInt32(img.GetAttribute("width")),this._barcodeOpts.Width);
            Assert.Equal(Convert.ToInt32(img.GetAttribute("height")),this._barcodeOpts.Height);
        }

    }
}
