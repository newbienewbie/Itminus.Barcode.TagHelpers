using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Itminus.Barcode.TagHelpers.WebTestApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

    }
}
