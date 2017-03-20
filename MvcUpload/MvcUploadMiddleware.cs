using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MvcUpload
{
    public class MvcUploadMiddleware
    {
        readonly RequestDelegate _next;
        readonly IHostingEnvironment _environment;

        public MvcUploadMiddleware(RequestDelegate next, IHostingEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public Task Invoke(HttpContext context)
        {
            var options = MvcUploadExtensions.Options;

            if (context.Request.Method == "POST" && context.Request.Path == options.Route && context.Request.Form.Files.Count != 0)
            {
                var file = context.Request.Form.Files.First();
                var uploadsPath = options.UploadsFolder;

                if (file == null)
                {
                    throw new Exception("Could not find a file to upload");
                }

                var fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');

                var filePath = Path.Combine(_environment.ContentRootPath, uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }

                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"uploaded\": 1,\"fileName\": \"" + fileName + "\",\"url\": \"/" + uploadsPath + "/" + fileName + "\"}");

            }
            else
            {
                // ignore the request if no files are present
                return _next(context);
            }
        }
    }
}
