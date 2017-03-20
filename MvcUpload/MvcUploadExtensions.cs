using Microsoft.AspNetCore.Builder;

namespace MvcUpload
{
    public static class MvcUploadExtensions
    {
        internal static MvcUploadOptions Options;

        public static IApplicationBuilder UseMvcUpload(this IApplicationBuilder builder, MvcUploadOptions options)
        {
            Options = options;
            return builder.UseMiddleware<MvcUploadMiddleware>();
        }
    }
}
