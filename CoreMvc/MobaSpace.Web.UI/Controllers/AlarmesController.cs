using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;

namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Soignant + "," + Roles.Infirmier + "," + Roles.Administrateur)]
    public class AlarmesController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly UserManager<User> _manager;

        public AlarmesController(MobaDataLayer datalayer, UserManager<User> manager)
        {
            _datalayer = datalayer;
            _manager = manager;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Alarme> alarmes = await this._datalayer.GetAlarmesAcquitterAsync();
            this.ViewBag.nbColumn = 4;
            return View(alarmes);
        }

        //Aquit alarms
        public async Task<IActionResult> Acquitter(long? idAlarme, bool? confirmation)
        {
            if(!idAlarme.HasValue)
            {
                NotFound();
            }
            try
            {
                var utilisateur = await this._manager.GetUserAsync(HttpContext.User);
                var alarme = await this._datalayer.GetAlarmeByIdAsync(idAlarme.Value);
                if(utilisateur == null)
                {
                    throw new Exception("User must be logger in to validate an alarm");
                }
                if (alarme.Acquittement != null || alarme.StringPriorite() != "HAUTE" && confirmation != null)
                {
                    throw new Exception("alarm is not valid");
                }
                await _datalayer.AcquitterAlarme(idAlarme.Value, confirmation, utilisateur);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        //Aquit alarms
        public async Task<IActionResult> AcquitterAll()
        {
             IEnumerable<Alarme> alarmes = await this._datalayer.GetAlarmesAcquitterTestAsync();
            if (alarmes.Count() == 0 || alarmes != null)
            {
                NotFound();
            }
            try
            {

                var utilisateur = await this._manager.GetUserAsync(HttpContext.User);
                if (utilisateur == null)
                {
                    throw new Exception("User must be logger in to validate an alarm");
                }
                /*if (alarme.Acquittement != null || alarme.StringPriorite() != "HAUTE" && confirmation != null)
                {
                    throw new Exception("alarm is not valid");
                }*/
                await _datalayer.AcquitterAlarmeAll(alarmes, utilisateur);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        //Return numbre of unacknowledged alarms
        [HttpGet]
        public async Task<int> CountAlarmes(){
            await this._datalayer.BeginTransactionAsync();
            IEnumerable<Alarme> alarmes = await this._datalayer.GetAlarmesAcquitterAsync();
            this._datalayer.CommitTransaction();
            return alarmes.Count(a => !a.Acquittement.HasValue);

        }

        //Generate an alarms
        public async Task<IActionResult> CreerAleatoirement()
        {
            await this._datalayer.CreerAleatoirement();
            return RedirectToAction(nameof(Index));
        }

        //Returning the biggest ID
        public async Task<long> GetMaxAlarmeId()
        {
            await this._datalayer.BeginTransactionAsync();
            IEnumerable<Alarme> alarmes = await this._datalayer.GetAlarmesAcquitterAsync();
            this._datalayer.CommitTransaction();
            alarmes = alarmes.Where(a => !a.Acquittement.HasValue).OrderByDescending(a => a.Priorite).ThenByDescending(a => a.Creation);
            if (alarmes.Count() > 0)
            {

                return alarmes.Max(a => a.Id);
            }
            else
            {
                return 0;
            }
        }

        public async Task<IActionResult> _AlarmesPartial(int Size)
        {
            await this._datalayer.BeginTransactionAsync();
            IEnumerable<Alarme> alarmes = await this._datalayer.GetAlarmesAcquitterAsync();
            this._datalayer.CommitTransaction();
            if (Size < 670)
            {
                this.ViewBag.nbColumn = 1;
            }
            else if (Size < 1150 && Size > 670 )
            {
                this.ViewBag.nbColumn = 2;
            }
            else if (Size < 1600 && Size > 1150)
            {
                this.ViewBag.nbColumn = 3;
            }
            else
            {
                this.ViewBag.nbColumn = 4;
            }
            return PartialView(alarmes.Where(a => !a.Acquittement.HasValue).OrderByDescending(a => a.Priorite).ThenByDescending(a => a.Creation));
        }
    }
}