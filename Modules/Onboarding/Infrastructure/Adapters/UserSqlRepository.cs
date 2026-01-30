using Microsoft.EntityFrameworkCore;
using MoMo.Common.DataAccess;
using MoMo.Modules.Onboarding.Core;
using MoMo.Modules.Onboarding.Core.Ports;

namespace MoMo.Modules.Onboarding.Infrastructure.Adapters
{
    public class UserSqlRepository(ModuleContext _context) : IUserRepository
    {
        public async Task<bool> IsUserAlreadyPresent(string nid)
        {
            var userSet =_context.Set<User>();
            return await userSet.AnyAsync(x => x.Nid.Equals(nid));            
        }

        public async Task SaveUser(User user) =>  await _context.Set<User>().AddAsync(user);
        
    }
}
