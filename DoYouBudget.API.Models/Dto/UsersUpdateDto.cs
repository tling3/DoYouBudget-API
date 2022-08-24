using System.ComponentModel.DataAnnotations;

namespace DoYouBudget.API.Models.Dto
{
    public class UsersUpdateDto : UsersInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}
