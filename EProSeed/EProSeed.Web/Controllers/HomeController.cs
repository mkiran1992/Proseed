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
            if (batch != null && batch.Count() > 0)
            {
                try
                {
                    int CurrentUserId = Convert.ToInt32(Session["UserId"]);
                    EProSeed.Lib.BLL.UserType CurrentUserType = UserType.None;
                    bool GotCurrentUser = Enum.TryParse<EProSeed.Lib.BLL.UserType>(Session["UserType"].ToString(), out CurrentUserType);
                    string CurrentUserEmail = Session["UserEmailId"].ToString();

                    if (GotCurrentUser)
                    {
                        if (CurrentUserType == UserType.Trainer)
                        {
                            objDashboard.BatchList = batch;
                            var LastBatchID = batch.LastOrDefault().Id;
                            var IndList = _Inductee.Get(20, 0);
                            objDashboard.InducteeList = IndList.Where(i => i.BatchID == LastBatchID).OrderByDescending(i => i.Batch.BatchDates.OrderBy(f=>f.BatchDate).First()).ToList();
                        }
                        else if (CurrentUserType == UserType.Trainee)
                        {
                            var inducteeBatchId = _Inductee.Get(CurrentUserEmail).BatchID;
                            var traineesBatch = batch.Where(B => B.Id == inducteeBatchId).Select(B => B).ToList<BatchModel>();
                            if (traineesBatch.Count() > 0)
                            {
                                objDashboard.BatchList = traineesBatch;
                                var LastBatchID = batch.LastOrDefault().Id;
                                var IndList = _Inductee.Get(20, 0);
                                objDashboard.InducteeList = IndList.Where(i => i.BatchID == inducteeBatchId).OrderByDescending(i => i.Id).ToList();
                            }
                        }
                    }
                    else
                    {

                        //objDashboard.BatchList = batch;
                        //var LastBatchID = batch.LastOrDefault().Id;
                        //var IndList = _Inductee.Get(20, 0);
                        //objDashboard.InducteeList = IndList.Where(i => i.BatchID == LastBatchID).OrderByDescending(i => i.Id).ToList();
                    }
                }
                catch (Exception ex)
                {
                    // handle here
                }
            }



            return View(objDashboard);
        }

    }
}
