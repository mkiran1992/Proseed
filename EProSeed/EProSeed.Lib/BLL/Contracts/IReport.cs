using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL.Contracts
{
    public interface IReport
    {
        int CountInductees(int batchId);

        float BatchAverage(int batchId);

        string TrainerName(int batchId);      
    }
}
