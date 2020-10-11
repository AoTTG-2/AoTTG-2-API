using AoTTG2.IDS.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class PhotonController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public PhotonController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Custom(string token2 = null)
        {
            //TODO: Validate the AccessToken

            var request = HttpContext;
            var jwtHandler = new JwtSecurityTokenHandler();
            var data = jwtHandler.ReadJwtToken(token2);
            var cookie = new Dictionary<string, object>()
            {
                { "authenticated", true }
            };
            return Ok(new
            {
                UserId = data.Subject,
                AuthCookie = cookie,
                ResultCode = 1,
            });
        }
    }
}