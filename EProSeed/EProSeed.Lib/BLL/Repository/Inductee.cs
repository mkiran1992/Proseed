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
                return db.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
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
                    return db.SaveChanges() > 0;
                }
               return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public InducteeModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            try
            {
                return db.Inductee.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<InducteeModel> InducteesByBatch(int? batchId)
        {
            if (batchId == null)
                throw new Exception("Select valid Trainee");
            try
            {
                return db.Inductee.Where(i=>i.Batch.Id== batchId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(InducteeModel Inductee)
        {
            try
            {
                if (Inductee != null)
                {
                    db.Entry(Inductee).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }


        public IList<InducteeModel> FindByEmp(string _EmpID)
        {
            
            try
            {
                return db.Inductee.Where(I=>I.EmpId ==_EmpID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public InducteeModel FindByEmail(string _email)
        {
            try
            {
               return db.Inductee.Where(I => I.Email == _email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
