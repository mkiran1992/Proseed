﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EProSeed.Lib.BLL;
using EProSeed.Models;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class BatchController : Controller
    {
        //
        // GET: /Batch/

        protected readonly IBatch BatchRepo;
        protected readonly ITrainer TrainerRepo;

        public BatchController()
        {
            BatchRepo = new Batch();
            TrainerRepo = new Trainer();
        }
        public ActionResult Index()
        {
            var _BatchList = BatchRepo.GetAll();

            return View(_BatchList);
        }


        public ActionResult Create()
        {
            var TrainerList = TrainerRepo.GetAll();
            ViewBag.Trainers = new SelectList(TrainerList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.vmBatch batchModel)
        {
            var batch = new BatchModel
            {
                BatchDates = batchModel.BatchDates == null ? new List<BatchDates>() : batchModel.BatchDates.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => new BatchDates() { BatchDate = DateTime.ParseExact(p, "MM/dd/yyyy", null) }).OrderBy(p => p.BatchDate).ToList(),
                Id = batchModel.Id,
                Name = batchModel.Name,
            };
            try
            {
                batch.TrainerId = Convert.ToInt32(User.Identity.Name);
                if (ModelState.IsValid)
                {

                    if (BatchRepo.Create(batch) == true)
                    {
                        ViewData["SuccessMsg"] = "Batch created successfully.";
                        return Redirect("/");
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Failed to create batch.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = "Failed to create batch.";
            }
            var TrainerList = TrainerRepo.GetAll();
            ViewBag.Trainers = new SelectList(TrainerList, "Id", "Name");
            var vmBatch = new Models.vmBatch
            {
                //BatchDates = batch.BatchDates.Select(p=> DateTime.ParseExact(p, "mm/dd/yyyy", null)).
                Id = batch.Id,
                Name = batch.Name,
                trainer = batch.trainer,
                TrainerId = batch.TrainerId
            };
            return View(vmBatch);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var Batch = BatchRepo.Find(id);
            if (Batch == null)
                return HttpNotFound();
            var TrainerList = TrainerRepo.GetAll();
            ViewBag.Trainers = new SelectList(TrainerList, "Id", "Name", Batch.TrainerId);
            var vmBatches = new Models.vmBatch
            {
                BatchDates = string.Join(",", Batch.BatchDates.Select(p => p.BatchDate)),
                Id = Batch.Id,
                Name = Batch.Name,
            };
            return View(vmBatches);
        }

        [HttpPost]
        public ActionResult Edit(BatchModel batch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (BatchRepo.Update(batch) == true)
                    {
                        ViewData["SuccessMsg"] = "Batch updated successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Failed to update batch.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = "Failed to update batch.";
            }
            var TrainerList = TrainerRepo.GetAll();
            ViewBag.Trainers = new SelectList(TrainerList, "Id", "Name", batch.TrainerId);
            return Redirect("/batch");
            //  return View(batch);
        }


        public ActionResult Delete(int? id)
        {
            try
            {
                BatchRepo.DeleteConfirmed(id);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = "Failed to delete batch.";
            }
            return Redirect("/batch");
        }



    }
}
