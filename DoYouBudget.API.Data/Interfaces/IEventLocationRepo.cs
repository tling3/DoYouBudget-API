using DoYouBudget.API.Models.Domain;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface IEventLocationRepo
    {
        Task<EventLocationModel> GetEventLocationById(int id);
        Task<(bool isSuccessful, int locationId)> InsertEventLocation(EventLocationModel domain);
    }
}
