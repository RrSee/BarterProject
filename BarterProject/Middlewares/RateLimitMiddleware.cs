using System.Collections.Concurrent;

namespace BarterProject.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _requestLimit;
        private readonly TimeSpan _timeSpan;
        private readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new();
        private readonly IHttpContextAccessor _contextAccessor;

        public RateLimitMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeSpan, IHttpContextAccessor contextAccessor)
        {
            _next = next;
            _requestLimit = requestLimit;
            _timeSpan = timeSpan;
            _contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isAuthenticated = _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

            // Sadece auth olmayanlar için rate limit uygulanır
            if (!isAuthenticated)
            {
                var clientIp = context.Connection.RemoteIpAddress?.ToString();

                if (!string.IsNullOrEmpty(clientIp))
                {
                    var now = DateTime.UtcNow;
                    var requestLog = _requestTimes.GetOrAdd(clientIp, _ => new List<DateTime>());

                    lock (requestLog)
                    {
                        requestLog.RemoveAll(timestamp => timestamp <= now - _timeSpan);

                        if (requestLog.Count >= _requestLimit)
                        {
                            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                            context.Response.Headers.RetryAfter = _timeSpan.TotalSeconds.ToString();
                            return;
                        }

                        requestLog.Add(now);
                    }
                }
            }

            await _next(context);
        }
    }
}
