using System.Collections.Generic;
using System.Linq;
using EProSeed.Models;
using EProSeed.DAL;
using System;
using System.Data.Entity;

namespace EProSeed.Lib.BLL.Repository
{
    public class TrainerFeedback : ITrainerFeedback
    {
        protected readonly ProDbContext db;
        public TrainerFeedback()
        {
            db = new ProDbContext();
        }

        public bool Create(TrainersFeedbackModel model)
        {
            try
            {
                db.TrainersFeedback.Add(model);
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(TrainersFeedbackModel model)
        {
            try
            {
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public bool Delete(int? id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("Select valid Inductee");

                var feedback = db.TrainersFeedback.Find(id);
                if (feedback != null)
                {
                    db.TrainersFeedback.Remove(feedback);
                    if (db.SaveChanges() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TrainersFeedbackModel GetTrainerFeedback(int Id)
        {
            try
            {
               return db.TrainersFeedback.Find(Id);               
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public CustomTrainerFeedbackModel GetTrainerFeedbackList(int batchId)
        {
            var customTrainerFeedback = new CustomTrainerFeedbackModel();
            try
            {
                var feedbackList = db.TrainersFeedback.Where(f => f.BatchID == batchId).OrderByDescending(d => d.DateCreated);
                var batchDetail = db.Batch.Find(batchId);
                var traineeList = db.Tranner.ToList();
                customTrainerFeedback.BatchID = batchDetail.Id;
                customTrainerFeedback.BatchName = batchDetail.Name;
                customTrainerFeedback.BatchStartDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).FirstOrDefault();
                customTrainerFeedback.BatchEndDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).LastOrDefault();
                customTrainerFeedback.TrainerID = batchDetail.trainer.Id;
                customTrainerFeedback.TrainerName = batchDetail.trainer.Name;
                customTrainerFeedback.TrainerEmail = batchDetail.trainer.Email;

                var feedbackReponseList = new List<FeedbackResponse>();
                var inducteeRepo = new Inductee();

                if (feedbackList.Count() > 0) //if records exist
                {
                    foreach (var feedback in feedbackList)
                    {
                        var trainee = new Inductee();
                        string traineeName = string.Empty;
                        var feedbackReponse = new FeedbackResponse();
                        feedbackReponse.ID = feedback.ID;
                        feedbackReponse.Rating = feedback.Rating;
                        feedbackReponse.WhatWentWell = feedback.WhatWentWell;
                        feedbackReponse.DidnotGoWell = feedback.DidnotGoWell;
                        feedbackReponse.CanBeImproved = feedback.CanBeImproved;
                        feedbackReponse.TraineeID = feedback.TraineeID;
                        feedbackReponse.TraineeName = traineeList.Find(tr => tr.Id == feedback.TraineeID).Name;

                        feedbackReponseList.Add(feedbackReponse);
                    }
                }
                customTrainerFeedback.FeedbackReponse = feedbackReponseList;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customTrainerFeedback;
        }

        public CustomTrainerFeedbackModel GetTrainerFeedbackListForTrainer(int batchId, string traineeId)
        {
            var customTrainerFeedback = new CustomTrainerFeedbackModel();
            try
            {
                var feedbackList = db.TrainersFeedback.Where(f => f.BatchID == batchId && f.TraineeID.ToString() == traineeId).OrderByDescending(d => d.DateCreated).ToList();
                var batchDetail = db.Batch.Find(batchId);

                customTrainerFeedback.BatchID = batchDetail.Id;
                customTrainerFeedback.BatchName = batchDetail.Name;
                customTrainerFeedback.BatchStartDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).FirstOrDefault();
                customTrainerFeedback.BatchEndDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).LastOrDefault();
                customTrainerFeedback.TrainerID = batchDetail.trainer.Id;
                customTrainerFeedback.TrainerName = batchDetail.trainer.Name;
                customTrainerFeedback.TrainerEmail = batchDetail.trainer.Email;

                var feedbackReponseList = new List<FeedbackResponse>();
                var inducteeRepo = new Inductee();

                if (feedbackList.Count() > 0) //if records exist
                {
                    foreach (var feedback in feedbackList)
                    {
                        var trainee = new Inductee();

                        var feedbackReponse = new FeedbackResponse();
                        feedbackReponse.ID = feedback.ID;
                        feedbackReponse.Rating = feedback.Rating;
                        feedbackReponse.WhatWentWell = feedback.WhatWentWell;
                        feedbackReponse.DidnotGoWell = feedback.DidnotGoWell;
                        feedbackReponse.CanBeImproved = feedback.CanBeImproved;
                        feedbackReponse.TraineeID = feedback.TraineeID;
                        feedbackReponse.TraineeName = db.Tranner.Find(feedback.TraineeID).Name;

                        feedbackReponseList.Add(feedbackReponse);
                    }
                }
                customTrainerFeedback.FeedbackReponse = feedbackReponseList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customTrainerFeedback;
        }

        public IList<TrainerModel> GetTrainerList()
        {
            return db.Tranner.OrderByDescending(t => t.Name).ToList();
        }
    }
}
