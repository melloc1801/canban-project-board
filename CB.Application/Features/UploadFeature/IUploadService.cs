namespace CB.Application.Features.UploadFeature;

public interface IUploadService
{
    Task<string> Upload(string path, IFormFile file);
}