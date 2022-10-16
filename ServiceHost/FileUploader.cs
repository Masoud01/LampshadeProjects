using _0_Framework.Application;
using System.IO;

namespace ServiceHost;

public class FileUploader:IFileUploader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public string Upload(IFormFile file,string picturePath)
    {
        if (file == null)
        {
            return "";
        }

        var pathDirectory =$"{_webHostEnvironment.WebRootPath}//ProductPicture//{picturePath}";
        if (!Directory.Exists(pathDirectory))
        {
            Directory.CreateDirectory(pathDirectory);
        }

        var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
        var filePah = $"{pathDirectory}//{fileName}";
        using (var output = System.IO.File.Create(filePah))
        {
            file.CopyTo(output);
        }

        return $"{picturePath}//{fileName}";
    }
}