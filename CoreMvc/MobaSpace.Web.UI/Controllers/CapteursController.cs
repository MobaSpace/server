using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;

namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Soignant + "," + Roles.Infirmier + "," + Roles.Administrateur )]
    public class CapteursController : Controller
    {
        private readonly MobaDataLayer _datalayer;

        public CapteursController(MobaDataLayer dataLayer)
        {
            _datalayer = dataLayer;
        }

        // GET: Capteur
        public async Task<IActionResult> Index()
        {
            return View((await _datalayer.GetCapteursAsync()).OrderByDescending(c => c.Creation));
        }

        // GET: Capteur/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capteur = await this._datalayer.GetCapteurAsync(id.Value);
            if (capteur == null)
            {
                return NotFound();
            }

            return View(capteur);
        }

        // GET: Capteur/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Capteur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCapteur,IdTypeCapteur,Description,EnMarche,NumSerie,Informations")] Capteur capteur)
        {
            if (ModelState.IsValid)
            {
                capteur.Creation = DateTime.UtcNow;
                capteur.Modification = DateTime.UtcNow;
                capteur.EtatOK = true;
                await _datalayer.CreateCapteurAsync(capteur);
                return RedirectToAction(nameof(Index));
            }
            return View(capteur);
        }

        
        // GET: Capteur/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capteur = await _datalayer.GetCapteurAsync(id.Value);
            if (capteur == null)
            {
                return NotFound();
            }

            return View(capteur);
        }

        // POST: Capteur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var capteur = await _datalayer.GetCapteurAsync(id);
            await _datalayer.DeleteCapteurAsync(capteur);
            return RedirectToAction(nameof(Index));
        }

        private bool CapteurExists(long id)
        {
            return _datalayer.CapteurExists(id);
        }

    }
}
