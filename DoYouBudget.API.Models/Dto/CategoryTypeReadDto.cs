using System;

namespace DoYouBudget.API.Models.Dto
{
    public class CategoryTypeReadDto : CategoryTypeUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
