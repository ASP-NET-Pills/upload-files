using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadFiles.ViewModels;

namespace UploadFiles
{
    [Route("/api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Get()
        {
            return Ok("Hello world!");
        }

        [Consumes("multipart/form-data")]
        [HttpPost("upload-file-multipart")]
        public async Task<IActionResult> UploadFileUsingMultipart([FromForm] MultipartRequest multipartRequest)
        {
            MultipartResponse multipartResponse = new MultipartResponse
            {
                FileLength = multipartRequest.File.Length,
                Number = multipartRequest.Number,
                Text = multipartRequest.Text
            };

            return Ok(multipartResponse);
        }

        [Consumes("application/octet-stream")]
        [HttpPost("upload-file-stream")]
        public async Task<IActionResult> UploadFileUsingStream([FromBody] Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);

                UploadFileResponse uploadFileResponse = new UploadFileResponse
                {
                    FileLength = memoryStream.Length
                };

                return Ok(uploadFileResponse);
            }
        }
    }
}