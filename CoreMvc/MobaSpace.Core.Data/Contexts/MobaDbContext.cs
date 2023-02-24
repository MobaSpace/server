
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobaSpace.Core.Data.Models;
using System;

namespace MobaSpace.Core.Data
{
    public class MobaDbContext : IdentityDbContext<User>
    {
        public MobaDbContext(DbContextOptions options) : base(options)
        {
        }

        public MobaDbContext()
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("mobaspace_data");
            base.OnModelCreating(builder);

            #region ViewDeclare

            builder.Entity<Nuit>()
                   .ToView("nuitsview");

            builder.Entity<Jour>()
                  .ToView("joursview");

            builder.Entity<Alarme>()
                  .ToView("alarmesview");

            builder.Entity<Observable>()
                  .ToView("observablesview");

            builder.Entity<Patient>()
                  .ToView("patientsview");

            builder.Entity<Tracker>()
                  .ToView("trackersview");

            builder.Entity<InfoBlocChambre>()
                  .ToView("Historiqueview");

            #endregion

            builder.Entity<NuitC>()
                    .HasIndex(n => new { n.PatientId, n.DateNuit })
                    .HasName("U_Date_PatientC")
                    .IsUnique();
            builder.Entity<JourC>()
                    .HasIndex(j => new { j.PatientId, j.DateJour })
                    .HasName("U_Date_J_PatientC")
                    .IsUnique();
            builder.Entity<Balance>()
                    .HasIndex(b => b.AdresseMAC)
                    .IsUnique();
            builder.Entity<Balance>()
                    .HasIndex(b => b.Nom)
                    .IsUnique();
            builder.Entity<TypeObservable>()
                    .HasIndex(TO => TO.Type)
                    .IsUnique();
            builder.Entity<PatientC>()
                   .HasIndex(p => p.NumCh)
                   .IsUnique();
            builder.Entity<Transaction>()
                   .Property(t => t.CodeRetour)
                   .IsRequired();
            builder.Entity<Transaction>()
                   .HasIndex(t => t.URI)
                   .IsUnique();
            builder.Entity<Transaction>()
                   .Property(t => t.URI)
                   .IsRequired();
            builder.Entity<Capteur>()
                   .HasIndex(c => c.Identifiant)
                   .IsUnique();
            builder.Entity<OAuth2Api>()
                   .Property(Ao=> Ao.EtatOK)
                   .HasDefaultValue(true)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<TrackerC>()
                   .Property(t => t.Power)
                   .HasDefaultValueSql("0");
            builder.Entity<PatientC>()
                   .Property(Pc => Pc.NouvellesDonneesLit)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<JourC>()
                   .Property(Jc => Jc.JourTraite)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<NuitC>()
                   .Property(Nc => Nc.NuitTraitee)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<ObservableC>()
                   .Property(Oc => Oc.ObservableTraite)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<InfoBlocChambreC>()
                   .Property(IBC => IBC.Traite)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<InfoBlocChambreC>()
                   .Property(IBC => IBC.UriNetSOINS)
                   .HasDefaultValue("")
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<PatientC>()
                   .Property(Pc => Pc.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<JourC>()
                   .Property(Jc => Jc.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<NuitC>()
                   .Property(Nc => Nc.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<ObservableC>()
                   .Property(Oc => Oc.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<TrackerC>()
                   .Property(Tc => Tc.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<AlarmeC>()
                   .Property(Ac => Ac.key_id)
                   .HasDefaultValue(0)
                   .ValueGeneratedOnAdd();
            builder.Entity<InfoBlocChambreC>()
                   .Property(Hc => Hc.key_id)
                   .HasDefaultValue(0);
            builder.Entity<AlarmeC>()
                   .Property(Ac => Ac.Priorite)
                   .HasDefaultValue(1)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<AlarmeC>()
                   .Property(Ac => Ac.Creation)
                   .HasDefaultValueSql("Now()")
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<AlarmeC>()
                   .Property(Ac => Ac.NbNotifications)
                   .HasDefaultValue(5)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<JourC>()
                   .Property(Jc => Jc.DateJour)
                   .HasDefaultValueSql("NOW()")
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<JourC>()
                   .Property(Jc => Jc.JourTraite)
                   .HasDefaultValue(false)
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<NuitC>()
                   .Property(Nc => Nc.DateNuit)
                   .HasDefaultValueSql("NOW()")
                   .ValueGeneratedOnAddOrUpdate();
            builder.Entity<ObservableC>()
                  .Property(Oc => Oc.Date)
                  .HasDefaultValueSql("NOW()")
                  .ValueGeneratedOnAddOrUpdate();
            builder.HasPostgresEnum<Periodes>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Alarme> Alarmes { get; set; }
        public virtual DbSet<ApiCapteur> ApisCapteurs { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Capteur> Capteurs { get; set; }
        public virtual DbSet<Jour> Jours { get; set; }
        public virtual DbSet<Nuit> Nuits { get; set; }
        public virtual DbSet<ContactPatient> ContactsPatients { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<ScoreForme> ScoreFormes { get; set; }
        public virtual DbSet<OAuth2Api> OAuth2Apis { get; set; }
        public virtual DbSet<Observable> Observables { get; set; }
        public virtual DbSet<TypeObservable> TypeObservables { get; set; }
        public virtual DbSet<CheckList> CheckLists { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Tracker> Trackers { get; set; }
        public virtual DbSet<InfoBlocChambre> Historiques { get; set; }
        public virtual DbSet<PatientC> PatientCs { get; set; }
        public virtual DbSet<JourC> JourCs { get; set; }
        public virtual DbSet<NuitC> NuitCs { get; set; }
        public virtual DbSet<ObservableC> ObservableCs { get; set; }
        public virtual DbSet<AlarmeC> AlarmeCs { get; set; }
        public virtual DbSet<TrackerC> TrackerCs { get; set; }
        public virtual DbSet<InfoBlocChambreC> HistoriqueCs { get; set; }
    }
}
