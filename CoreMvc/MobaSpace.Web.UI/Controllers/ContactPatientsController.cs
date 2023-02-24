using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;

namespace MobaSpace.Web.UI.Controllers
{
    public class ContactPatientsController : Controller
    {
        private readonly MobaDataLayer _datalayer;

        public ContactPatientsController(MobaDataLayer datalayer)
        {
            _datalayer = datalayer;
        }

        // GET: ContactPatients
        public IActionResult Index(long? patientId)
        {
            if (patientId is null)
            {
                return NotFound();
            }
            var contactsPatients = _datalayer.GetContactsPatientByPatient(patientId.Value);
            if (contactsPatients == null)
            {
                return NotFound();
            }
            ViewData["patientId"] = patientId;
            return View(contactsPatients);
        }


        // GET: ContactPatients/Create
        public async Task<IActionResult> Create(long? patientId)
        {
            var patients = SetViewDataPatientList();

            var contact = new ContactPatient();
            if(patientId.HasValue)
            {
                contact.Patient = await _datalayer.GetPatientCByIdAsync(patientId.Value);
            }
            ViewData["patientId"] = patientId;
            return View(contact);
        }

        private IEnumerable<PatientC> SetViewDataPatientList()
        {
            var patients = _datalayer.GetPatientsAsync().Result;
            ViewBag.PatientList = new SelectList(patients, "IdPatient", "NomPrenom");
            return patients;
        }

        // POST: ContactPatients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,Nom,Prenom,NumTel,Email")] ContactPatient contactPatient)
        {
            if (ModelState.IsValid)
            {
                await _datalayer.CreateContactPatientAsync(contactPatient);
                return RedirectToAction("Edit", "Patients", new { id = contactPatient.Id });
            }
            SetViewDataPatientList();
            return View(contactPatient);
        }



        // GET: ContactPatients/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPatient = _datalayer.GetContactPatient(id.Value);
            if (contactPatient == null)
            {
                return NotFound();
            }
            return View(contactPatient);
        }

        // POST: ContactPatients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PatientId,Nom,Prenom,NumTel,Email")] ContactPatient contactPatient)
        {
            if (id != contactPatient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _datalayer.UpdateContactPatientAsync(contactPatient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ContactPatientExistsAsync(contactPatient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Patients", new { id = contactPatient.Id });
            }
            SetViewDataPatientList();
            return View(contactPatient);
        }

        // GET: ContactPatients/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPatient = _datalayer.GetContactPatient(id.Value);
            if (contactPatient == null)
            {
                return NotFound();
            }

            return View(contactPatient);
        }

        // POST: ContactPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contactPatient = _datalayer.GetContactPatient(id);
            await _datalayer.DeleteContactPatientAsync(contactPatient);
            return RedirectToAction("Edit", "Patients", new { id = contactPatient.Id });
        }

        private async Task<bool> ContactPatientExistsAsync(long id)
        {
            return (await _datalayer.GetContactsPatientsAsync()).Any(e => e.Id == id);
        }
    }
}
