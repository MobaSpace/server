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
    public class OAuth2ApiViewModel
    {
        public OAuth2Api Api { get; set; }

        public Capteur Capteur { get; set; }

        public Tracker Tracker { get; set; }

        public OAuth2ApiViewModel()
        {

        }

        public OAuth2ApiViewModel(OAuth2Api api)
        {
            this.Api = api;

        }
        public OAuth2ApiViewModel(ApiCapteur apiCapteur)
        {
            this.Api = apiCapteur.Api;
            this.Capteur = apiCapteur.Capteur;

        }
    }
}
