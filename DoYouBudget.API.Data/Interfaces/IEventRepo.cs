using DoYouBudget.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface IEventRepo
    {
        Task<IEnumerable<EventModel>> GetEvents();
        Task<EventModel> GetEventById(int id);
        Task<bool> InsertEvent(EventModel domain);
    }
}
