using DoYouBudget.API.Models.Base;
using System;

namespace DoYouBudget.API.Models.Domain
{
    public class CategoryModel : BaseDomain
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Budget { get; set; }
        public int TypeId { get; set; }
        public DateTime PostDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
