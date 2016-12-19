using EProSeed.Lib.BLL;
using EProSeed.Lib.BLL.Contracts;
using EProSeed.Lib.BLL.Repository;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProSeed.Web.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        protected readonly IBatch BatchRepo;
        protected readonly IReport ReportRepo;

        public ReportController()
        {
            BatchRepo = new Batch();
            ReportRepo = new Report();
        }

        public ActionResult Index()
        {
            var batchList = BatchRepo.GetAll();
            ViewBag.Batch = new SelectList(batchList, "Id", "Name");
            return View("Report");
        }

        [HttpGet]
        public ActionResult ViewReport(int batchId)
        {
            var report = new ReportModel();
            report.TrainerName = ReportRepo.TrainerName(batchId);
            report.NumberofInductees = ReportRepo.CountInductees(batchId);
            report.BatchAverage = ReportRepo.BatchAverage(batchId);
            return PartialView("_ReportPartial", report);
        }
    }
}
