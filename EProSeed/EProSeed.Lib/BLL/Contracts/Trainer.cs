using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EProSeed.Lib.BLL
{
    public class Trainer : ITrainer
    {
        protected readonly ProDbContext db;
        public Trainer()
        {
            db = new ProDbContext();
        }
        /// <summary>
        /// Avoid to use it, instead use another version.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public TrainerModel Login(string Email, string Password)
        {
            try
            {
                var User = db.Tranner.Where(t => t.Email == Email && t.Password == Password).SingleOrDefault();

                if (User != null)
                {

                   return User;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TrainerModel Login(string Email, string Password, out UserType UserType)
        {
            UserType = UserType.None;
            try
            {
                var User = db.Tranner.Where(t => t.Email == Email && t.Password == Password).SingleOrDefault();

                if (User != null)
                {

                    UserType = (UserType)db.TrainerTraineeUserMapping.Where(M => User.Id == M.Map_Trainer_Id).Select(M => M.Map_UserType_Id).SingleOrDefault();
                    
                    return User;

                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Create(TrainerModel trainer)
        {
            db.Tranner.Add(trainer);
            db.SaveChanges();
            return trainer.Id;
        }

        public string GetName(int id)
        {
            try
            {
                var tranner = db.Tranner.Where(t => t.Id == id).SingleOrDefault();
                return tranner.Name;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public TrainerModel Find(int? id)
        {
            var tranner = db.Tranner.Where(t => t.Id == id).SingleOrDefault();
            return tranner;
        }

        public IList<TrainerModel> GetAll()
        {
          return   db.Tranner.ToList();
        }

    }
}
