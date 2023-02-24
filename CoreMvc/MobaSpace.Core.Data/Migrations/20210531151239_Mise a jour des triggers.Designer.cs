﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobaSpace.Core.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    [DbContext(typeof(MobaDbContext))]
    [Migration("20210531151239_Mise a jour des triggers")]
    partial class Miseajourdestriggers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("mobaspace_data")
                .HasAnnotation("Npgsql:Enum:periodes", "matin,midi,soir")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.AlarmeC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Acquittement")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("CapteurId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Confirmation")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("Now()");

                    b.Property<byte[]>("Description")
                        .HasColumnType("bytea");

                    b.Property<int>("NbNotifications")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("integer")
                        .HasDefaultValue(5);

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<int>("Priorite")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("UtilisateurId")
                        .HasColumnType("text");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("CapteurId");

                    b.HasIndex("PatientId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Alarmes");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ApiCapteur", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ApiId")
                        .HasColumnType("bigint");

                    b.Property<long>("CapteurId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ApiId");

                    b.HasIndex("CapteurId");

                    b.ToTable("ApisCapteurs");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Balance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AdresseMAC")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DernierePesee")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Lectures")
                        .HasColumnType("jsonb");

                    b.Property<string>("Nom")
                        .HasColumnType("text");

                    b.Property<double>("Valeur")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("AdresseMAC")
                        .IsUnique();

                    b.HasIndex("Nom")
                        .IsUnique();

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Capteur", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Creation")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("EtatOK")
                        .HasColumnType("boolean");

                    b.Property<string>("Identifiant")
                        .HasColumnType("text");

                    b.Property<string>("Marque")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Modele")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Modification")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Identifiant")
                        .IsUnique();

                    b.ToTable("Capteurs");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.CheckList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Check_Item")
                        .HasColumnType("text");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<short>("Rang")
                        .HasColumnType("smallint");

                    b.Property<int>("programme")
                        .HasColumnType("Periodes[3]");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ContactPatient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("PatientId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("UtilisateurId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("ContactsPatients");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.JourC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateJour")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("JourTraite")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<byte[]>("NbPas")
                        .HasColumnType("bytea");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("TempsAllongeTotal")
                        .HasColumnType("bytea");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("PatientId", "DateJour")
                        .IsUnique()
                        .HasName("U_Date_J_PatientC");

                    b.ToTable("Jours");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.NuitC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("DateDebut")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("DateFin")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("DateNuit")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<byte[]>("DetailSorties")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("DureeReveilAuLit")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("DureeReveilHorsLit")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("DureeSommeil")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FCMax")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FCMin")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FCMoy")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FRMax")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FRMin")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FRMoy")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("NbReveils")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("NbSorties")
                        .HasColumnType("bytea");

                    b.Property<bool>("NuitTraitee")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("ScoreNuit")
                        .HasColumnType("bytea");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("PatientId", "DateNuit")
                        .IsUnique()
                        .HasName("U_Date_PatientC");

                    b.ToTable("Nuits");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.OAuth2Api", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("ApiName")
                        .HasColumnType("text");

                    b.Property<string>("ApiUserId")
                        .HasColumnType("text");

                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("OAuth2Apis");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ObservableC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Chambre")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("ObservableTraite")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<long>("TypeObservableId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Values")
                        .HasColumnType("bytea");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("TypeObservableId");

                    b.ToTable("Observables");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.PatientC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Chambre")
                        .HasColumnType("bytea");

                    b.Property<string>("CheminPhoto")
                        .HasColumnType("text");

                    b.Property<int?>("Coucher_h")
                        .HasColumnType("integer");

                    b.Property<int?>("Coucher_min")
                        .HasColumnType("integer");

                    b.Property<byte[]>("CumulTempsAllonge")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("DernierCoucher")
                        .HasColumnType("bytea");

                    b.Property<long?>("DernierIndiceTraite")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("DernierLever")
                        .HasColumnType("bytea");

                    b.Property<int?>("DureeMaxHorsLit_min")
                        .HasColumnType("integer");

                    b.Property<byte[]>("FreqCardiaqueMax_bpm")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FreqCardiaqueMin_bpm")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FreqRespMax_bpm")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FreqRespMin_bpm")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("Historique")
                        .HasColumnType("bytea");

                    b.Property<int?>("Lever_h")
                        .HasColumnType("integer");

                    b.Property<int?>("Lever_min")
                        .HasColumnType("integer");

                    b.Property<bool>("NouvellesDonneesHisto")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("NouvellesDonneesLit")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<byte[]>("NumCh")
                        .HasColumnType("bytea");

                    b.Property<int?>("Posture")
                        .HasColumnType("integer");

                    b.Property<int?>("TempsMaxAllongeJour")
                        .HasColumnType("integer");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("NumCh")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.TrackerC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double[]>("AccVector")
                        .HasColumnType("double precision[]");

                    b.Property<long>("CapteurId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LecturesWifi")
                        .HasColumnType("jsonb");

                    b.Property<byte[]>("NbPas")
                        .HasColumnType("bytea");

                    b.Property<double>("Power")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValueSql("0");

                    b.Property<bool>("Traite")
                        .HasColumnType("boolean");

                    b.Property<long>("key_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("nonce")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("CapteurId");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CodeRetour")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DetailRetour")
                        .HasColumnType("text");

                    b.Property<string>("URI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("URI")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.TypeObservable", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("URI")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("TypeObservable");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Appel")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("Creation")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("EmailAlarmes")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastConnection")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SMS")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobaSpace.Core.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.AlarmeC", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Capteur", "Capteur")
                        .WithMany()
                        .HasForeignKey("CapteurId");

                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("MobaSpace.Core.Data.Models.User", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ApiCapteur", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.OAuth2Api", "Api")
                        .WithMany("ApiCapteurs")
                        .HasForeignKey("ApiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobaSpace.Core.Data.Models.Capteur", "Capteur")
                        .WithMany("ApisCapteurs")
                        .HasForeignKey("CapteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.CheckList", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ContactPatient", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany("Contacts")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobaSpace.Core.Data.Models.User", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.JourC", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.NuitC", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany("Nuits")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.OAuth2Api", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany("Apis")
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ObservableC", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.PatientC", "Patient")
                        .WithMany("Observables")
                        .HasForeignKey("PatientId");

                    b.HasOne("MobaSpace.Core.Data.Models.TypeObservable", "TypeObservable")
                        .WithMany()
                        .HasForeignKey("TypeObservableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.TrackerC", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Capteur", "Capteur")
                        .WithMany()
                        .HasForeignKey("CapteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
