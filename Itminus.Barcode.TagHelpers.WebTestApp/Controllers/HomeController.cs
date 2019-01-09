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
        public IActionResult QR_Code([FromBody]BarcodeOptions barcodeOptions)
        {
            return View(barcodeOptions);
        }

        public IActionResult Code_128([FromBody]BarcodeOptions barcodeOptions)
        {
            return View(barcodeOptions);
        }

    }
}
