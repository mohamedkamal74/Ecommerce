using Microsoft.AspNetCore.Http;

namespace Ecommerce.core.Services
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImageasync(IFormFileCollection files, string src);
        void DeleteImageAsync(string src);
    }
}
