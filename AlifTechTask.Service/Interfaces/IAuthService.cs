namespace AlifTechTask.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenereToken(string email);
    }
}
