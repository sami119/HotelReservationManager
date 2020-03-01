using HotelReservationManager.Areas.Identity.Data;
using HotelReservationManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelReservationManager.Policy
{
    public class IsActiveHandler : AuthorizationHandler<IsActiveRequirment>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<HotelManagerUser> _userManager;

        public IsActiveHandler(IHttpContextAccessor httpContextAccessor, UserManager<HotelManagerUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsActiveRequirment requirement)
        {
            UserIdentityProvider identityProvider = new UserIdentityProvider(_httpContextAccessor);
            var curUserId = identityProvider.GetCurrentUserId();
            var curUser = _userManager.FindByIdAsync(curUserId);
            if(curUser.Result.IsActive == true)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
