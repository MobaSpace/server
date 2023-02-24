using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;


namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Infirmier + "," + Roles.Administrateur )]

    public class QualiteController : Controller
    {
        private readonly MobaDataLayer _datalayer;

        public QualiteController(MobaDataLayer dataLayer)
        {
            _datalayer = dataLayer;
        }


        // GET: QualiteController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Alarme> alarmes = await _datalayer.GetQualitéAlarmesAsync();
            return View(alarmes);
        }



    }
}
