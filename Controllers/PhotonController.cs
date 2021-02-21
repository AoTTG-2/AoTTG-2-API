using AoTTG2.IDS.Models;
using AoTTG2.IDS.Quickstart;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
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
        public async Task<IActionResult> Custom(string token = null)
        {
            var result = await ValidateToken(token);
            return Ok(result);
        }

        private async Task<CustomAuthenticationResult> ValidateToken(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                jwtHandler.ValidateToken(token, await GetParameters(), out var validatedToken);
                var securityToken = jwtHandler.ReadJwtToken(token);
                return new CustomAuthenticationResult
                {
                    AuthCookie = new Dictionary<string, object>()
                    {
                        {"authenticated", true}
                    },
                    ExpireAt = DateTime.UtcNow.Ticks,
                    ResultCode = PhotonResultCode.Success,
                    UserId = securityToken.Subject
                };
            }
            catch (Exception e)
            {
                return new CustomAuthenticationResult
                {
                    ResultCode = PhotonResultCode.Failed
                };
            }

        }

        private async Task<TokenValidationParameters> GetParameters()
        {
            var address = HttpContext.GetIdentityServerIssuerUri();
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{address}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            var validationParameters =
                new TokenValidationParameters
                {
                    ValidIssuer = address,
                    ValidAudience = address + "/resources",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = openIdConfig.SigningKeys
                };

            return validationParameters;
        }
    }
}