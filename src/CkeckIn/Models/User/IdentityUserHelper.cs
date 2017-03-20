using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CkeckIn.Models.User
{
    public static class IdentityUserHelper
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static Guid GetGuidUserId(this ClaimsPrincipal principal)
        {
            Guid id;
            if(Guid.TryParse(GetUserId(principal), out id))
            {
                return id;
            } 
            return Guid.Empty;
        }
    }
}
