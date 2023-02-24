using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobaSpace.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MobaSpace.Core.Data.Models
{
    [Table("Patients")]
    public class PatientC
    {
        public long Id { get; set; }
        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }

        public byte[] Chambre { get; set; }
        public byte[] NumCh { get; set; }
        public string CheminPhoto { get; set; }

        public byte[] DernierCoucher { get; set; }

        [DefaultValue(false)]
        public bool NouvellesDonneesLit { get; set; } = false;
        public byte[] DernierLever { get; set; }

        public int? Coucher_h { get; set; }
        public int? Coucher_min { get; set; }
        public int? Lever_h { get; set; }
        public int? Lever_min { get; set; }
        public int? DureeMaxHorsLit_min { get; set; }
        public POSTURE? Posture { get; set; }
        public int? TempsMaxAllongeJour { get; set; }
        public byte[] DerniereLocalisation { get; set; }

        public byte[] CumulTempsAllonge { get; set; }

        public virtual ICollection<OAuth2Api> Apis { get; set; }
        public virtual ICollection<ContactPatient> Contacts { get; set; }
        public virtual ICollection<NuitC> Nuits { get; set; }

        public virtual ICollection<JourC> Jours { get; set; }

        public virtual ICollection<ObservableC> Observables { get; set; }

        public virtual ICollection<AlarmeC> AlarmeCs { get; set; }


        [NotMapped]
        public IEnumerable<Capteur> Capteurs
        {
            get
            {
                if (Apis != null)
                {
                    foreach (var api in Apis)
                    {
                        foreach (var ac in api.ApiCapteurs)
                        {
                            yield return ac.Capteur;
                        }
                    }
                }
            }
        }

        [NotMapped]
        public virtual Patient UnencriptedPatient { get; set; }


        public String LastUpdate
        {
            get
            {
                String value;
                DateTime?[] tabLastDate;
                if (this.UnencriptedPatient == null)
                {
                    tabLastDate = new DateTime?[] { null, null };
                }
                else
                {
                    tabLastDate = new DateTime?[] { this.UnencriptedPatient.DernierLever, this.UnencriptedPatient.DernierCoucher };
                }

                if (tabLastDate.Max() is null)
                {
                    TimeSpan? Timediff = DateTime.Now - new DateTime();
                    value = Timediff?.ThereIs();
                }
                else
                {
                    TimeSpan? TimeDiff = DateTime.Now - tabLastDate.Max();
                    value = TimeDiff?.ThereIs();
                }
                return value;
            }
        }

        public TimeSpan? LastUpdateTS
        {
            get
            {
                TimeSpan? timediff;
                DateTime?[] tabLastDate;
                if (this.UnencriptedPatient == null)
                {
                    tabLastDate = new DateTime?[] { null, null };
                }
                else
                {
                    tabLastDate = new DateTime?[] { this.UnencriptedPatient.DernierLever, this.UnencriptedPatient.DernierCoucher };
                }

                if (tabLastDate.Max() is null)
                {
                    timediff = DateTime.Now - new DateTime();
                }
                else
                {
                    timediff = DateTime.Now - tabLastDate.Max();
                }
                return timediff;
            }
        }

        public string GetStringLoca()
        {
            JObject json = JObject.Parse(this.UnencriptedPatient.DerniereLocalisation);
            if (json.SelectToken("date-heure") != null && json.SelectToken("Piece") != null)
            {
                DateTime date = DateTime.Parse(json.SelectToken("date-heure").ToString());
                TimeSpan? timediff = DateTime.Now - date;
                return "" + json.SelectToken("Piece") + " " + timediff?.ThereIs();
            }
            return "Indisponible";
        }

        public override string ToString()
        {
            return $"{Chambre}";
        }

        public enum POSTURE
        {
            Debout = 2,
            Assis = 1,
            Allonge = 0
        }

    }
}
