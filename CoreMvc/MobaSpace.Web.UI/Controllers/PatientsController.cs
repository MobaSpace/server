using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using MobaSpace.Core.File;
using MobaSpace.Core.Utils;
using MobaSpace.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Soignant + "," + Roles.Infirmier + "," + Roles.Administrateur)]
    public class PatientsController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PatientsController> _logger;

        private readonly string _repWwwRootPath;
        private readonly string _repWwwImagesPatients;

        private readonly CultureInfo _enCulture = new CultureInfo("en-EN");

        public PatientsController(MobaDataLayer datalayer, IWebHostEnvironment env, SignInManager<User> signinManager, UserManager<User> userManager, ILogger<PatientsController> logger)
        {

            this._datalayer = datalayer;
            this._signinManager = signinManager;
            this._userManager = userManager;
            this._logger = logger;
            this._repWwwRootPath = env.WebRootPath;
            this._repWwwImagesPatients = Path.Combine(env.WebRootPath, "images", "patients");
            if (!Directory.Exists(_repWwwImagesPatients))
            {
                Directory.CreateDirectory(_repWwwImagesPatients);
            }
        }

        // GET: Patient
        public async Task<ActionResult> Index()
        {
            await _datalayer.BeginTransactionAsync();
            var patients = await this._datalayer.GetPatientsForDisplayAsync();
            _datalayer.CommitTransaction();
            return View(patients.OrderBy(p => p.UnencriptedPatient.NumCh));
        }

        public async Task<IActionResult> _PatientsPartial(int Size)
        {
            await this._datalayer.BeginTransactionAsync();
            var patients = await this._datalayer.GetPatientsForDisplayAsync();
            this._datalayer.CommitTransaction();
            if (Size < 670)
            {
                this.ViewBag.nbColumn = 1;
            }
            else if (Size < 1150 && Size > 670)
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
            return PartialView(patients.OrderBy(p => p.UnencriptedPatient.NumCh));
        }

        // GET: Patients/Details/5
        public async Task<ActionResult> Details(long id)
        {
            var patient = await this._datalayer.GetPatientDetailAsync(id);
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            var utilisateurs = this._datalayer.GetUsersAsync().ToArray();
            var vm = new PatientViewModel( utilisateurs.Where(u => (_userManager.IsInRoleAsync(u, Roles.Soignant).Result)),
                _datalayer.GetApisUpdatePatient());
            return View(vm);
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PatientViewModel vm)
        {
            
            try
            {
                _logger.LogInformation("Create a new resident...");
                for (int i = 0; i < vm.ContactsPatient.Count(); i++)
                {
                    ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.Email");
                    ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.Password");
                    ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.UserSurname");
                    ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.UserFirstname");
                }
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                vm.Patient.UnencriptedPatient.TempsMaxAllongeJour = vm.Patient.UnencriptedPatient.TempsMaxAllongeJour * 3600;
               // vm.Patient.CumulTempsAllonge = 0;
                await this._datalayer.BeginTransactionAsync();
                await this._datalayer.CreatePatientAsync(vm.Patient);
                var ListUser = await _datalayer.GetPatientsCAsync();
                vm.Patient = ListUser.Last();
                vm.Patient.UnencriptedPatient.CheminPhoto = Path.Combine("images", "patients", "default.png");
                vm.Patient.Contacts = new Collection<ContactPatient>();
                vm.Patient.Apis = new Collection<OAuth2Api>();
                if (vm.Image != null)
                {
                    try
                    {
                        var cheminImage = Path.Combine(_repWwwImagesPatients, vm.Image.FileName);
                        vm.Patient.UnencriptedPatient.CheminPhoto = Path.GetRelativePath(_repWwwRootPath, cheminImage);
                        FileTools.CopyTo(vm.Image, cheminImage);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ControllerError", "Erreur lors de l'envoie de la photo.");
                        Debug.WriteLine(ex?.Message);
                        return View(vm);
                    }
                }

                #region Contacts
                //gestion des ajouts de contacts
                foreach (var contact in vm.ContactsPatient.Where(c => c.Selectionne))
                {
                    var utilisateur = await this._datalayer.GetUserAsync(contact.Utilisateur.Id);
                    vm.Patient.Contacts.Add(new ContactPatient() { Patient = new PatientC { Id = vm.Patient.Id }, Utilisateur = utilisateur });
                }
                #endregion
                #region Apis
                //gestion des ajouts d'apis
                foreach (var ap in vm.ApisPatient.Where(c => c.Selectionne))
                {
                
                    var api = await this._datalayer.GetApiAsync(ap.Api.Id);
                    vm.Patient.Apis.Add(api);
                    
                }
                #endregion

                await this._datalayer.UpdatePatientAsync(vm.Patient);
                this._datalayer.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("ControllerError", "Impossible de créer le patient, le nom de la chambre ou le numéro de la chambre est surement déjà utilisé");
                _logger.LogError(ex, "Fail to create patient");
                return View(vm);
            }
        }

        // GET: Patients/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            await _datalayer.BeginTransactionAsync();
            var patient = await this._datalayer.GetPatientCAsync(id);
            _datalayer.CommitTransaction();
            if (patient is null) { return NotFound(); }

            var utilisateurs = this._datalayer.GetUsersAsync().ToArray();
            var vm = new PatientViewModel(patient,  
                utilisateurs.Where(u => (_userManager.IsInRoleAsync(u, Roles.Soignant).Result)),
                _datalayer.GetApisUpdatePatient());



            return View(vm);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PatientViewModel vm)
        {
            _logger.LogInformation("Edit resident : " + vm.Patient.Id);
            for (int i = 0; i < vm.ContactsPatient.Count(); i++) { 
                ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.Email");
                ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.Password");
                ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.UserSurname");
                ModelState.Remove("ContactsPatient[" + i + "].Utilisateur.UserFirstname");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await this._datalayer.BeginTransactionAsync();
                var patientC = await this._datalayer.GetPatientCAsync(vm.Patient.Id);

                patientC.UnencriptedPatient.Chambre = vm.Patient.UnencriptedPatient.Chambre;

                patientC.UnencriptedPatient.Coucher_h = vm.Patient.Coucher_h;
                patientC.UnencriptedPatient.Coucher_min = vm.Patient.Coucher_min;

                patientC.UnencriptedPatient.Lever_h = vm.Patient.Lever_h;
                patientC.UnencriptedPatient.Lever_min = vm.Patient.Lever_min;

                patientC.UnencriptedPatient.DureeMaxHorsLit_min = vm.Patient.DureeMaxHorsLit_min;
                patientC.UnencriptedPatient.TempsMaxAllongeJour = vm.Patient.TempsMaxAllongeJour * 3600;

                patientC.UnencriptedPatient.NumCh = vm.Patient.UnencriptedPatient.NumCh;
                
                #region Image

                if (vm.Image != null)
                {
                    try
                    {
                        var cheminImage = Path.Combine(_repWwwImagesPatients, vm.Image.FileName);
                        patientC.CheminPhoto = Path.GetRelativePath(_repWwwRootPath, cheminImage);
                        FileTools.CopyTo(vm.Image, cheminImage);

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ControllerError", "Erreur lors de l'envoie de la photo.");
                        _logger.LogError(ex, "Fail to update patient");
                        return View(vm);
                    }
                }
                #endregion
                #region Contacts
                //gestion des ajouts de contacts
                foreach (var contact in vm.ContactsPatient.Where(c => c.Selectionne))
                {
                    if(patientC.Contacts.Count(c => c.Utilisateur.Id == contact.Utilisateur.Id) == 0)
                    {
                        var utilisateur = await this._datalayer.GetUserAsync(contact.Utilisateur.Id);
                        patientC.Contacts.Add(new ContactPatient() { Patient = patientC, Utilisateur = utilisateur });
                    }
                }
                //gestion des suppressions de contacts
                var contactsOri = patientC.Contacts.ToArray();
                foreach (var contact in contactsOri)
                {
                    var selection = vm.ContactsPatient.First(cs => cs.Utilisateur.Id == contact.Utilisateur.Id);
                    if (!selection.Selectionne)
                    {
                        patientC.Contacts.Remove(contact);
                    }
                }
                #endregion
                #region Apis
                //gestion des ajouts d'apis
                foreach (var ap in vm.ApisPatient.Where(c => c.Selectionne))
                {
                    if (patientC.Apis.Count(a => a.Id == ap.Api.Id) == 0)//cette api n'existe pas
                    {
                        var api = await this._datalayer.GetApiAsync(ap.Api.Id);
                        patientC.Apis.Add(api);
                    }
                }
                //gestion des suppressions d'apis
                var apiOri = patientC.Apis.ToArray();
                foreach (var api in apiOri)
                {
                    var selection = vm.ApisPatient.First(ap => ap.Api.Id == api.Id);
                    if (!selection.Selectionne)
                    {
                        patientC.Apis.Remove(api);
                    }
                }
                #endregion

                await this._datalayer.UpdatePatientAsync(patientC);
                this._datalayer.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("ControllerError", "Impossible de modifier le patient, le nom de la chambre ou le numéro de la chambre est surement déjà utilisé");
                _logger.LogError(ex.Message, "Fail to update patient");
                this.ViewBag.Apis = (await this._signinManager.GetExternalAuthenticationSchemesAsync()).Where(shm => shm.Name.Contains("Api")).ToList();
                return View(vm);
            }
        }        

        //Get all elements from Patient
        [HttpGet]
        public async Task<IEnumerable<PatientC>> GetPatients(){
            await _datalayer.BeginTransactionAsync();
            var patients = await this._datalayer.GetPatientsForDisplayAsync();
            _datalayer.CommitTransaction();
            return patients;
        }

        //returning max value for LastUpdate
        [HttpGet]
        public String GetLimitLastUpdate()
        {
            TimeSpan TimeDiff = DateTime.Now - new DateTime();
            return TimeDiff.ThereIs();
        }

        //Get all postures from Patient
        [HttpGet]
        public async Task<IEnumerable<PatientC>> GetPatientsPosition()
        {
            await _datalayer.BeginTransactionAsync();
            var patients = await this._datalayer.GetPatientsPostureAsync();
            _datalayer.CommitTransaction();
            return patients;
        }


        // GET: Patients/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _datalayer.BeginTransactionAsync();
            var patient = await this._datalayer.GetPatientForDeleteAsync(id);
            _logger.LogInformation("Delete resident : " + id);
            _datalayer.CommitTransaction();
            if (patient is null)
            {
                return NotFound("idPatient");
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PatientC patient)
        {

            try
            {
                await _datalayer.BeginTransactionAsync();
                patient = await _datalayer.GetPatientCForDeleteAsync(patient.Id);
                await this._datalayer.DeletePatientAsync(patient);
                _datalayer.CommitTransaction();
                _logger.LogInformation("Success Delete resident ");
            }
            catch (Exception ex)
            {
                _datalayer.RollbackTransaction();
                _logger.LogError(ex, "Fail to delete patient, api and devices");
                ModelState.AddModelError("Suppression patient", "Echec de suppression du patient");
                await _datalayer.BeginTransactionAsync();
                var currentPatient = await this._datalayer.GetPatientForDeleteAsync(patient.Id);
                _logger.LogError("Cancel Delete resident : " + patient.Id);
                _datalayer.CommitTransaction();

                return View(currentPatient);
            }
            return RedirectToAction(nameof(Index));
        }

        //Returning the biggest nuit Id
        public async Task<long> GetMaxNuitsId()
        {
            await _datalayer.BeginTransactionAsync();
            IEnumerable<Nuit> nuits = await this._datalayer.GetIdNuits();
            _datalayer.CommitTransaction();
            if (nuits.Count() > 0)
            {
                return nuits.Max(n => n.Id);
            }
            else
            {
                return 0;
            }
            
        }

        public async Task<String> GetJours(long id)
        {
            await _datalayer.BeginTransactionAsync();
            ICollection<JourC> Jours = await _datalayer.GetJoursCByPatientAsync(id);
            Patient patient = await _datalayer.GetPatientAsync(id);
            _datalayer.CommitTransaction();
            PatientC patientObject = new PatientC { UnencriptedPatient = patient, Jours = Jours };

            return Newtonsoft.Json.JsonConvert.SerializeObject(patientObject);
        } 


        //Returning Data for graph 
        public async Task<List<List<String>>> DataGraph(long id)
        {
            //Initialisation des données 
            await _datalayer.BeginTransactionAsync();
            IEnumerable<Nuit> Nuits = await this._datalayer.GetNuitForGraph(id);
            _datalayer.CommitTransaction();
            List<Nuit> NuitList = new List<Nuit>();
            Nuit nuitSelected = new Nuit();

            /*Recupération des données à analyser 
             ---------------------------------------------------------
             | NbReveils | NbSorties | TempsTotalSommeil | Date Nuit |
             ---------------------------------------------------------
             |           |           |                   |           |
             |     0     |     1     |         2         |     3     |
             |           |           |                   |           |
             */
            List<List<string>> listData = new List<List<string>>();
            foreach (Nuit nuit in Nuits.OrderBy(n => n.DateNuit.Date))
            {
                List<String> tempList = new List<string>();
                //Nombre de reveils
                tempList.Add(nuit.NbReveils.ToString());
                //Nombre de sorties
                tempList.Add(nuit.NbSorties.ToString());
                //Temps de Sommeil
                if ( nuit.DureeSommeil != null)
                {
                    tempList.Add(nuit.DureeSommeil.Value.TotalHours.ToString(System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    tempList.Add("0");
                }

                //Date de la nuit
                tempList.Add(nuit.DateNuit.Day.ToString() + "/" + nuit.DateNuit.Month.ToString());
                //Ajout des données
                listData.Add(tempList);
            }

            return listData;
        }


    }
}