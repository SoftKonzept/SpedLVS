using LvsMobileAPI.Services;

namespace LvsMobileAPI.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if ((userId != null) && (userId > 0))
            {
                int iUser = 0;
                int.TryParse(userId.ToString(), out iUser);
                if (iUser > 0)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = userService.GetUser(iUser);
                }
            }

            await _next(context);
        }
    }
}
