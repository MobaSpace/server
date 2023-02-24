using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Infirmier + "," +Roles.Administrateur)]

    public class BalancesController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly ILogger<BalancesController> _logger;

        public BalancesController(MobaDataLayer dataLayer, ILogger<BalancesController> logger)
        {
            _datalayer = dataLayer;
            this._logger = logger;
        }


        // GET: BalancesController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Balance> balances = await _datalayer.GetBalancesAsync();
            return View(balances);
        }

        // GET: Balances/Create
        public IActionResult Create()
        {
            var vm = new Balance();
            return View(vm);

        }

        // POST: Balances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Balance vm)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                vm.DernierePesee = DateTime.Now;
                await this._datalayer.BeginTransactionAsync();
                await this._datalayer.CreateBalancesAsync(vm);
                this._datalayer.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Fail to create Balance");
                return View(vm);
            }

        }

    }
}
