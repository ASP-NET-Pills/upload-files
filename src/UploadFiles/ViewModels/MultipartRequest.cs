using Microsoft.AspNetCore.Http;

namespace UploadFiles.ViewModels
{
    public class MultipartRequest
    {
        public IFormFile File { get; set; }

        public int Number { get; set; }
        
        public string Text { get; set; }
    }
}