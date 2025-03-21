namespace ECommerce.Api.Interface.IService
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordHash, string password);
    }
}
