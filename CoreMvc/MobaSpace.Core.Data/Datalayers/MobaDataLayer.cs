using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobaSpace.Core.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobaSpace.Core.Data.Datalayers
{

    public class MobaDataLayer
    {
        #region Variables
        /// <summary>
        /// Database context
        /// </summary>
        protected readonly MobaDbContext _context;

        private IConfiguration _configuration;




        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Database context</param>
        public MobaDataLayer(MobaDbContext context , IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }


        #endregion

        #region Transaction management
        public Task BeginTransactionAsync(CancellationToken token = default)
        {
            return this._context.Database.BeginTransactionAsync(token);
        }

        public void CommitTransaction()
        {
            this._context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this._context.Database.RollbackTransaction();
        }

        #endregion

        #region Users
        //Select all users and dependance
        public IQueryable<User> GetUsersAsync()
        {
            return this._context.Users;
        }
        //Select all ID users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await this._context.Users.Select(u => new User
                                                    {
                                                        Id = u.Id
                                                    }).ToListAsync();
        }
        //Select all data for an user 
        public async Task<User> GetUserAsync(string id)
        {
            return await this._context.Users.FindAsync(id);
        }
        //Delete selected user
        public async Task ClearUserIdAlarme(string id, CancellationToken token = default)
        {
            var alarmes = await GetAlarmeByIdUserAsync(id);
            foreach(Alarme alarme in alarmes)
            {
                var newAlarme = alarme;
                newAlarme.Utilisateur = null;
                _context.Alarmes.Remove(alarme);
                await this._context.SaveChangesAsync(token);
                _context.Alarmes.Add(newAlarme);
                await this._context.SaveChangesAsync(token);
            }

        }

        #endregion

        #region Patients
        //Select all data for all Patients
        public async Task<IEnumerable<PatientC>> GetPatientsCAsync(CancellationToken token = default)
        {
            IEnumerable<PatientC> data = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Posture = p.Posture,
                Apis = p.Apis,
                DernierCoucher = p.DernierCoucher,
                DernierLever = p.DernierLever,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                CumulTempsAllonge = p.CumulTempsAllonge,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min,
                NumCh = p.NumCh
            }).ToListAsync(token);
            foreach (PatientC patient in data)
            {
                patient.UnencriptedPatient = await GetPatientAsync(patient.Id);
                patient.Nuits = (ICollection<NuitC>)await GetNuitsByPatientAsync(patient.Id);
                foreach (OAuth2Api api in patient.Apis)
                {
                    api.ApiCapteurs = await GetApisCapteursByIdApiAsync(api.Id);
                }
            }

            return data;

        }
        // Select some data and link with api and observable for an patient
        public async Task<PatientC> GetPatientCByIdAsync(long PatientId, CancellationToken token = default)
        {
            PatientC patient = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Posture = p.Posture,
                Apis = p.Apis,
                DernierCoucher = p.DernierCoucher,
                DernierLever = p.DernierLever,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                CumulTempsAllonge = p.CumulTempsAllonge,
                Nuits = p.Nuits,
                NumCh = p.NumCh
            }).Where(p => p.Id == PatientId).FirstOrDefaultAsync();
                foreach (OAuth2Api api in patient.Apis)
                {
                    api.ApiCapteurs = await GetApisCapteursByIdApiAsync(api.Id);
                }

            return patient;

        }
        // Select Some data and link with api and observable for all patients
        public async Task<IEnumerable<PatientC>> GetPatientsAsync(CancellationToken token = default)
        {
            IEnumerable<PatientC> data = await _context.PatientCs.Select(p => new PatientC
                                                            {
                                                                Id = p.Id,
                                                                CheminPhoto = p.CheminPhoto,
                                                                Apis = p.Apis,
                                                                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                                                                DerniereLocalisation = p.DerniereLocalisation,
                                                                Nuits = p.Nuits,
                                                                Jours = p.Jours,
                                                            }).ToListAsync(token);
            foreach (PatientC patient in data)
            {
                patient.UnencriptedPatient = await GetPatientAsync(patient.Id);
                patient.Nuits = (ICollection<NuitC>)await GetNuitsByPatientAsync(patient.Id);
                patient.Nuits = patient.Nuits.OrderByDescending(n => n.Id).ToList();
                foreach (OAuth2Api api in patient.Apis)
                {
                    api.ApiCapteurs = await GetApisCapteursByIdApiAsync(api.Id);
                }

            }
            return data;

        }
        // Select Some data and link with api and observable for all patients
        public async Task<IEnumerable<PatientC>> GetPatientsForDisplayAsync(CancellationToken token = default)
        {
            IEnumerable<PatientC> data = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                CheminPhoto = p.CheminPhoto,
                Apis = p.Apis,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                DerniereLocalisation = p.DerniereLocalisation,
                Nuits = p.Nuits,
                Jours = p.Jours,
            }).ToListAsync(token);
            foreach (PatientC patient in data)
            {
                patient.UnencriptedPatient = await GetPatientAsync(patient.Id);
                patient.Nuits = (ICollection<NuitC>)await GetLast7NuitsByPatientAsync(patient.Id);
                patient.Nuits = patient.Nuits.OrderByDescending(n => n.Id).ToList();
                foreach (OAuth2Api api in patient.Apis)
                {
                    api.ApiCapteurs = await GetApisCapteursByIdApiAsync(api.Id);
                }

            }
            return data;

        }

        //Get posture of all patient
        public async Task<IEnumerable<PatientC>> GetPatientsPostureAsync(CancellationToken token = default)
        {
            IEnumerable<PatientC> refreshData = await _context.PatientCs.Select(p => new PatientC
                                    {
                                        Id = p.Id,
                                        Posture = p.Posture,

                                    }).ToListAsync(token);
            foreach(PatientC patient in refreshData)
            {
                patient.UnencriptedPatient = await GetPatientAsync(patient.Id);
            }

            return refreshData;

        }

        //Get Posture, Apis and NumCh for all PAtients
        public async Task<IEnumerable<PatientC>> GetPatientsPositionAsync(CancellationToken token = default)
        {
            return await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Posture = p.Posture,
                Apis = p.Apis,
                NumCh = p.NumCh
            }).ToListAsync(token);


        }

        //Create a new patient
        public async Task CreatePatientAsync(PatientC patient, CancellationToken token = default)
        {
            var test = _context.ChangeTracker.Entries();
            var ListPatient = await GetPatientsCAsync();
            this._context.Database.ExecuteSqlRaw("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Patients\"', 'Id')," +
            " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Patients\"));");
            if (ListPatient.Count() != 0)
            {
                patient.UnencriptedPatient.Id = ListPatient.Last().Id + 1;
            }
            else
            {
                patient.UnencriptedPatient.Id = 1;
            }
            this._context.Patients.Add(patient.UnencriptedPatient);
            await this._context.SaveChangesAsync(token);

        }

        //Get all data from patient
        public async Task<PatientC> GetPatientDetailAsync(long Id)
        {
            PatientC data = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Apis = p.Apis,
                DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min,
                Contacts = p.Contacts,
                CumulTempsAllonge = p.CumulTempsAllonge,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                NumCh = p.NumCh

            }).Where(p => p.Id == Id).FirstOrDefaultAsync();


            data.UnencriptedPatient = await GetPatientAsync(data.Id);
            foreach (ContactPatient contact in data.Contacts)
            {
                ContactPatient temp = await GetContactPatientAsync(contact.Id);
                contact.Utilisateur = temp.Utilisateur;
            }


            foreach (OAuth2Api api in data.Apis)
            {
                api.ApiCapteurs = await GetApisCapteursByIdApiAsync(api.Id);
            }

            return data;

        }

        //Get Patient for delete
        public async Task<PatientC> GetPatientForDeleteAsync(long id)
        {
            PatientC data = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id
            }).Where(p => p.Id == id).FirstOrDefaultAsync();

            data.UnencriptedPatient = await GetPatientAsync(data.Id);


            return data;

        }

        //Get Encrypted Patient
        public PatientC GetPatientC(long id)
        {
            PatientC data = _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Apis = p.Apis,
                Contacts = p.Contacts,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min,
                CumulTempsAllonge = p.CumulTempsAllonge,
                DernierCoucher = p.DernierCoucher,
                DernierLever = p.DernierLever,
                DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                NumCh = p.NumCh
            }).Where(p => p.Id == id).FirstOrDefault();


            data.UnencriptedPatient = GetPatient(data.Id);
            return data;
        }

        //Get asynchrone Encrypted Patient
        public async Task<PatientC> GetPatientCAsync(long id)
        {
            PatientC data = await _context.PatientCs.Select(p => new PatientC
                                                    {
                                                        Id = p.Id,
                                                        Chambre = p.Chambre,
                                                        CheminPhoto = p.CheminPhoto,
                                                        Apis = p.Apis,
                                                        Contacts = p.Contacts,
                                                        Coucher_h = p.Coucher_h,
                                                        Coucher_min = p.Coucher_min,
                                                        CumulTempsAllonge = p.CumulTempsAllonge,
                                                        DernierCoucher = p.DernierCoucher,                                                        
                                                        DernierLever = p.DernierLever,
                                                        DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                                                        Lever_h = p.Lever_h,
                                                        Lever_min = p.Lever_min,
                                                        TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                                                        NumCh = p.NumCh
                                                    }).Where(p => p.Id == id).FirstOrDefaultAsync();


            data.UnencriptedPatient = await GetPatientAsync(data.Id);
            foreach (ContactPatient contact in data.Contacts)
            {
                contact.Utilisateur = (await GetContactPatientAsync(contact.Id)).Utilisateur;
            }
            return data;
        }

        public async Task<PatientC> GetPatientCForDeleteAsync(long id)
        {
            PatientC data = await _context.PatientCs.Select(p => new PatientC
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Apis = p.Apis,
                Contacts = p.Contacts,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min,
                CumulTempsAllonge = p.CumulTempsAllonge,
                DernierCoucher = p.DernierCoucher,
                DernierLever = p.DernierLever,
                DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                Observables = p.Observables,
                NumCh = p.NumCh
            }).Where(p => p.Id == id).FirstOrDefaultAsync();


            data.UnencriptedPatient = await GetPatientAsync(data.Id);
            data.AlarmeCs = (await GetAlarmeByIdPatientAsync(data.Id)).ToList();
            foreach (ContactPatient contact in data.Contacts)
            {
                contact.Utilisateur = (await GetContactPatientAsync(contact.Id)).Utilisateur;
            }
            return data;
        }



        //Get unencrypted Patient
        public Patient GetPatient(long id)
        {
            Patient data = _context.Patients.Select(p => new Patient
            {
                Id = p.Id,
                Chambre = p.Chambre,
                CheminPhoto = p.CheminPhoto,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min,
                CumulTempsAllonge = p.CumulTempsAllonge,
                DernierCoucher = p.DernierCoucher,
                DernierLever = p.DernierLever,
                DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                NumCh = p.NumCh
            }).Where(p => p.Id == id).FirstOrDefault();



            return data;
        }

        //Get asynchrone unenvrypted Patient 
        public async Task<Patient> GetPatientAsync(long id)
        {
            Patient data = await _context.Patients.Select(p => new Patient
                                                            {
                                                                Id = p.Id,
                                                                Chambre = p.Chambre,
                                                                CheminPhoto = p.CheminPhoto,
                                                                Coucher_h = p.Coucher_h,
                                                                Coucher_min = p.Coucher_min,
                                                                CumulTempsAllonge = p.CumulTempsAllonge,
                                                                DerniereLocalisation = p.DerniereLocalisation,
                                                                DernierCoucher = p.DernierCoucher,
                                                                DernierLever = p.DernierLever,
                                                                DureeMaxHorsLit_min = p.DureeMaxHorsLit_min,
                                                                Lever_h = p.Lever_h,
                                                                Lever_min = p.Lever_min,
                                                                TempsMaxAllongeJour = p.TempsMaxAllongeJour,
                                                                NumCh = p.NumCh
                                                            }).Where(p => p.Id == id).FirstOrDefaultAsync();





            return data;
        }

        public async Task<Patient> GetLimitJourAsync(long id)
        {
            return await _context.Patients.Select(p => new Patient
            {
                Id = p.Id,
                Lever_h = p.Lever_h,
                Lever_min = p.Lever_min,
                Coucher_h = p.Coucher_h,
                Coucher_min = p.Coucher_min
            }).Where(p => p.Id == id).FirstOrDefaultAsync() ;
        }

        //Update selected Patient
        public async Task UpdatePatientAsync( PatientC patient, CancellationToken token = default)
        {
            var contextPatientC = _context.PatientCs.First(p => p.Id == patient.Id);
            var contextPatient = _context.Patients.First(p => p.Id == patient.Id);
            contextPatientC.Apis = patient.Apis;
            contextPatientC.Contacts = patient.Contacts;
            contextPatient.CheminPhoto = patient.UnencriptedPatient.CheminPhoto;
            contextPatient.Chambre = patient.UnencriptedPatient.Chambre;
            contextPatient.Coucher_h = patient.UnencriptedPatient.Coucher_h;
            contextPatient.Coucher_min = patient.UnencriptedPatient.Coucher_min;
            contextPatient.Lever_h = patient.UnencriptedPatient.Lever_h;
            contextPatient.Lever_min = patient.UnencriptedPatient.Lever_min;
            contextPatient.DureeMaxHorsLit_min = patient.UnencriptedPatient.DureeMaxHorsLit_min;
            contextPatient.TempsMaxAllongeJour = patient.UnencriptedPatient.TempsMaxAllongeJour;
            contextPatient.NumCh = patient.UnencriptedPatient.NumCh;
            await this._context.SaveChangesAsync(token);
        }

        //Delete selected Patient
        public async Task DeletePatientAsync(PatientC patient, CancellationToken token = default)
        {
            _context.PatientCs.Remove(patient);
            await this._context.SaveChangesAsync(token);
        }
        #endregion
        
        #region Capteurs
        //Get asynchrone all Capteur
        public async Task<IEnumerable<Capteur>> GetCapteursAsync(CancellationToken token = default)
        {

            IEnumerable<Capteur> data = await _context.Capteurs.Select(c => new Capteur
                                                                {
                                                                    ApisCapteurs = c.ApisCapteurs,
                                                                    Modele = c.Modele,
                                                                    Marque = c.Marque,
                                                                    EtatOK = c.EtatOK,
                                                                    Identifiant = c.Identifiant,
                                                                    Type = c.Type,
                                                                    Creation = c.Creation,
                                                                    Id = c.Id,
                                                                    Modification = c.Modification
                                                                }).ToListAsync();

            foreach(Capteur capteur in data)
            {
                foreach(ApiCapteur ac in capteur.ApisCapteurs)
                {
                    ac.Api = (await GetApisCapteursByIdAsync(ac.Id)).Api;
                    ac.Capteur = (await GetApisCapteursByIdAsync(ac.Id)).Capteur;
                }
            }

            return data;


        }

        //Create a capteur
        public async Task CreateCapteurAsync(Capteur Capteur, CancellationToken token = default)
        {
            this._context.Capteurs.Add(Capteur);
            await this._context.SaveChangesAsync(token);
        }

        //Get asynchrone a selected capteur
        public async Task<Capteur> GetCapteurAsyncWithoutPatient(long id, CancellationToken token = default)
        {

            Capteur data = await _context.Capteurs
                            .Include(c => c.ApisCapteurs)
                            .ThenInclude(ac => ac.Api)
                            .FirstOrDefaultAsync(c => c.Id == id, token);

            return data;
        }

        //Get asynchrone a selected capteur
        public async Task<Capteur> GetCapteurAsync(long id, CancellationToken token = default)
        {

            Capteur data = await _context.Capteurs
                            .Include(c => c.ApisCapteurs)
                            .ThenInclude(ac => ac.Api)
                            .ThenInclude(a => a.Patient)
                            .FirstOrDefaultAsync(c => c.Id == id, token);
            foreach (ApiCapteur ac in data.ApisCapteurs)
            {
                if(ac.Api.Patient != null)
                {
                    ac.Api.Patient = await GetPatientCAsync(ac.Api.Patient.Id);
                }


            }

            return data;
        }

        //Get a selected Capteur by identifiant
        public async Task<Capteur> GetCapteurByIdentifiantAsync(string identifiant, CancellationToken token = default)
        {
             Capteur data = await  _context.Capteurs
                                           .Include(c => c.ApisCapteurs)
                                           .ThenInclude(ac => ac.Api)
                                           .ThenInclude(a => a.Patient)
                                           .FirstOrDefaultAsync(c => c.Identifiant == identifiant, token);
            if (data != null) { 
                foreach (ApiCapteur ac in data.ApisCapteurs) {

                    if (ac.Api.Patient != null)
                    {
                        ac.Api.Patient = await GetPatientCAsync(ac.Api.Patient.Id);
                    }

                }
            }

            return data;
        }

        //Update a selected Capteur
        public async Task UpdateCapteurAsync(Capteur Capteur, CancellationToken token = default)
        {
            _context.Update(Capteur);
            await this._context.SaveChangesAsync(token);
        }

        //Delete a selected Capteur
        public async Task DeleteCapteurAsync(Capteur Capteur, CancellationToken token = default)
        {
            IEnumerable<AlarmeC> alarmeCs = await this.GetAlarmeByCapteurAsync(Capteur.Id); 
            foreach(AlarmeC alarmeC in alarmeCs)
            {
                _context.Remove(alarmeC);
            }
            _context.Remove(Capteur);
            await this._context.SaveChangesAsync(token);
        }

        //Check if a capteur exist
        public bool CapteurExists(long id)
        {
            return _context.Capteurs.Any(c => c.Id == id);
        }

        #endregion

        #region Apis - Capteurs

        //Get asynchrone all ApiCapteur
        public async Task<IEnumerable<ApiCapteur>> GetApisCapteursAsync()
        {
            IEnumerable<ApiCapteur> data = await this._context.ApisCapteurs.Select(ac => new ApiCapteur
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Api = ac.Api,
                                                                                    Capteur = ac.Capteur
                                                                                }).ToListAsync();
            foreach (ApiCapteur ac in data)
            {
                ac.Api = await GetApiAsync(ac.Api.Id);
                ac.Capteur = await GetCapteurAsync(ac.Capteur.Id);
            }

            return data;
        }

        //Get asynchrone a ApiCapteur by id
        public async Task<ApiCapteur> GetApisCapteursByIdAsync(long Id)
        {
            ApiCapteur data = await this._context.ApisCapteurs.Select(ac => new ApiCapteur
                                                                {
                                                                    Id = ac.Id,
                                                                    Api = ac.Api,
                                                                    Capteur = ac.Capteur
                                                                }).Where(ac => ac.Id == Id).FirstOrDefaultAsync();
                        
            data.Api = await GetApiAsync(data.Api.Id);
            data.Capteur = await GetCapteurAsync(data.Capteur.Id);
            
            return data;
        }

        //Get asynchrone a ApiCapteur by Api id
        public async Task<IEnumerable<ApiCapteur>> GetApisCapteursByIdApiAsync(long Id)
        {
            IEnumerable<ApiCapteur> data = await this._context.ApisCapteurs.Select(ac => new ApiCapteur
                                                                {
                                                                    Id = ac.Id,
                                                                    Api = new OAuth2Api { Id = ac.Api.Id  },
                                                                    Capteur = ac.Capteur
                                                                }).Where(ac => ac.Api.Id == Id).ToListAsync();
            

            foreach (ApiCapteur ac in data)
            {

                ac.Capteur = await GetCapteurAsyncWithoutPatient(ac.Capteur.Id);
                ac.Api = await GetApisByIdWithoutPatientAsync(ac.Api.Id);
            }

            return data;
        }

        //Check if a/some capteur exist for this api
        public async Task<bool> ApiCapteurExists(OAuth2Api api, Capteur capteur)
        {
            return await this._context.ApisCapteurs.CountAsync(ac => ac.Api.Id == api.Id && ac.Capteur.Id == capteur.Id) != 0;
        }      

        #endregion

        #region OAuth2Api

        //Add an api
        public async Task AddApi(Models.OAuth2Api api, CancellationToken token = default)
        {
            this._context.Add(api);
            await this._context.SaveChangesAsync(token);
        }

        //Delete an Api
        public async Task DeleteApi(Models.OAuth2Api api, CancellationToken token = default)
        {
            this._context.Remove(api);
            await this._context.SaveChangesAsync(token);
        }

        //Update an Api
        public async Task UpdateApi(Models.OAuth2Api api, CancellationToken token = default)
        {
            this._context.Update(api);
            await this._context.SaveChangesAsync(token);
        }

        //Get asynchrone by id an api
        public async Task<OAuth2Api> GetApiAsync(long id)
        {
            var api = await this._context.OAuth2Apis
                .Include(a => a.Patient)
                .Include(a => a.ApiCapteurs)
                .ThenInclude(ac => ac.Capteur)
                .FirstAsync(a => a.Id == id);
            if (api.Patient == null)
            {

            }
            else
            {
                api.Patient = await GetPatientCAsync(api.Patient.Id);
            }
            return api;
        }

        //Get asynchrone all apis
        public async Task<IEnumerable<OAuth2Api>> GetApisAsync()
        {
            var apis = await this._context.OAuth2Apis
            .Include(a => a.Patient)
            .Include(a => a.ApiCapteurs)
            .ThenInclude(ac => ac.Capteur)
            .ToListAsync();
            foreach(OAuth2Api api in apis)
            {
                if (api.Patient == null)
                {

                }
                else
                {
                    api.Patient = await GetPatientCAsync(api.Patient.Id);
                }
            }

            return apis;
        }

        //Get asynchrone api by name
        public async Task<OAuth2Api> GetApisByIdWithoutPatientAsync(long id)
        {
            return await this._context.OAuth2Apis
                .Include(a => a.ApiCapteurs)
                .ThenInclude(ac => ac.Capteur)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        //Get asynchrone api by name
        public async Task<OAuth2Api> GetApisByNameAsync(string apiName)
        {
            return await this._context.OAuth2Apis
                .Include(a => a.ApiCapteurs)
                .ThenInclude(ac => ac.Capteur)
                .FirstOrDefaultAsync(a => a.ApiName == apiName);
        }

        //Delete an api
        public async Task DeleteApiAsync(long id, CancellationToken token = default)
        {
            var api = await this.GetApiAsync(id);
            if(api != null)
            {
                this._context.Remove(api);
                await this._context.SaveChangesAsync(token);
            }
        }

        //Check if api exist
        public bool ApiExists(OAuth2Api api)
        {
            return this._context.OAuth2Apis.Count(a => a.ApiUserId == api.ApiUserId && a.Provider == api.Provider) > 0;
        }

        //Get asynchrone all apis for a patient
        public async Task<IEnumerable<OAuth2Api>> GetPatientApiAsync(long id)
        {
            return await this._context.OAuth2Apis.Where(a => a.Patient.Id == id).ToListAsync();
        }

        //Get asynchrone a api from idPatient and apiType
        public async Task<OAuth2Api> GetApiAsync(long idPatient, ApiType apiType)
        {
            var patientApis = await this.GetPatientApiAsync(idPatient);
            return patientApis.Where(api => api.Provider == apiType.ToString()).First();
        }

        //Get api from type
        public IEnumerable<OAuth2Api> GetApis(ApiType apiType)
        {
            return this._context.OAuth2Apis.Where(a => a.Provider == apiType.ToString());
        }

        //Get api for update
        public IEnumerable<OAuth2Api> GetApisUpdatePatient()
        {
            return this._context.OAuth2Apis
                                .Include(a => a.Patient);
        }

        //Get all apis
        public  IEnumerable<OAuth2Api> GetApis()
        {
            var apis = this._context.OAuth2Apis
                            .Include(a => a.ApiCapteurs)
                            .ThenInclude(ac => ac.Capteur)
                            .ToList();

            return apis;
        }
        #endregion

        #region Alarmes
        
        //Get asynchrone all alarmes
        public async Task<IEnumerable<Alarme>> GetAllAlarmesAsync(CancellationToken token = default)
        {
            return await this._context.Alarmes
                                      .Include(a => a.Capteur)
                                      .Include(a => a.Patient)
                                      .ThenInclude(p => p.Apis)
                                      .ThenInclude(a => a.ApiCapteurs)
                                      .ThenInclude(ac => ac.Capteur)
                                      .Include(a => a.Utilisateur).ToListAsync(token);
        }

        //Get asynchrone alarme by id
        public async Task<Alarme> GetAlarmeByIdAsync(long idAlarme, CancellationToken token = default)
        {
            return await this._context.Alarmes.Select(a => new Alarme
                                                        {
                                                            Id = a.Id,
                                                            Priorite = a.Priorite,
                                                            Creation = a.Creation,
                                                            Description = a.Description,
                                                            Acquittement = a.Acquittement
                                                            
                                                        }).Where(a => a.Id == idAlarme).FirstOrDefaultAsync();
                                                    }

        public async Task<IEnumerable<AlarmeC>> GetAlarmeByIdPatientAsync(long idPatient, CancellationToken token = default)
        {
            return await this._context.Alarmes.Select(a => new AlarmeC
            {
                Id = a.Id,
                Priorite = a.Priorite,
                Creation = a.Creation,
                Acquittement = a.Acquittement,
                Patient = a.Patient

            }).Where(a => a.Patient.Id == idPatient).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Alarme>> GetAlarmeByIdUserAsync(string idUser, CancellationToken token = default)
        {
            return await this._context.Alarmes.Select(a => new Alarme
            {
                Id = a.Id,
                Priorite = a.Priorite,
                Creation = a.Creation,
                Acquittement = a.Acquittement,
                Utilisateur = a.Utilisateur,
                Patient = a.Patient,
                Capteur = a.Capteur,
                Confirmation = a.Confirmation,
                Description = a.Description,
                NbNotifications =  a.NbNotifications

            }).Where(a => a.Utilisateur.Id == idUser).ToListAsync();
        }

        //Get asynchrone alarme by capteurs
        public async Task<IEnumerable<AlarmeC>> GetAlarmeByCapteurAsync(long idCapteur, CancellationToken token = default)
        {
            return await this._context.Alarmes.Select(a => new AlarmeC
            {
                Id = a.Id,
                Priorite = a.Priorite,
                Capteur = a.Capteur,
                Creation = a.Creation,
                Acquittement = a.Acquittement

            }).Where(a => a.Capteur.Id == idCapteur).ToArrayAsync();
        }

        //Get alarmes for Qualité page 
        public async Task<IEnumerable<Alarme>> GetQualitéAlarmesAsync(CancellationToken token = default)
        {
            var alarmes = await this._context.Alarmes.Select(a => new Alarme
                                                 {
                                                     Id = a.Id,
                                                     Priorite = a.Priorite,
                                                     Creation = a.Creation,
                                                     Acquittement = a.Acquittement,
                                                     Patient = new PatientC
                                                                {
                                                                    Id = a.Patient.Id
                                                                 },
                                                 }).ToListAsync();

            foreach (Alarme alarme in alarmes)
            {
                alarme.Patient = await GetPatientCAsync(alarme.Patient.Id);
            }

            return alarmes;
        }

        //Get asynchrone all Alarmes where acquitter == null
        public async Task<IEnumerable<Alarme>> GetAlarmesAcquitterAsync(CancellationToken token = default)
        {

            var data =  await _context.Alarmes.Select(a => new Alarme
                                                {
                                                    Id = a.Id,
                                                    Priorite = a.Priorite,
                                                    Capteur = a.Capteur,
                                                    Creation = a.Creation,
                                                    Description = a.Description,
                                                    Acquittement = a.Acquittement,
                                                    NbNotifications = a.NbNotifications,
                                                    Patient = new PatientC { 
                                                                            Id = a.Patient.Id
                                                                            }
                                                }).Where(a => a.Acquittement == null).OrderByDescending(a => a.Priorite).ToListAsync();
            var dureeMax = 0;
            if (_configuration.GetValue<int>("Alarmes:Duree") == 0)
            {
                dureeMax = 24;
            }
            else
            {
                dureeMax = _configuration.GetValue<int>("Alarmes:Duree");
            }
            data = data.Where(a => a.Duree.TotalHours <= dureeMax && a.Duree.TotalHours >= 0).ToList();
            foreach (Alarme a in data)
            {
                if (a.Capteur != null)
                {
                    a.Capteur = await GetCapteurAsync(a.Capteur.Id);
                }

                if (a.Patient != null)
                {
                    a.Patient = await GetPatientCAsync(a.Patient.Id);
                }
               
            }

            return data;

        }

        public async Task<IEnumerable<Alarme>> GetAlarmesAcquitterTestAsync(CancellationToken token = default)
        {

            var data = await _context.Alarmes.Select(a => new Alarme
            {
                Id = a.Id,
                Priorite = a.Priorite,
                Creation = a.Creation,
                Description = a.Description,
                Acquittement = a.Acquittement,
                NbNotifications = a.NbNotifications,
            }).Where(a => a.Acquittement == null).OrderByDescending(a => a.Priorite).ToListAsync();
            var dureeMax = 0;
            if (_configuration.GetValue<int>("Alarmes:Duree") == 0)
            {
                dureeMax = 24;
            }
            else
            {
                dureeMax = _configuration.GetValue<int>("Alarmes:Duree");
            }
            data = data.Where(a => a.Duree.TotalHours <= dureeMax && a.Duree.TotalHours >= 0).ToList();

            return data;

        }

        //Make an alarm acquit
        public async Task AcquitterAlarmeAll(IEnumerable<Alarme> alarmes, User utilisateur, CancellationToken token = default)
        {
            foreach (Alarme alarme in alarmes)
            {
                var updatedAlarme = await this._context.Alarmes.FindAsync(alarme.Id);
                updatedAlarme.Acquittement = DateTime.Now;
                updatedAlarme.Utilisateur = utilisateur;
                updatedAlarme.NbNotifications = 0;
                //alarme.Confirmation = confirmation;
                this._context.Update(updatedAlarme);
            }
            await this._context.SaveChangesAsync();

        }

        //Make an alarm acquit
        public async Task AcquitterAlarme(long id, bool? confirmation, User utilisateur, CancellationToken token = default)
        {
            var alarme = await this._context.Alarmes.FindAsync(id);
            alarme.Acquittement = DateTime.Now;
            alarme.Utilisateur = utilisateur;
            alarme.NbNotifications = 0;
            alarme.Confirmation = confirmation;
            this._context.Update(alarme);
            await this._context.SaveChangesAsync(token);
        }

        //Generate an alarm
        public async Task CreerAleatoirement(CancellationToken token = default)
        {
            PatientC patient = null;
            Random rnd = new Random();
            while(patient is null)
            {
                var patients = await this.GetPatientsAsync();
                var index = rnd.Next(0, patients.Count());
                patient = await this.GetPatientCByIdAsync(index);
                if(token.IsCancellationRequested) { return; }
            }
            Capteur capteur = null;
            while(capteur is null)
            {
                var capteurs = await this.GetCapteursAsync();
                var index = rnd.Next(0, capteurs.Count());
                capteur = capteurs.ElementAt(index);
                if(token.IsCancellationRequested) { return; }
            }
            int priorite = rnd.Next(101);
            var alarme = new Alarme() { Creation = DateTime.UtcNow, Capteur = capteur, Patient = patient, Priorite = priorite, Description = "Alarme generee aleatoirement" };
            this._context.Add(alarme);
            await this._context.SaveChangesAsync(token);
        }

        #endregion

        #region Contacts Patients
        //Get asynchrone all ContactPatient 
        public async Task<IEnumerable<ContactPatient>> GetContactsPatientsAsync(CancellationToken token = default)
        {
            return await ( _context.ContactsPatients
                  .Include(p => p.Patient)).ToListAsync(token);
        }

        //Create asynchrone a ContactPatient
        public async Task CreateContactPatientAsync(ContactPatient contact, CancellationToken token = default)
        {
            this._context.ContactsPatients.Add(contact);
            await this._context.SaveChangesAsync(token);
        }

        //Get a ContactPatient by Patient id
        public IEnumerable<ContactPatient> GetContactsPatientByPatient(long patientId)
        {
            return _context.ContactsPatients.Include(p => p.Patient).Where(cp => cp.Patient.Id == patientId);
        }

        //Get asynchrone a ContactPatient
        public async Task<ContactPatient> GetContactPatientAsync(long id)
        {
            return await _context.ContactsPatients.Include(p => p.Utilisateur).Where(cp => cp.Id == id).FirstAsync();
        }

        //Get a ContactPatient
        public ContactPatient GetContactPatient(long id)
        {
            return _context.ContactsPatients.Include(p => p.Patient).Where(cp => cp.Id == id).First();
        }
        
        //Update a ContactPatient 
        public async Task UpdateContactPatientAsync(ContactPatient contact, CancellationToken token = default)
        {
            _context.Update(contact);
            await this._context.SaveChangesAsync(token);
        }

        //Add a ContactPatient
        public void AddContactPatient(ContactPatient contact)
        {
            _context.Add(contact);
            this._context.SaveChanges();
        }

        //Delete a ContactPatient
        public void DeleteContactPatient(ContactPatient contact)
        {
            _context.Remove(contact);
            this._context.SaveChanges();
        }

        //Delete async a ContactPatient
        public async Task DeleteContactPatientAsync(ContactPatient contact, CancellationToken token = default)
        {
            _context.Remove(contact);
            await this._context.SaveChangesAsync(token);
        }

        //Delete all ContactPatient for an user
        public async Task DeleteContactPatientByUser(User user, CancellationToken token = default)
        {
            var ListCPPatient = await _context.ContactsPatients.Where(cp => cp.Utilisateur.Id == user.Id).ToListAsync();
            if (ListCPPatient.Count > 0)
            {
                
                _context.RemoveRange(ListCPPatient);
            }

            await this._context.SaveChangesAsync(token);
        }
        #endregion

        #region Nuits

        //Get asynchrone all Nuits
        public async Task<IEnumerable<Nuit>> GetNuitsAsync(CancellationToken token = default)
        {
            return await this._context.Nuits.ToListAsync(token);
        }

        //Get asynchrone all id Nuit
        public async Task<IEnumerable<Nuit>> GetIdNuits(CancellationToken token = default)
        {
            return await this._context.Nuits.Select(n => new Nuit{
                                                    Id = n.Id
                                                }).ToListAsync(token);
        }

        //Get asynchrone a Nuit from id
        public async Task<Nuit> GetNuitId(long id, CancellationToken token = default)
        {
            return await this._context.Nuits.Select(n => new Nuit
            {
                Id = n.Id,
                DateDebut = n.DateDebut,
                DateFin = n.DateFin,
                DateNuit = n.DateNuit,
                DetailSorties = n.DetailSorties,
                DureeReveilAuLit = n.DureeReveilAuLit,
                DureeReveilHorsLit = n.DureeReveilHorsLit,
                DureeSommeil = n.DureeSommeil,
                NbReveils = n.NbReveils,
                NbSorties = n.NbSorties,
                NuitTraitee = n.NuitTraitee,
                PatientId = n.PatientId,
                ScoreNuit = n.ScoreNuit

            }).Where(n => n.Id == id).FirstOrDefaultAsync();
        }

        //Get asynchrone all Nuit for a Patient
        public async Task<IEnumerable<NuitC>> GetNuitsByPatientAsync(long Id, CancellationToken token = default)
        {
            IEnumerable<NuitC> nuits = await this._context.NuitCs.Select(n => new NuitC
            {

                Id = n.Id,
                DateDebut = n.DateDebut,
                DateFin = n.DateFin,
                DateNuit = n.DateNuit,
                DetailSorties = n.DetailSorties,
                DureeReveilAuLit = n.DureeReveilAuLit,
                DureeReveilHorsLit = n.DureeReveilHorsLit,
                DureeSommeil = n.DureeSommeil,
                NbReveils = n.NbReveils,
                NbSorties = n.NbSorties,
                NuitTraitee = n.NuitTraitee,
                PatientId = n.PatientId,
                ScoreNuit = n.ScoreNuit
            }).Where(n => n.PatientId == Id).ToListAsync(token);

            foreach(NuitC nuit in nuits)
            {
                nuit.UnencriptedNuit = await GetNuitId(nuit.Id);
            }

            return nuits;
        }

        //Get asynchrone all Nuit for a Patient
        public async Task<IEnumerable<NuitC>> GetLast7NuitsByPatientAsync(long Id, CancellationToken token = default)
        {
            IEnumerable<NuitC> nuits = await this._context.NuitCs.Select(n => new NuitC
            {

                Id = n.Id,
                DateDebut = n.DateDebut,
                DateFin = n.DateFin,
                DateNuit = n.DateNuit,
                DetailSorties = n.DetailSorties,
                DureeReveilAuLit = n.DureeReveilAuLit,
                DureeReveilHorsLit = n.DureeReveilHorsLit,
                DureeSommeil = n.DureeSommeil,
                NbReveils = n.NbReveils,
                NbSorties = n.NbSorties,
                NuitTraitee = n.NuitTraitee,
                PatientId = n.PatientId,
                ScoreNuit = n.ScoreNuit
            }).Where(n => n.PatientId == Id).Where(n => n.PatientId == Id && n.DateNuit <= DateTime.Now && n.DateNuit >= DateTime.Now.AddDays(-8)).ToListAsync(token);

            foreach (NuitC nuit in nuits)
            {
                nuit.UnencriptedNuit = await GetNuitId(nuit.Id);
            }

            return nuits;
        }

        //Get asynchrone last 7 Nuit for a Patient 
        public async Task<IEnumerable<Nuit>> GetNuitForGraph(long Id, CancellationToken token = default)
        {
            return await this._context.Nuits.Select(n => new Nuit
            {

                Id = n.Id,
                DateDebut = n.DateDebut,
                DateFin = n.DateFin,
                DateNuit = n.DateNuit,
                DetailSorties = n.DetailSorties,
                DureeReveilAuLit = n.DureeReveilAuLit,
                DureeReveilHorsLit = n.DureeReveilHorsLit,
                DureeSommeil = n.DureeSommeil,
                NbReveils = n.NbReveils,
                NbSorties = n.NbSorties,
                NuitTraitee = n.NuitTraitee,
                PatientId = n.PatientId,
                ScoreNuit = n.ScoreNuit,

            }).Where(n => n.PatientId == Id && n.DateNuit <= DateTime.Now && n.DateNuit >= DateTime.Now.AddDays(-8)).ToListAsync();
        }
        #endregion

        #region Jours

        //Get asynchrone all Jours
        public async Task<IEnumerable<Jour>> GetJoursAsync(CancellationToken token = default)
        {
            return await this._context.Jours.ToListAsync(token);
        }

        public async Task<ICollection<JourC>> GetJoursCByPatientAsync(long idPatient, CancellationToken token = default)
        {
            var data = await _context.JourCs.Select(j => new JourC {
                Id = j.Id,
                PatientId = j.PatientId,
                DateJour = j.DateJour
            }).Where(j => j.PatientId == idPatient && j.DateJour <= DateTime.Today && j.DateJour >= DateTime.Today.AddDays(-7)).OrderBy(j => j.DateJour).ToListAsync();

            foreach (JourC jourC in data)
            {
                jourC.UnencriptedJour = await GetJourAsync(jourC.Id);
            }

            return data;
        }

        public async Task<Jour> GetJourAsync( long idJour, CancellationToken token = default)
        {
            return await _context.Jours.Select(j => new Jour
            {
                Id = j.Id,
                DateJour = j.DateJour,
                PatientId = j.PatientId,
                TempsAllongeTotal = j.TempsAllongeTotal,
                TempsTotalActivite = j.TempsTotalActivite,
                VitesseMarcheMoyenne = j.VitesseMarcheMoyenne,
                NbPas = j.NbPas
            }).Where(j => j.Id == idJour).FirstOrDefaultAsync();

        }

        #endregion

        #region Balances

        //Get asynchrone all Balances
        public async Task<IEnumerable<Balance>> GetBalancesAsync(CancellationToken token = default)
        {
            return await this._context.Balances.Select(b => new Balance
                                                {
                                                    Id = b.Id,
                                                    Nom = b.Nom,
                                                    DernierePesee = b.DernierePesee,
                                                    Valeur = b.Valeur
                                                }).ToListAsync();
        }

        //Create asynchrone a Balance
        public async Task CreateBalancesAsync(Balance balance,  CancellationToken token = default)
        {
             this._context.Balances.Add(balance);
             await this._context.SaveChangesAsync();
        }
        #endregion

        #region Observables

        //Get asynchrone all Obseravbles
        public async Task<IEnumerable<Observable>> GetObservablesAsync(CancellationToken token = default)
        {
            return await this._context.Observables.Select(O => new Observable
                                                   {
                                                        Id = O.Id,
                                                        PatientId = O.PatientId,
                                                        Chambre = O.Chambre,
                                                        Date = O.Date,
                                                        Values = O.Values,
                                                        TypeObservable = O.TypeObservable
                                                   }).ToArrayAsync();
        }
        #endregion

        #region TypeObservables

        //Get asynchrone all TypeObservables
        public async Task<IEnumerable<TypeObservable>> GetTypeObservablesAsync(CancellationToken token = default)
        {
            return await this._context.TypeObservables.Select(TO => new TypeObservable
                                                    {
                                                       Id = TO.Id,
                                                       Type = TO.Type,
                                                       Description = TO.Description,
                                                       URI = TO.URI
                                                    }).ToArrayAsync();
        }

        //Get asynchrone a TypObservable
        public async Task<TypeObservable> GetTypeObservablesByObservablesAsync(long id ,CancellationToken token = default)
        {
            return await this._context.TypeObservables.Select(TO => new TypeObservable
            {
                Id = TO.Id,
                Type = TO.Type,
                Description = TO.Description,
                URI = TO.URI
            }).Where(TO => TO.Id == id).FirstOrDefaultAsync();
        }
        #endregion

        #region Tracker

        //Add a new Tracker
        public async Task AddTracker(Tracker tracker , CancellationToken token = default)
        {
            if ((await GetTrackersAsync()).Count() != 0)
            {
                tracker.Id = (await GetTrackersAsync()).Max(t => t.Id) + 1;
            }
            else
            {
                tracker.Id = 1;
            }

            this._context.Add(tracker);
            await this._context.SaveChangesAsync(token);
        }

        //Get asynchrone Tracker
        public async Task<Tracker> GetTrackerAsync(long id)
        {
            return await _context.Trackers.Select(Tr => new Tracker
            {
                Id = Tr.Id,
                AccVector = Tr.AccVector,
                Capteur = Tr.Capteur,
                LastUpdate = Tr.LastUpdate,
                LecturesWifi = Tr.LecturesWifi,
                NbPas = Tr.NbPas,
                Power = Tr.Power,
                Traite = Tr.Traite
            }).Where(Tr => Tr.Id ==  id).FirstOrDefaultAsync();
        }
        
        //Get asynchrone all Trackers
        public async Task<IEnumerable<Tracker>> GetTrackersAsync()
        {
            return await _context.Trackers.Select(Tr => new Tracker
            {
                Id = Tr.Id,
                AccVector = Tr.AccVector,
                Capteur = Tr.Capteur,
                LastUpdate = Tr.LastUpdate,
                LecturesWifi = Tr.LecturesWifi,
                NbPas = Tr.NbPas,
                Power = Tr.Power,
                Traite = Tr.Traite
            }).ToListAsync();
        }

        //Get asynchrone a Tracker from id Capteur
        public async Task<Tracker> GetTrackerAsyncByIdCapteur(long idCapteur)
        {
            return await _context.Trackers.Select(Tr => new Tracker
            {
                Id = Tr.Id,
                AccVector = Tr.AccVector,
                Capteur = Tr.Capteur,
                LastUpdate = Tr.LastUpdate,
                LecturesWifi = Tr.LecturesWifi,
                NbPas = Tr.NbPas,
                Power = Tr.Power,
                Traite = Tr.Traite
            }).Where(Tr => Tr.Capteur.Id == idCapteur).FirstOrDefaultAsync();
        }

        //Update a Tracker
        public async Task UpdateTracker(Tracker tracker, CancellationToken token = default)
        {
            this._context.Update(tracker);
            await this._context.SaveChangesAsync(token);
        }
        #endregion

        #region ScoreForme

        //Get asynchrone all ScoreForme 
        public async Task<IEnumerable<ScoreForme>> GetScoreFormeAsync()
        {
            List<ScoreForme> scoreFormes = new List<ScoreForme>();
            IEnumerable<ScoreForme> scoreFormesWithoutDateFilter = await _context.ScoreFormes.Select(SF => new ScoreForme
            {
                Id = SF.Id,
                Current_values = SF.Current_values,
                Date = SF.Date,
                IndiceDeConfiance = SF.IndiceDeConfiance,
                NumCh = SF.NumCh,
                ScoreJour = SF.ScoreJour,
                ScorePred = SF.ScorePred,
                TauxDeRemp = SF.TauxDeRemp
            }).OrderBy(SF => SF.NumCh).ToListAsync();

            var uniqueNbCh = scoreFormesWithoutDateFilter.Select(SF => SF.NumCh).Distinct().ToList();
            foreach ( int nbCh in uniqueNbCh)
            {
                var MaxDate = scoreFormesWithoutDateFilter.Where(Sf => Sf.NumCh == nbCh && Sf.Date <= DateTime.Today.AddDays(-1)).FirstOrDefault();
                ScoreForme maxDateScore;
                if (MaxDate == null)
                {
                    maxDateScore = (ScoreForme)scoreFormesWithoutDateFilter.Where(Sf => Sf.NumCh == nbCh && Sf.Date.Equals(scoreFormesWithoutDateFilter.Where(Sf => Sf.NumCh == nbCh).Max(Sf => Sf.Date).Date)).FirstOrDefault();
                }
                else
                {
                    maxDateScore = (ScoreForme)scoreFormesWithoutDateFilter.Where(Sf => Sf.NumCh == nbCh && Sf.Date.Equals(scoreFormesWithoutDateFilter.Where(Sf => Sf.NumCh == nbCh && Sf.Date <= DateTime.Today.AddDays(-1)).Max(Sf => Sf.Date).Date)).FirstOrDefault();
                }
                scoreFormes.Add(maxDateScore);
            }

            foreach (ScoreForme scoreForme in scoreFormes)
            {
                var ScoreDetails = (await GetLastInsertedData(scoreForme.NumCh));
                if (ScoreDetails != null)
                {
                    scoreForme.Date = ScoreDetails.Date;
                }
            }


            return scoreFormes.ToList();
        }

        //Get asynchrone all ScoreForme for a chambre number
        public async Task<IEnumerable<ScoreForme>> GetScoreFormeByIdAsync(long NumCh)
        {
            return await _context.ScoreFormes.Select(SF => new ScoreForme
            {
                Id = SF.Id,
                Current_values = SF.Current_values,
                Date = SF.Date,
                IndiceDeConfiance = SF.IndiceDeConfiance,
                NumCh = SF.NumCh,
                ScoreJour = SF.ScoreJour,
                ScorePred = SF.ScorePred,
                TauxDeRemp = SF.TauxDeRemp
            }).Where(SF => SF.NumCh == NumCh && SF.Date <= DateTime.Today.AddDays(-1) && SF.Date >= DateTime.Today.AddDays(-8)).ToListAsync();
        }

        //Get asynchrone all ScoreForme group by NumCh
        public async Task<IEnumerable<IGrouping<int, ScoreForme>>> GetScoresCH()
        {
            return (await _context.ScoreFormes.Select(SF => new ScoreForme
                                                        {
                                                            NumCh = SF.NumCh,
                                                        }).OrderBy(SF => SF.NumCh).ToListAsync()).GroupBy(SF => SF.NumCh);
        }

        //Get asynchrone all ScoreForme for a NumCh
        public async Task<IEnumerable<ScoreForme>> GetScoresByCh(int numCh)
        {
            return await _context.ScoreFormes.Select(SF => new ScoreForme
                                                    {
                                                         Id = SF.Id,
                                                         Date = SF.Date,
                                                         Current_values = SF.Current_values,
                                                         IndiceDeConfiance = SF.IndiceDeConfiance,
                                                         NumCh = SF.NumCh,
                                                         ScoreJour = SF.ScoreJour,
                                                         ScorePred = SF.ScorePred,
                                                         TauxDeRemp = SF.TauxDeRemp
                                                    }).Where(SF => SF.NumCh == numCh).ToListAsync() ;
        }

        //Delete all ScoreForme in IEnumerable Scores
        public async Task DeleteScores(IEnumerable<ScoreForme> Scores , CancellationToken Token = default)
        {
            foreach(ScoreForme score in Scores)
            {
                _context.Remove(score);
                await this._context.SaveChangesAsync(Token);
            }
        }

        //Get last time where linto has insert data for a NumCh
        public async Task<ScoreForme> GetLastInsertedData(int NumCh)
        {
            return await _context.ScoreFormes.Select(SF => new ScoreForme
                                                {
                                                    Id = SF.Id,
                                                    NumCh = SF.NumCh,
                                                    Date = SF.Date,
                                                    Current_values = SF.Current_values
                                                })
                                                .Where(SF => SF.NumCh == NumCh && SF.Current_values != "[]" && SF.Current_values != null )
                                                .OrderByDescending(SF => SF.Date)
                                                .FirstOrDefaultAsync();
        }


        #endregion
    }

}
