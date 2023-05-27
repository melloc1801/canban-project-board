using Microsoft.Net.Http.Headers;

namespace CB.Application.Features.UploadFeature;

public class UploadService: IUploadService
{
    public async Task<string> Upload(string path, IFormFile file)
    {
        var localPathToSave = Path.Combine("wwwroot", path);
        Directory.CreateDirectory(localPathToSave);

        var filename = ContentDispositionHeaderValue
            .Parse(file.ContentDisposition)
            .FileName
            .ToString()
            .Trim('"');
        var fullPath = Path.Combine(localPathToSave, filename);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return Path.Combine(path, filename);
    }
}