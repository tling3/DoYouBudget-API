using System;

namespace DoYouBudget.API.Models.Dto
{
    public class EventReadDto : EventUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
