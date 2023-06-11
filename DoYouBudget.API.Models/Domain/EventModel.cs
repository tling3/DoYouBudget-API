using DoYouBudget.API.Models.Base;
using System;

namespace DoYouBudget.API.Models.Domain
{
    public class EventModel : BaseDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string OnlineUrl { get; set; }
        public int LocationId { get; set; }
        public string ModifiedBy { get; set; }
    }
}
