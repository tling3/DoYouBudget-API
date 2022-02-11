using System.ComponentModel.DataAnnotations;

namespace DoYouBudget.API.Models.Dto
{
    public class CategoryUpdateDto : CategoryInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}
