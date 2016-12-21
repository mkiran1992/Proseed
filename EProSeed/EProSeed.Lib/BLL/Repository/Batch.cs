using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProSeed.DAL;
using EProSeed.Models;
using System.Data.Entity;

namespace EProSeed.Lib.BLL
{
    public class Batch : IBatch
    {
        protected readonly ProDbContext db;
        public Batch()
        {
            db = new ProDbContext();
        }

        public IList<BatchModel> GetAll()
        {
            try
            {
                var Batch = db.Batch.ToList();
                return Batch;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<BatchModel> GetAllForTrainee(string traineeId)
        {
            try
            {
                var inductee = db.Inductee.FirstOrDefault(i => i.Id.ToString() == traineeId);
                var batch = db.Batch.Where(b => b.Id == inductee.BatchID).ToList();
                return batch;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Batch Details
        /// </summary>
        /// <param name="traineeId"></param>
        /// <returns>BatchModel</returns>
        public BatchModel GetBatchDetailsByTraineeId(string traineeId)
        {
            try
            {
                var batchDetails = (from inductee in db.Inductee
                                 join  trainee in db.Tranner on inductee.Email equals trainee.Email
                                 join batch in db.Batch on inductee.BatchID equals batch.Id
                                 select batch).FirstOrDefault();

                return batchDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Create(BatchModel _batch)
        {
            try
            {
                db.Batch.Add(_batch);
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public bool Edit(BatchModel batch)
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
                throw new Exception(ex.Message);
            }
            return false;
        }


        public BatchModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            try
            {
                BatchModel batch = db.Batch.Find(id);
                return batch;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }


        public IList<BatchModel> FindByName(string Name)
        {
            try
            {
                var Batch = db.Batch.Where(b => b.Name == Name);
                return Batch.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
