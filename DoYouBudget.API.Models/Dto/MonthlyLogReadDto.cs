using System;

namespace DoYouBudget.API.Models.Dto
{
    public class MonthlyLogReadDto : MonthlyLogUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
