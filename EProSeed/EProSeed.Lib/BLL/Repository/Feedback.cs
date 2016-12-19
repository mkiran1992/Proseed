using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL
{
    public class Feedback : IFeedback
    {
        protected readonly ProDbContext db;
        public Feedback()
        {
            db = new ProDbContext();
        }

        public bool Create(FeedbackModel Feedback)
        {
            try
            {
                db.Feedback.Add(Feedback);
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public FeedbackModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            try
            {
                FeedbackModel feedback = db.Feedback.Find(id);
                return feedback;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool Update(FeedbackModel feedback)
        {
            try
            {
                if (feedback != null)
                {
                    db.Entry(feedback).State = EntityState.Modified;
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
            if (id == null)
                throw new Exception("Invalid feedback.");
            try
            {
                FeedbackModel feedback = db.Feedback.Find(id);
                //  DeleteProperty(feedback.PropertyId);
                //feedback = db.Feedback.Find(id);
                if (feedback != null)
                {
                    db.Property.Remove(feedback.Property);
                    db.Feedback.Remove(feedback);
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


        public bool UpdateProperty(PropertyModel Property, string Date, int? feedbackID)
        {
            try
            {
                if (Property != null)
                {

                    if (Date != null)
                    {
                        var feedback = Find(feedbackID);
                        var dt = Convert.ToDateTime(Date);
                        feedback.FeedbackDate = dt;
                        db.Entry(feedback).State = EntityState.Modified;
                    }


                    db.Entry(Property).State = EntityState.Modified;
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



        public bool DeleteProperty(int? id)
        {
            if (id == null)
                throw new Exception("Invalid feedback.");
            try
            {
                var property = db.Property.Find(id);
                if (property != null)
                {
                    db.Property.Remove(property);
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

        public IList<FeedbackModel> GetFeedbacksForTrainee(int traineeID)
        {
            return db.Feedback.Where(p => p.InducteeID == traineeID).ToList();
        }
    }
}
