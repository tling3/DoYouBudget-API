using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using System;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class EventLocationRepo : BaseRepo<DoYouBudgetContext>, IEventLocationRepo
    {
        DoYouBudgetContext _context;
        public EventLocationRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EventLocationModel> GetEventLocationById(int id)
        {
            return await _context.EventLocation.FindAsync(id);
        }

        public async Task<(bool isSuccessful, int locationId)> InsertEventLocation(EventLocationModel domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            await _context.AddAsync(domain);
            bool isSuccessful = SaveChanges();
            return (isSuccessful: isSuccessful, locationId: domain.Id);
        }
    }
}
