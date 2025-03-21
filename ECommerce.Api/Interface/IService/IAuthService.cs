namespace ECommerce.Api.Interface.IService
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string username, string password);
    }
}
