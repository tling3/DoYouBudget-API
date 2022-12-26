using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class MonthlyLogRepo : BaseRepo<DoYouBudgetContext>, IMonthlyLogRepo
    {
        private readonly DoYouBudgetContext _context;

        public MonthlyLogRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<MonthlyLogModel> GetMonthlyLogsByUserId(int userId, int month)
        {
            IEnumerable<MonthlyLogModel> domain = _context.MonthlyLog.Where(item => item.UserId == userId && item.Month == month).ToList();
            return domain;
        }

        public async Task<bool> InsertMonthlyLog(MonthlyLogModel domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            await _context.AddAsync(domain);
            bool isSuccessful = SaveChanges();
            return isSuccessful;
        }
    }
}
