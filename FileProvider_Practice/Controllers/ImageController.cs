using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;
using System;


namespace FileProvider_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFileProvider _fileProvider;
        private readonly IWebHostEnvironment _environment;
        public ImageController(IFileProvider fileProvider,IWebHostEnvironment environment)
        {
            this._fileProvider = fileProvider;
            _environment = environment;
        }
      

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            IFileInfo fileInfo = _fileProvider.GetFileInfo(id.ToString() + ".jpg");

            if (fileInfo.Exists)
            {
                return File(fileInfo.CreateReadStream(), "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost] 
        public IResult UploadImage(IFormFile file)
        {
            
            string path = Path.Combine(_environment.WebRootPath + "Image");

            //確認有沒有Image料夾
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("建立資料夾");
            }
            //以目前圖片數量+1命名
            int lastfileName = Directory.GetFiles(path, "*.jpg").Length;
            string filename = (lastfileName + 1).ToString() + ".jpg";
            //file to byte[]

            using (var stream = System.IO.File.Create(Path.Combine(path, filename)))
            {
                file.CopyToAsync(stream);
            }
            return Results.Ok(lastfileName);
        }

    }
}
