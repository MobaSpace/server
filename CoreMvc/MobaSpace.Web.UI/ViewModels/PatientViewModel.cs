using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobaSpace.Core.Data.Models;
using MobaSpace.Core.File;
using MobaSpace.Web.UI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Web.UI.ViewModels
{
    public class PatientViewModel
    {
        public PatientC Patient { get; set; }

        public Nuit Nuit { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(Extensions = ".jpg|.png|.gif|.jpeg|.bmp", Optionnal = true)]
        [Display(Name = "Photo")]
        public IFormFile Image { get; set; }

        public List<ContactSelectionne> ContactsPatient {get; set;} = new List<ContactSelectionne>();
        public List<ApiSelectionne> ApisPatient {get; set;} = new List<ApiSelectionne>();

        public PatientViewModel()
        {

        }

        public PatientViewModel(IEnumerable<User> soignants, IEnumerable<OAuth2Api> apis)
        {
            foreach (var utilisateur in soignants)
            {
                this.ContactsPatient.Add(new ContactSelectionne() { Utilisateur = utilisateur, Selectionne = false });
            }
            foreach (var api in apis)
            {
                if (api.Patient != null)//null 
                {
                    continue;
                }
                this.ApisPatient.Add(new ApiSelectionne() { Api = api, Selectionne = false });
            }

        }

        public PatientViewModel(PatientC patient, IEnumerable<User> soignants, IEnumerable<OAuth2Api> apis)
        {
            this.Patient = patient;
            foreach (var utilisateur in soignants)
            {
                this.ContactsPatient.Add(new ContactSelectionne() { Utilisateur = utilisateur, Selectionne = false });
            }
            foreach (var contact in this.Patient.Contacts)
            {
                var cs = this.ContactsPatient.First(c => c.Utilisateur.UserName == contact.Utilisateur.UserName);
                cs.Selectionne = true;
            }

            foreach (var api in apis)
            {
                if(api.Patient != null && api.Patient.Id != patient.Id)//null and patient
                {
                    continue;
                }
                this.ApisPatient.Add(new ApiSelectionne() { Api = api, Selectionne = false });
            }
            foreach (var api in patient.Apis)
            {
                var cs = this.ApisPatient.First(a => a.Api.Id == api.Id);
                cs.Selectionne = true;
            }
        }
    }

    public class ContactSelectionne
    {
        public User Utilisateur { get; set; }
        public bool Selectionne { get; set; }
    }

    public class ApiSelectionne
    {
        public OAuth2Api Api { get; set; }
        public bool Selectionne { get; set; }
    }
}
