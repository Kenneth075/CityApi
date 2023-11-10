using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace City.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }



        [HttpGet("{fileId}")]
        public ActionResult GetFiles(string fileId)
        {
            //Look up the actual file according to the fileId. Hard coder file for demo purpose.
            var pathToFile = "Ken Cv.pdf";

            //Check if the file exit in the path

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }
            
            //Finding the correct content type for our file.
            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";   //Default media type for arbituray binary data.
            }

            //if the file exit, read out bytes and pass it to the file.

            var bytes = System.IO.File.ReadAllBytes(pathToFile);

            //Pass through a content-type and getting the file name

            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
       
    }
}
