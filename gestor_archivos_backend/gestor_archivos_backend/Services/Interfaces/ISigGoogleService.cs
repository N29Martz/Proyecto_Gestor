
namespace gestor_archivos_backend.Services.Interfaces
{
    public interface ISigGoogleService
    {
        Task<string> SignAsync(string imageUrl, TimeSpan expiration);
        Task<string> SignAsync(string imageUrl);
    }
}
