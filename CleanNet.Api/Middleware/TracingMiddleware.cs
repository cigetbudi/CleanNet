using System.Diagnostics;
using System.Text;

namespace CleanNet.Api.Middleware
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;

        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var activity = Activity.Current;
            if (activity != null)
            {
                activity.SetTag("http.method", context.Request.Method);
                activity.SetTag("http.path", context.Request.Path);

                if (context.Request.QueryString.HasValue)
                    activity.SetTag("http.query", context.Request.QueryString.Value);

                // Kalau request content-type json, baca body dan log ke activity
                if (context.Request.ContentType != null &&
                    context.Request.ContentType.Contains("application/json"))
                {
                    context.Request.EnableBuffering();

                    using var reader = new StreamReader(
                        context.Request.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        bufferSize: 1024,
                        leaveOpen: true);

                    string body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; 

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        activity.SetTag("http.request_body", body);
                    }
                }
            }

            await _next(context);
        }
    }
}