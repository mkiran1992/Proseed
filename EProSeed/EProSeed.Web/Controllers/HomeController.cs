using System;
using System.Web.Mvc;
using EProSeed.Lib.BLL;
using EProSeed.Models;
using System.Linq;
using EProSeed.Web.Models;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly IBatch _Batch;
        protected readonly IInductee _Inductee;

        public HomeController()
        {
            _Batch = new Batch();
            _Inductee = new Inductee();

        }

        public ActionResult Index()
        {
            vmDashBoard objDashboard = new vmDashBoard();
            var batch = _Batch.GetAll();
            if (batch != null && batch.Any())
            {
                UserType CurrentUserType;
                Enum.TryParse<EProSeed.Lib.BLL.UserType>(Session["UserType"].ToString(), out CurrentUserType);
                string CurrentUserEmail = Session["UserEmailId"].ToString();

                GetDataBasedOnUserType(objDashboard, batch, CurrentUserType, CurrentUserEmail);
            }
            return View(objDashboard);
        }

        private void GetDataBasedOnUserType(vmDashBoard objDashboard, System.Collections.Generic.IList<BatchModel> batch, UserType CurrentUserType, string CurrentUserEmail)
        {
                if (CurrentUserType == UserType.Trainer)
                {
                    objDashboard.BatchList = batch;
                    var LastBatchID = batch.LastOrDefault().Id;
                    var IndList = _Inductee.Get(20, 0);
                    objDashboard.InducteeList = IndList.Where(i => i.BatchID == LastBatchID).OrderByDescending(i => i.Batch.BatchDates.OrderBy(f => f.BatchDate).First()).ToList();
                }
                else if (CurrentUserType == UserType.Trainee)
                {
                    var inducteeBatchId = _Inductee.Get(CurrentUserEmail).BatchID;
                    var traineesBatch = batch.Where(B => B.Id == inducteeBatchId).Select(B => B).ToList<BatchModel>();
                    if (traineesBatch.Any())
                    {
                        objDashboard.BatchList = traineesBatch;
                        var IndList = _Inductee.Get(20, 0);
                        objDashboard.InducteeList = IndList.Where(i => i.BatchID == inducteeBatchId).OrderByDescending(i => i.Id).ToList();
                    }
                }
        }
    }
}
