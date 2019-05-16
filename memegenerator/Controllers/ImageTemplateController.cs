using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MemeGenerator.Controllers
{
    public class ImageTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}