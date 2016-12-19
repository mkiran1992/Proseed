using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EProSeed.Lib.BLL
{
    public class Inductee:IInductee
    {
        protected readonly ProDbContext db;
        public Inductee()
        {
            db = new ProDbContext();
        }


        public IList<InducteeModel> Get(int count=10,int skip=0)
        {
            return  db.Inductee.Take(count).OrderByDescending(I=>I.Id).Skip(skip).ToList();
        }


        

        public bool Create(InducteeModel Inductee)
        {
            try
            {
                db.Inductee.Add(Inductee);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public InducteeModel Get(string mailId)
        {
            return db.Inductee.Where(I => I.Email.Trim() == mailId.Trim()).Select(I => I).FirstOrDefault();
        }


        public bool Delete(int? ID)
        {
            try
            {
                if (ID == null)
                    throw new Exception("Select valid Trainee");

                var Inductee = db.Inductee.Find(ID);
                if(Inductee !=null)
                {
                    db.Inductee.Remove(Inductee);
                    if (db.SaveChanges() > 0)
                        return true;
                }
               return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public InducteeModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            try
            {
                InducteeModel batch = db.Inductee.Find(id);
                return batch;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IList<InducteeModel> InducteesByBatch(int? BatchId)
        {
            if (BatchId == null)
                throw new Exception("Select valid Trainee");
            try
            {
                var  InducteesList = db.Inductee.Where(i=>i.Batch.Id== BatchId).ToList();
                return InducteesList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool Update(InducteeModel Inductee)
        {
            try
            {
                if (Inductee != null)
                {
                    db.Entry(Inductee).State = EntityState.Modified;
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


        public IList<InducteeModel> FindByEmp(string _EmpID)
        {
            
            try
            {
                var Ind = db.Inductee.Where(I=>I.EmpId ==_EmpID);
                return Ind.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
