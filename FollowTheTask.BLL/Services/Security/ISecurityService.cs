namespace FollowTheTask.BLL.Services.Security
{
    public interface ISecurityService : IService
    {
        string GetNewSalt();

        string ApplySalt(string password, string salt);
    }
}