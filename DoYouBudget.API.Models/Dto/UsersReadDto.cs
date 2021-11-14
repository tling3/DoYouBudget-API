using System;

namespace DoYouBudget.API.Models.Dto
{
    public class UsersReadDto : UsersUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
