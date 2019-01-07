using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Itminus.Barcode.TagHelpers.WebTestApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebTestApp.Models;

namespace WebTestApp.Controllers
{
    public class HomeController : Controller
    {
        private BarcodeOptions _barcodeOptions;

        public HomeController(IOptions<BarcodeOptions> barcodeOptions) {
            this._barcodeOptions = barcodeOptions.Value;
        }
        public IActionResult Index()
        {
            return View(this._barcodeOptions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
