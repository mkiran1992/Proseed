using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL
{
    public interface ITrainerFeedback
    {
        //  IList<TrainerFeedbackQuestionModel> GetTrainerFeedbackQuestions();
        IList<TrainerModel> GetTrainerList();
        CustomTrainerFeedbackModel GetTrainerFeedbackList(int batchId);
        CustomTrainerFeedbackModel GetTrainerFeedbackListForTrainer(int batchId, string traineeId);
        // IList<TrainerFeedbackQuestionModel> GetFeedbackQuestions();   
        TrainersFeedbackModel GetTrainerFeedback(int Id);

        bool Create(TrainersFeedbackModel model);
        bool Update(TrainersFeedbackModel model);
        bool Delete(int? ID);

    }
}
