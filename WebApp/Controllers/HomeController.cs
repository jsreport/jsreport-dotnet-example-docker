using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jsreport.AspNetCore;
using jsreport.Types;
using WebApp.Model;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult Invoice()
        {
            HttpContext.JsReportFeature()
                // the normal jsreport base url injection into the html doesn't work properly with docker and asp.net because of port mapping
                // the project typically starts with some http://localhost:1234 url but inside docker the url is http://localhost
                .Configure((req) => req.Options.Base = "http://localhost")
                .Recipe(Recipe.ChromePdf);         

            return View(InvoiceModel.Example());
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult Items()
        {
            HttpContext.JsReportFeature()
                .Recipe(Recipe.HtmlToXlsx);               

            return View(InvoiceModel.Example());
        }
    }
}
