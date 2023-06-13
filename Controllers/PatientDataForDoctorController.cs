using LastClinc.Models;
using LastClinc.Models.Repositories;
using LastClinc.ViewModels;
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
    public class PatientDataForDoctorController : Controller
    {
        private readonly IClincRepository<PatientData> patientDataRepository;
        private readonly IClincRepository<PatientDataForDoctor> patientDataForDoctorRepository;

        public PatientDataForDoctorController(IClincRepository<PatientData> patientDataRepository,IClincRepository<PatientDataForDoctor> patientDataForDoctorRepository )
        {
            this.patientDataRepository = patientDataRepository;
            this.patientDataForDoctorRepository = patientDataForDoctorRepository;
        }
        public ActionResult Index()
        {
            var patientDatas = patientDataForDoctorRepository.List();
            return View(patientDatas);
        }

        // GET: PatientDataForDoctorController/Details/5
        public ActionResult Details(int id)
        {
            var patientDatas = patientDataForDoctorRepository.FindById(id);
            return View(patientDatas);
        }

        // GET: PatientDataForDoctorController/Create
        public ActionResult Create()
        {
            var model = new PatientDataDoctorViewModel
            {
                PatientDatas = FillSelectList()
            };
            return View(model);
        }

        // POST: PatientDataForDoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientDataDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.PatientId == -1)
                    {
                        ViewBag.Message = "من فضلك قم باختيار مريض";
                        var vmodel = new PatientDataDoctorViewModel
                        {
                            PatientDatas = FillSelectList()
                        };
                        return View(vmodel);
                    }
                    var patientDatas = patientDataRepository.FindById(model.PatientId);
                    PatientDataForDoctor patientDataForDoctor = new PatientDataForDoctor
                    {
                        Id = model.PatientDoctorId,
                        Complain = model.Complain,
                        Diagnosis = model.Diagnosis,
                        Exmination = model.Exmination,
                        Investigations = model.Investigations,
                        Medicine = model.Medicine,
                        PastHistory = model.PastHistory,
                        Treatment = model.Treatment,
                        PatientData = patientDatas
                    };
                    patientDataForDoctorRepository.Add(patientDataForDoctor);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError(string.Empty, "عليك اكمال جميع البيانات");
            return View(GetAllPatient());
        }

        // GET: PatientDataForDoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            var patientForDoctor = patientDataForDoctorRepository.FindById(id);
            var patientId = patientForDoctor.PatientData == null ? patientForDoctor.PatientData.Id = 0 : patientForDoctor.PatientData.Id;
            var viewModel = new PatientDataDoctorViewModel
            {
                PatientDoctorId = patientForDoctor.Id,
                Complain = patientForDoctor.Complain,
                Diagnosis = patientForDoctor.Diagnosis,
                Exmination = patientForDoctor.Exmination,
                Investigations = patientForDoctor.Investigations,
                Medicine = patientForDoctor.Medicine,
                PastHistory = patientForDoctor.PastHistory,
                Treatment = patientForDoctor.Treatment,
                PatientId = patientId,
                PatientDatas = patientDataRepository.List().ToList()
            };
            return View(viewModel);
        }

        // POST: PatientDataForDoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientDataDoctorViewModel model)
        {
            try
            {
                var patientData = patientDataRepository.FindById(model.PatientId);
                PatientDataForDoctor patientDataForDoctor = new PatientDataForDoctor
                {
                    Id = model.PatientDoctorId,
                    Complain = model.Complain,
                    Diagnosis = model.Diagnosis,
                    Exmination = model.Exmination,
                    Investigations = model.Investigations,
                    Medicine = model.Medicine,
                    PastHistory = model.PastHistory,
                    Treatment = model.Treatment,
                    PatientData = patientData
                };
                patientDataForDoctorRepository.Delete(id);
                patientDataForDoctorRepository.Update(model.PatientDoctorId, patientDataForDoctor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientDataForDoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            var patientDatas = patientDataForDoctorRepository.FindById(id);
            return View(patientDatas);
        }

        // POST: PatientDataForDoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                patientDataForDoctorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string searchString)
        {
            var result = patientDataForDoctorRepository.Search(searchString);
            return View("Index", result);
        }
        List<PatientData> FillSelectList()
        {
            var patientDatas = patientDataRepository.List().ToList();
            patientDatas.Insert(0, new PatientData { Id = -1, PatientName = "-----  من فضلك قم باختيار اسم المريض -----" });
            return patientDatas;
        }
        PatientDataDoctorViewModel GetAllPatient()
        {
            var vmodel = new PatientDataDoctorViewModel
            {
                PatientDatas = FillSelectList()
            };
            return vmodel;
        }
    }
}
