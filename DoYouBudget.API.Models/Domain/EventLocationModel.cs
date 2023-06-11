using DoYouBudget.API.Models.Base;

namespace DoYouBudget.API.Models.Domain
{
    public class EventLocationModel : BaseDomain
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ModifiedBy { get; set; }
    }
}
