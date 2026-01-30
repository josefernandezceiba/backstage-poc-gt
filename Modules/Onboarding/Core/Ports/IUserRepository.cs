namespace MoMo.Modules.Onboarding.Core.Ports
{
    public interface IUserRepository
    {
        public Task SaveUser(User user);

        public Task<bool> IsUserAlreadyPresent(string  nid);
    }
}
