using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EProSeed.Models;
namespace EProSeed.DAL
{
    public class ProDbContext : DbContext
    {
        public ProDbContext() : base("_dbProDev")
        {

        }

        public DbSet<BatchModel> Batch { get; set; }
        public DbSet<BatchDates> BatchDates { get; set; }

        public DbSet<TrainerModel> Tranner { get; set; }

        public DbSet<InducteeModel> Inductee { get; set; }

        public DbSet<PropertyModel> Property { get; set; }

        public DbSet<FeedbackModel> Feedback { get; set; }

        public DbSet<Trainer_UserType_Map_Model> TrainerTraineeUserMapping { get; set; }

        public DbSet<TrainersFeedbackModel> TrainersFeedback { get; set; }

        //public DbSet<TrainerFeedbackQuestionModel> TrainerFeedbackQuestion { get; set; }

        //public DbSet<TrainerFeedbackQuestionResponseMappingModel> TrainerFeedbackQuestionResponseMapping { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
