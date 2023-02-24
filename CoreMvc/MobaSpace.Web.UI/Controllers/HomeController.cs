using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Web.UI.Models;

namespace MobaSpace.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MobaDataLayer _datalayer;

        public HomeController(ILogger<HomeController> logger, MobaDataLayer datalayer)
        {
            _logger = logger;
            _datalayer = datalayer;
        }

        public IActionResult Index()
        {
            var users = _datalayer.GetUsersAsync();
            return View(users);
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
