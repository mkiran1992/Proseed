using System;
using System.Collections.Generic;
using System.Linq;
using EProSeed.DAL;
using EProSeed.Models;
using System.Data.Entity;

namespace EProSeed.Lib.BLL
{
    public class Batch : IBatch
    {
        protected readonly ProDbContext db;
        protected readonly IInductee _Inductee;
        public Batch()
        {
            db = new ProDbContext();
            _Inductee = new Inductee();
        }

        public IList<BatchModel> GetAll()
        {
            try
            {
                return db.Batch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<BatchModel> GetAllForTrainee(string traineeId)
        {
            try
            {
                var inductee = db.Inductee.FirstOrDefault(i => i.Id.ToString() == traineeId);
                return db.Batch.Where(b => b.Id == inductee.BatchID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BatchModel GetBatchDetailsByTraineeId(string traineeId)
        {
            int traineeIntId = Convert.ToInt32(traineeId);
            var user = db.Tranner.FirstOrDefault(us=>us.Id== traineeIntId);

            var inducteeBatchId = _Inductee.Get(user.Email).BatchID;
            var traineesBatch = db.Batch.Where(B => B.Id == inducteeBatchId).Select(B => B).ToList().FirstOrDefault();
            try
            {
                var batchDetails = (from inductee in db.Inductee
                                 join  trainee in db.Tranner on inductee.Email equals trainee.Email
                                 join batch in db.Batch on inductee.BatchID equals batch.Id
                                 select batch).FirstOrDefault();
                batchDetails.trainer = traineesBatch.trainer;

                return batchDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(BatchModel _batch)
        {
            try
            {
                var batch=db.Batch.SingleOrDefault(b => b.Name == _batch.Name.Trim());
                if (batch != null)
                    return false;
                db.Batch.Add(_batch);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool Edit(BatchModel batch)
        {
            try
            {
                if (batch != null)
                {
                    db.Entry(batch).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }


        public BatchModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            try
            {
                return db.Batch.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(BatchModel batch)
        {
            try
            {
                if (batch != null)
                {
                    db.Entry(batch).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                        return true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public void DeleteConfirmed(int? id)
        {
            if (id == null)
                throw new Exception("Select valid batch");
            try
            {
                BatchModel batch = db.Batch.Find(id);
                if (batch != null)
                {
                    db.Batch.Remove(batch);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<BatchModel> FindByName(string Name)
        {
            try
            {
                return db.Batch.Where(b => b.Name == Name).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
