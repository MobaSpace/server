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
    [Migration("20210104142409_Modification Table balance")]
    partial class ModificationTablebalance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("mobaspace_data")
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

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Alarme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Acquittement")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("CapteurId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Creation")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("NbNotifications")
                        .HasColumnType("integer");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<int>("Priorite")
                        .HasColumnType("integer");

                    b.Property<string>("UtilisateurId")
                        .HasColumnType("text");

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

                    b.ToTable("Capteurs");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ContactPatient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<string>("UtilisateurId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("ContactsPatients");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Jour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateJour")
                        .HasColumnType("date");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<int?>("TempsAllongeTotal")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PatientId", "DateJour")
                        .IsUnique()
                        .HasName("U_Date_J_Patient");

                    b.ToTable("Jours");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Nuit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DateDebut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateNuit")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("DureeReveilAuLit")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("DureeReveilHorsLit")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("DureeSommeil")
                        .HasColumnType("interval");

                    b.Property<int?>("FCMax")
                        .HasColumnType("integer");

                    b.Property<int?>("FCMin")
                        .HasColumnType("integer");

                    b.Property<int?>("FCMoy")
                        .HasColumnType("integer");

                    b.Property<int?>("FRMax")
                        .HasColumnType("integer");

                    b.Property<int?>("FRMin")
                        .HasColumnType("integer");

                    b.Property<int?>("FRMoy")
                        .HasColumnType("integer");

                    b.Property<int?>("NbReveils")
                        .HasColumnType("integer");

                    b.Property<int?>("NbSorties")
                        .HasColumnType("integer");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<int?>("ScoreNuit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PatientId", "DateNuit")
                        .IsUnique()
                        .HasName("U_Date_Patient");

                    b.ToTable("Nuits");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.OAuth2Api", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApiName")
                        .HasColumnType("text");

                    b.Property<string>("ApiUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("OAuth2Apis");
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Chambre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CheminPhoto")
                        .HasColumnType("text");

                    b.Property<int?>("Coucher_h")
                        .HasColumnType("integer");

                    b.Property<int?>("Coucher_min")
                        .HasColumnType("integer");

                    b.Property<int?>("CumulTempsAllonge")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DernierCoucher")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("DernierIndiceTraite")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DernierLever")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DureeMaxHorsLit_min")
                        .HasColumnType("integer");

                    b.Property<int?>("FreqCardiaqueMax_bpm")
                        .HasColumnType("integer");

                    b.Property<int?>("FreqCardiaqueMin_bpm")
                        .HasColumnType("integer");

                    b.Property<int?>("FreqRespMax_bpm")
                        .HasColumnType("integer");

                    b.Property<int?>("FreqRespMin_bpm")
                        .HasColumnType("integer");

                    b.Property<string>("Historique")
                        .HasColumnType("jsonb");

                    b.Property<int?>("Lever_h")
                        .HasColumnType("integer");

                    b.Property<int?>("Lever_min")
                        .HasColumnType("integer");

                    b.Property<bool>("NouvellesDonneesHisto")
                        .HasColumnType("boolean");

                    b.Property<bool>("NouvellesDonneesLit")
                        .HasColumnType("boolean");

                    b.Property<int?>("Posture")
                        .HasColumnType("integer");

                    b.Property<int?>("TempsMaxAllongeJour")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Patients");
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

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Alarme", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Capteur", "Capteur")
                        .WithMany()
                        .HasForeignKey("CapteurId");

                    b.HasOne("MobaSpace.Core.Data.Models.Patient", "Patient")
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

            modelBuilder.Entity("MobaSpace.Core.Data.Models.ContactPatient", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Patient", "Patient")
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

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Jour", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.Nuit", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Patient", "Patient")
                        .WithMany("Nuits")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobaSpace.Core.Data.Models.OAuth2Api", b =>
                {
                    b.HasOne("MobaSpace.Core.Data.Models.Patient", "Patient")
                        .WithMany("Apis")
                        .HasForeignKey("PatientId");
                });
#pragma warning restore 612, 618
        }
    }
}
