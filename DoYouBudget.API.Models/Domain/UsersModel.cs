using DoYouBudget.API.Models.Base;

namespace DoYouBudget.API.Models.Domain
{
    public class UsersModel : BaseDomain
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ModifiedBy { get; set; }
    }
}
