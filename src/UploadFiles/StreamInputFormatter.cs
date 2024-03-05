using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace DustInTheWind.UploadFiles
{
    public class StreamInputFormatter : InputFormatter
    {
        public StreamInputFormatter()
        {
            MediaTypeHeaderValue mediaTypeHeaderValue = MediaTypeHeaderValue.Parse("application/octet-stream");
            SupportedMediaTypes.Add(mediaTypeHeaderValue);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return context.HttpContext.Request.ContentType == "application/octet-stream";
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(Stream);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            return InputFormatterResult.SuccessAsync(context.HttpContext.Request.Body);
        }
    }
}