using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<MonthlyLogModel>> GetMonthlyLogsByUserId(int userId, int month)
        {
            IEnumerable<MonthlyLogModel> domain = await _context.MonthlyLog.Where(item => item.UserId == userId && item.Month == month).ToListAsync();
            return domain;
        }

        public async Task<MonthlyLogModel> GetMonthlyLogById(int id)
        {
            var domain = await _context.MonthlyLog.FindAsync(id);
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

        public void UpdateMonthlyLogById(MonthlyLogModel domain)
        {
            // intentionally left blank
        }

        public bool DeleteMonthlyLog(MonthlyLogModel domain)
        {
            _context.MonthlyLog.Remove(domain);
            bool isSuccessful = SaveChanges();
            return isSuccessful;
        }
    }
}
