using System.ComponentModel.DataAnnotations;

namespace DoYouBudget.API.Models.Dto
{
    public class MonthlyLogUpdateDto : MonthlyLogInsertDto
    {
        [Key]
        public int Id { get; set; }

    }
}
