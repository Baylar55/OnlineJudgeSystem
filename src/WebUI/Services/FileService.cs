﻿namespace AlgoCode.WebUI.Services;

public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";

        var path = Path.Combine(webHostEnvironment.WebRootPath, "assets/images", fileName);

        using (FileStream fileStream = new(path, FileMode.Create, FileAccess.ReadWrite))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }

    public void Delete(string fileName)
    {
        var path = Path.Combine(webHostEnvironment.WebRootPath, "assets", "images", fileName);

        if (File.Exists(path)) File.Delete(path);
    }

    public bool IsImage(IFormFile file)
    {
        if (file.ContentType.Contains("image/")) return true;

        return false;
    }

    public bool CheckSize(IFormFile file, int size)
    {
        if (file.Length / 1024 > size) return false;
        
        return true;
    }
}
