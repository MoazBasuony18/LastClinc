using LastClinc.Models;
using LastClinc.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Controllers
{
    [Authorize]
    public class PatientDataController : Controller
    {
        private readonly IClincRepository<PatientData> patientDataRepository;

        public PatientDataController(IClincRepository<PatientData> PatientDataRepository)
        {
            patientDataRepository = PatientDataRepository;
        }
        // GET: PatientDataController
        public ActionResult Index()
        {
           var patientDatas=patientDataRepository.List();
            return View(patientDatas);
        }

        // GET: PatientDataController/Details/5
        public ActionResult Details(int id)
        {
            var patientDatas = patientDataRepository.FindById(id);
            return View(patientDatas);
        }

        // GET: PatientDataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientDataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientData patient)
        {
            try
            {
                patientDataRepository.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientDataController/Edit/5
        public ActionResult Edit(int id)
        {
            var patientDatas = patientDataRepository.FindById(id);
            return View(patientDatas);
        }

        // POST: PatientDataController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientData patient)
        {
            try
            {
                patientDataRepository.Update(id, patient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientDataController/Delete/5
        public ActionResult Delete(int id)
        {
            var patientDatas = patientDataRepository.FindById(id);
            return View(patientDatas);
        }

        // POST: PatientDataController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                patientDataRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
