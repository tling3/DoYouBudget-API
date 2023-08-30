using DoYouBudget.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface IMonthlyLogRepo
    {
        Task<IEnumerable<MonthlyLogModel>> GetMonthlyLogsByUserIdAsync(int userId, int month);
        Task<bool> InsertMonthlyLogAsync(MonthlyLogModel domain);
        Task<MonthlyLogModel> GetMonthlyLogByIdAsync(int id);
        void UpdateMonthlyLogById(MonthlyLogModel domain);
        bool DeleteMonthlyLog(MonthlyLogModel domain);
        bool SaveChanges();
    }
}
