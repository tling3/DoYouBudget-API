using System;

namespace DoYouBudget.API.Models.Dto
{
    public class MonthlyLogInsertDto
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
        public int Month { get; set; }
        public string ModifiedBy { get; set; }
    }
}
