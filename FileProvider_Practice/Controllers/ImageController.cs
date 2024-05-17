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
        public ImageController(IFileProvider fileProvider)
        {
            this._fileProvider = fileProvider;
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

    }
}
