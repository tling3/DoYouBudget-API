using System.ComponentModel.DataAnnotations;

namespace DoYouBudget.API.Models.Dto
{
    public class CategoryTypeUpdateDto : CategoryTypeInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}
