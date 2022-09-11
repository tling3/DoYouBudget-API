using System;

namespace DoYouBudget.API.Models.Dto
{
    public class CategoryReadDto : CategoryUpdateDto
    {
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
