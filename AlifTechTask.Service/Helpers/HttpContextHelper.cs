using Microsoft.AspNetCore.Http;

namespace AlifTechTask.Service.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor { get; set; }
        public static HttpContext HttpContext => Accessor?.HttpContext;
        public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
        public static Guid? UserId => GetUserId();

        private static Guid? GetUserId()
        {
            string value = HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value;

            bool canParse = Guid.TryParse(value, out Guid id);
            return canParse ? id : null;
        }
    }
}
