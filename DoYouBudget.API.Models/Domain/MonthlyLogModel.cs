using DoYouBudget.API.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoYouBudget.API.Models.Domain
{
    public class MonthlyLogModel : BaseDomain
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
        public int Month { get; set; }
        public string ModifiedBy { get; set; }
        public int CategoryId { get; set; }
    }

}
