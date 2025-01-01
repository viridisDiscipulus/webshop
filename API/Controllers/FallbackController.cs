using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)] // Exclude from Swagger
    public class FallbackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var indexPath = Path.Combine(Directory.GetCurrentDirectory(), "webshop/mala-sapa-webshop-frontend/dist/mala-sapa-webshop-frontend", "index.html");
            return PhysicalFile(indexPath, "text/html");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }



}