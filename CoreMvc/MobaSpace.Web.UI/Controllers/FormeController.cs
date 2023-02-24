using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = Roles.Infirmier + "," + Roles.Administrateur)]
    public class FormeController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly ILogger<FormeController> _logger;
        private readonly UserManager<User> _manager;

        public FormeController(MobaDataLayer dataLayer, ILogger<FormeController> logger , UserManager<User> manager)
        {
            _datalayer = dataLayer;
            this._logger = logger;
            _manager = manager;
        }


        // GET: ScoreFormeController
        public async Task<ActionResult> Index()
        {
            IEnumerable<ScoreForme> scoreFormes = await _datalayer.GetScoreFormeAsync();
            return View(scoreFormes);
        }

        // GET: Forme/Delete
        public async Task<ActionResult> Delete()
        {
            await _datalayer.BeginTransactionAsync();
            var listNumChScore = await this._datalayer.GetScoresCH();
            var listNumCh = new List<int>();
            foreach(IGrouping<int, ScoreForme> num in listNumChScore)
            {
                listNumCh.Add(num.Key);
            }
            _datalayer.CommitTransaction();
            if(listNumCh is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(listNumCh);
        }
        
        //Reset for a room number
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(IFormCollection formfield)
        {
            try{
                await _datalayer.BeginTransactionAsync();
                int numCh = Convert.ToInt32(formfield["Numch"]);
                var Scores = await this._datalayer.GetScoresByCh(numCh);
                await this._datalayer.DeleteScores(Scores);
                _datalayer.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }catch (Exception ex)
            {
                return RedirectToAction(nameof(Delete));
            }

        }

        //Returning data for graph
        public async Task<List<List<string>>> DataGraph(long NumCh)
        {
            //Initialisation des données 
            await _datalayer.BeginTransactionAsync();
            IEnumerable<ScoreForme> Scores = await this._datalayer.GetScoreFormeByIdAsync(NumCh);
            Scores = Scores.OrderBy(SF => SF.Date).ToList(); 
            _datalayer.CommitTransaction();
            

            List<List<string>> listData = new List<List<string>>();
            List<string> SVList = new List<string>();
            List<string> SPList = new List<string>();
            List<string> ICList = new List<string>();
            List<string> TRList = new List<string>();
            List<string> DateList = new List<string>();
            //for each type of data building a table
            for (int i = 0; i < Scores.Where(S => S.Date <= DateTime.Today.Date.AddDays(-1)).Count() ; i++)
            {
                if (Scores.ElementAt(i).TauxDeRemp != null && Scores.ElementAt(i).IndiceDeConfiance != null && Scores.ElementAt(i).ScoreJour != null)
                {
                    SVList.Add(((double)Scores.ElementAt(i).ScoreJour * 100).ToString());
                    if(i != 0 ) {
                        if (Scores.ElementAt(i - 1).ScorePred != null)
                        {
                            SPList.Add(((double)Scores.ElementAt(i - 1).ScorePred * 100).ToString());
                        }
                        else
                        {
                            SPList.Add(null);
                        }
                    }
                    ICList.Add(((double)Scores.ElementAt(i).IndiceDeConfiance* 100).ToString());
                    TRList.Add(((double)Scores.ElementAt(i).TauxDeRemp * 100).ToString());
                    DateList.Add(Scores.ElementAt(i).Date.Day.ToString() + "/" + Scores.ElementAt(i).Date.Month.ToString());
                }
            }
            listData.Add(SVList);
            listData.Add(SPList);
            listData.Add(ICList);
            listData.Add(TRList);
            listData.Add(DateList);
            return listData;
        }
    }
}
