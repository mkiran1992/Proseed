using EProSeed.DAL;
using EProSeed.Lib.BLL.Contracts;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL.Repository
{
    public class Report : IReport
    {
        protected readonly ProDbContext db;

        public Report()
        {
            db = new ProDbContext();
        }

        public string TrainerName(int batchId)
        {
            try
            {
                int trainerId = db.Batch.Where(B => B.Id == batchId).Select(T => T.TrainerId).SingleOrDefault().HasValue ? db.Batch.Where(B => B.Id == batchId).Select(T => T.TrainerId).SingleOrDefault().Value : 0;
                string TrainerName = db.Tranner.Where(t => t.Id == trainerId).Select(n => n.Name).SingleOrDefault();
                return TrainerName;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        public int CountInductees(int batchId)
        {
            try
            {
                int count = db.Inductee.Where(m => m.BatchID == batchId).Count();
                return count;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public float BatchAverage(int batchId)
        {
            float avg = 0;
            int property = 0;
            List<int> propertyInductee = new List<int>();
            List<float> sumOfInducteeRating = new List<float>();

            try
            {
                List<int> countInductee = db.Inductee.Where(m => m.BatchID == batchId).Select(m => m.Id).ToList();

                if(countInductee.Count != 0)
                {
                    foreach (int item in countInductee)
                    {
                        property = db.Feedback.Distinct().Where(m => m.InducteeID == item).Select(m => m.PropertyId).FirstOrDefault();
                        propertyInductee.Add(property);
                    }
                }    
                          
                if(propertyInductee.Count!=0)
                {
                    foreach (int inductee in propertyInductee)
                    {
                        PropertyModel Properties = db.Property.Where(P => P.ID == inductee).Select(P => P).SingleOrDefault();
                        int sumforInductee = Properties.PassionForClientSuccessRating + Properties.FocusOnQualityRating + Properties.CommunicationRating + Properties.TransparencyRating + Properties.OwnerShipRating + Properties.TeamPlayerRating + Properties.CommitmentRating + Properties.DisciplineRating + Properties.EnergyRating + Properties.TechnicalCompetencyRating;
                        int noOfDays = db.BatchDates.Where(d => d.BatchID == batchId).Count();
                        float avgPerInductee = ((float)sumforInductee /(noOfDays));
                        sumOfInducteeRating.Add(avgPerInductee);
                    }
                }  
                            
                if(sumOfInducteeRating.Count!=0)
                {
                    float finalSum = sumOfInducteeRating.Sum();
                    avg = finalSum / countInductee.Count;
                }
               
                return avg;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }
    }
}
