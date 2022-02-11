using System;

namespace DoYouBudget.API.Models.Dto
{
    public class CategoryReadDto : CategoryUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
