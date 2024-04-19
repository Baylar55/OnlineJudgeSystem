using Microsoft.AspNetCore.Http;

namespace AlgoCode.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
        void Delete(string fileName);
        bool IsImage(IFormFile file);
        bool CheckSize(IFormFile file, int size);
    }
}
