using System;

namespace DoYouBudget.API.Models.Domain
{
    public class CategoryTypeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
