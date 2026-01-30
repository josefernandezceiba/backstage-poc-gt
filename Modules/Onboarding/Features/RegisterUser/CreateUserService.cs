using MoMo.Common.Ports;
using MoMo.Modules.Onboarding.Core;
using MoMo.Modules.Onboarding.Core.Ports;


namespace MoMo.Modules.Onboarding.Features.RegisterUser
{
    public class CreateUserService(IUserRepository _repository, IUnitOfWork _unitOfWork)
    {
        public async Task RegisterUser(User user,  CancellationToken token)
        {            
            if (!await _repository.IsUserAlreadyPresent(user.Nid))
            {
                await _repository.SaveUser(user);
                await _unitOfWork.SaveAsync(token);
            }
        }
    }
}
