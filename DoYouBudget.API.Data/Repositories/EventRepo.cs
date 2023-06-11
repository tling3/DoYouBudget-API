using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class EventRepo : BaseRepo<DoYouBudgetContext>, IEventRepo
    {
        DoYouBudgetContext _context;
        public EventRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventModel>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<EventModel> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<bool> InsertEvent(EventModel domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            await _context.AddAsync(domain);
            bool isSuccessful = SaveChanges();
            return isSuccessful;
        }
    }
}
