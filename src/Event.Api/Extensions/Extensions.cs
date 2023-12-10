namespace Event.Api.Extensions
{
    /// <summary>
        /// 
        /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetUserId(this HttpContext httpContext)
        {
            var userClaimsCount = httpContext.User.Claims.Count();
            var userClaims = httpContext.User.Claims;
            if (userClaimsCount == 0)
            {
                return string.Empty;
            }
            return userClaims.Single(x => x.Type == "uid").Value;
        }
    }
}
