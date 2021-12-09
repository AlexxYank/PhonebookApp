using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PhonebookApp.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        }

        public string GetUserName()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.Name);
        }
    }
}
