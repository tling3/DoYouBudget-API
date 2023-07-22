using DoYouBudget.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface IMonthlyLogRepo
    {
        Task<IEnumerable<MonthlyLogModel>> GetMonthlyLogsByUserId(int userId, int month);
        Task<bool> InsertMonthlyLog(MonthlyLogModel domain);
        Task<MonthlyLogModel> GetMonthlyLogById(int id);
        void UpdateMonthlyLogById(MonthlyLogModel domain);
        bool DeleteMonthlyLog(MonthlyLogModel domain);
        bool SaveChanges();
    }
}
