using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class UsersSqlRepo : BaseRepo<DoYouBudgetContext>, IUsersRepo
    {
        private readonly DoYouBudgetContext _context;
        public UsersSqlRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersModel>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
