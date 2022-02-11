namespace DoYouBudget.API.Models.Dto
{
    public class CategoryInsertDto
    {
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Budget { get; set; }
        public string ModifiedBy { get; set; }
    }
}
