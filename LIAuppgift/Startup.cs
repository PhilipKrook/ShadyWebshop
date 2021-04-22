using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(LIAuppgift.Startup))]

namespace LIAuppgift
{
    public class Startup
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(Startup));

        public void Configuration(IAppBuilder app)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub"; 

            // set the default authentication type to 'Cookies'
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromMinutes(OIDCInMemoryConfiguration.AuthCookieValidMinutes),
                SlidingExpiration = true
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "ZZLouDKZUq6NrBjzyLvznrZf1AfCjJvg",
                ClientSecret = "3K4VDmK24SVLqI74ZOgK2RYSDIb2y-kePiIW0pwIyKA9baB96tA_k66i2taB-7XH",
                Authority = "https://tietoevry-partner-sandbox.eu.auth0.com/", 
                RedirectUri = "https://localhost:44392/signin-oidc",
                PostLogoutRedirectUri = "https://localhost:44392/signout-oidc", 
                Scope = $"openid email profile", 
                ResponseType = "code id_token",
                RequireHttpsMetadata = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "sub",
                    RoleClaimType = "role",
                    ValidateTokenReplay = true
                },
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType, 
                UseTokenLifetime = false, 
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = notification =>
                    {
                        switch (notification.ProtocolMessage.RequestType)
                        {
                            case OpenIdConnectRequestType.Authentication:

                                if (notification.OwinContext.Response.StatusCode == 401)
                                {
                                    if (IsAjaxRequest(notification.Request))
                                    {
                                        if (Logger.IsInformationEnabled())
                                        {
                                            Logger.Information($"Request is made with AJAX and response is 401.");
                                        }

                                        notification.HandleResponse();
                                        return Task.FromResult(0);
                                    }

                                    if (notification.OwinContext.Authentication.User.Identity.IsAuthenticated)
                                    {
                                        if (Logger.IsInformationEnabled())
                                        {
                                            Logger.Information($"Request response code would be 401 but user '{notification.OwinContext.Authentication.User.Identity.Name}' is authenticated, switching response code to 403 (forbidden).");
                                        }

                                        notification.OwinContext.Response.StatusCode = 403;
                                        notification.HandleResponse();
                                        return Task.FromResult(0);
                                    }
                                }

                                break;
                            case OpenIdConnectRequestType.Logout:

                                if (notification.OwinContext.Authentication.User.Identity.IsAuthenticated)
                                {
                                    Logger.Information($"User is logging out. User: {notification.OwinContext.Authentication.User.Identity.Name}.");
                                }

                                var idTokenHint = notification.OwinContext.Authentication.User.FindFirst(OpenIdConnectParameterNames.IdToken);

                                if (idTokenHint != null)
                                {
                                    if (Logger.IsDebugEnabled())
                                    {
                                        Logger.Debug($"Redirecting to Identity provider for logout with IdTokenHint.");
                                    }

                                    notification.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                                }
                                else
                                {
                                    if (Logger.IsDebugEnabled())
                                    {
                                        Logger.Debug($"Redirecting to Identity provider for logout without IdTokenHint.");
                                    }
                                }

                                return Task.FromResult(0);

                            case OpenIdConnectRequestType.Token:
                                break;
                            default:
                                break;
                        }

                        return Task.FromResult(0);
                    },
                    AuthorizationCodeReceived = async notification =>
                    {
                        if (Logger.IsDebugEnabled())
                        {
                            Logger.Debug($"Authorization code received for sub: {notification.JwtSecurityToken.Subject}. Received claims: {GetClaimsAsString(notification.JwtSecurityToken.Claims)}.");
                        }
                        else
                        {
                            Logger.Information($"Authorization code received for sub: {notification.JwtSecurityToken.Subject}.");
                        }

                        OpenIdConnectConfiguration configuration = null;

                        try
                        {
                            configuration = await notification.Options.ConfigurationManager.GetConfigurationAsync(notification.Request.CallCancelled);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Failed to get OpenIdConnectConfiguration. Cannot authorize the client with sub: {notification.JwtSecurityToken.Subject}.", ex);
                            throw;
                        }

                        var tokenClient = new TokenClient(configuration.TokenEndpoint, notification.Options.ClientId, notification.Options.ClientSecret, style: AuthenticationStyle.PostValues);

                        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(notification.ProtocolMessage.Code, notification.RedirectUri, cancellationToken: notification.Request.CallCancelled);

                        if (tokenResponse.IsError)
                        {
                            Logger.Error($"There was an error retrieving the access token for sub: {notification.JwtSecurityToken.Subject}. Error: {tokenResponse.Error}. Error description: {tokenResponse.ErrorDescription}.");

                            notification.HandleResponse();

                            notification.Response.Write($"Error retrieving access token. {tokenResponse.ErrorDescription}.");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
                        {
                            Logger.Error($"Didn't receive access token for sub: {notification.JwtSecurityToken.Subject}.");

                            notification.HandleResponse();

                            notification.Response.Write($"Error, access token not received. {tokenResponse.ErrorDescription}.");
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(tokenResponse.IdentityToken))
                        {
                            try
                            {
                                JwtSecurityTokenHandler idTokenHandler = new JwtSecurityTokenHandler();
                                var parsedIdToken = idTokenHandler.ReadJwtToken(tokenResponse.IdentityToken);

                                if (string.Compare(parsedIdToken.Issuer, notification.JwtSecurityToken.Issuer, StringComparison.OrdinalIgnoreCase) != 0 ||
                                    string.Compare(parsedIdToken.Subject, notification.JwtSecurityToken.Subject, StringComparison.OrdinalIgnoreCase) != 0)
                                {
                                    Logger.Error($"Authorization endpoint id token 'sub' ({notification.JwtSecurityToken.Subject}) and 'iss' ({notification.JwtSecurityToken.Issuer}) claim values don't match with token endpoint 'sub' ({parsedIdToken.Subject}) and 'iss' ({parsedIdToken.Issuer}) claim values.");

                                    notification.HandleResponse();

                                    notification.Response.Write("Token endpoint identity token doesn't match autohorization endpoint returned identity token.");
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"Failed to validate token endpoint identity token against autohorization endpoint returned identity token.", ex);

                                notification.HandleResponse();

                                notification.Response.Write("Failed to validate token endpoint identity token against autohorization endpoint returned identity token.");
                                return;
                            }
                        }
                        else
                        {
                            Logger.Information($"Token endpoint didn't return identity token for sub: {notification.JwtSecurityToken.Subject}.");
                        }

                        var userInfoClient = new UserInfoClient(configuration.UserInfoEndpoint);
                        var userInfoResponse = await userInfoClient.GetAsync(tokenResponse.AccessToken);

                        if (userInfoResponse.IsError)
                        {
                            Logger.Error($"There was an error retrieving the user information for sub: {notification.JwtSecurityToken.Subject}. Error: {userInfoResponse.Error}.");

                            notification.HandleResponse();

                            notification.Response.Write($"Error retrieving user information. {userInfoResponse.Error}.");
                            return;
                        }

                        if (Logger.IsDebugEnabled())
                        {
                            Logger.Debug($"Userinfo received for sub: {notification.JwtSecurityToken.Subject}. Received claims: {GetClaimsAsString(userInfoResponse.Claims)}.");
                        }
                        var authClaimsIdentity = new ClaimsIdentity(notification.AuthenticationTicket.Identity.AuthenticationType, "sub", JwtClaimTypes.Role);

                        List<Claim> roleClaims = new List<Claim>();
                        List<Claim> otherClaims = new List<Claim>();

                        authClaimsIdentity.AddClaim(new Claim(JwtClaimTypes.Role, "WebAdmins"));
                        authClaimsIdentity.AddClaim(new Claim(JwtClaimTypes.Role, "WebEditor"));

                        foreach (var c in userInfoResponse.Claims)
                        {
                            if (string.Compare(c.Type, "role", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                roleClaims.Add(c);
                            }
                            else
                            {
                                otherClaims.Add(c);
                            }
                        }

                        string username = notification.JwtSecurityToken.Subject;
                        authClaimsIdentity.AddClaim(new Claim("sub", username));

                        bool addPublisherClaim = false;

                        authClaimsIdentity.AddClaim(new Claim(OpenIdConnectParameterNames.IdToken, notification.ProtocolMessage.IdToken));

                        notification.AuthenticationTicket = new AuthenticationTicket(authClaimsIdentity, notification.AuthenticationTicket.Properties);

                        Logger.Information($"Authenticated and logging in user '{GetFullName(authClaimsIdentity.Claims)}' (sub: {notification.JwtSecurityToken.Subject}).");

                        await ServiceLocator.Current.GetInstance<ISynchronizingUserService>().SynchronizeAsync(notification.AuthenticationTicket.Identity);
                    },
                    AuthenticationFailed = notification =>
                    {
                        Logger.Error($"Authentication failed: {notification.Exception.Message}");

                        notification.HandleResponse();
                        notification.Response.Write(notification.Exception.Message);
                        return Task.FromResult(0);
                    },
                    SecurityTokenReceived = notification =>
                    {

                        if (Logger.IsDebugEnabled())
                        {
                            try
                            {
                                Logger.Debug($"Security token received. Code: '{notification.ProtocolMessage.Code}', IdToken: '{notification.ProtocolMessage.IdToken}'.");
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"Security token received. Failed to read Code and IdToken for debug logging.", ex);
                            }
                        }

                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = notification =>
                    {

                        if (Logger.IsDebugEnabled())
                        {
                            try
                            {
                                Logger.Debug($"Security token validated for sub: {notification.AuthenticationTicket.Identity.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value}.");
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"Security token validated. Failed to read values from protocol message for debug logging.", ex);
                            }
                        }

                        return Task.FromResult(0);
                    },
                    MessageReceived = notification =>
                    {

                        if (Logger.IsDebugEnabled())
                        {
                            Logger.Debug($"Message received.");
                        }

                        return Task.FromResult(0);
                    }
                }
            });

            app.UseStageMarker(PipelineStage.Authenticate);

            app.Map("/util/login.aspx", map =>
            {
                map.Run(ctx =>
                {
                    if (ctx.Authentication.User == null || !ctx.Authentication.User.Identity.IsAuthenticated)
                    {
                        ctx.Response.StatusCode = 401;
                    }
                    else
                    {
                        ctx.Response.Redirect("/");
                    }

                    return Task.FromResult(0);
                });
            });

            app.Map("/util/logout.aspx", map =>
            {
                map.Run(ctx =>
                {
                    ctx.Authentication.SignOut();
                    return Task.FromResult(0);
                });
            });

        }
        private static bool IsAjaxRequest(IOwinRequest request)
        {

            if (request == null)
            {
                return false;
            }

            var headers = request.Headers;

            if (headers != null && string.Compare(headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }

        private static string GetClaimsAsString(IEnumerable<Claim> claims)
        {
            if (claims == null || !claims.Any())
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(512);

            foreach (var c in claims)
            {
                sb.Append($"[{c.Type}:{c.Value}], ");
            }

            if (sb.Length > 2)
            {
                sb.Length = sb.Length - 2;
            }

            return sb.ToString();
        }

        private static string GetFullName(IEnumerable<Claim> claims)
        {
            if (claims == null || !claims.Any())
            {
                return string.Empty;
            }

            var firstname = claims.FirstOrDefault(c => string.Compare(c.Type, JwtClaimTypes.GivenName, StringComparison.OrdinalIgnoreCase) == 0);
            var lastname = claims.FirstOrDefault(c => string.Compare(c.Type, JwtClaimTypes.FamilyName, StringComparison.OrdinalIgnoreCase) == 0);

            return $"{firstname?.Value} {lastname?.Value}";
        }
    }

    internal static class OIDCInMemoryConfiguration
    {
       
        public const string ClientId = "epi-alloy-mvc"; // TODO: change your client ID here
        /// <summary>
        /// OIDC client secret.
        /// </summary>
        public const string ClientSecret = "epi-alloy-mvc-very-secret"; // TODO: change your secret here
        /// <summary>
        /// OIDC authority. Also used to get OIDC discovery automatically if the identity provider is using the default well-known endpoint (/.well-known/openid-configuration).
        /// </summary>
        public const string Authority = "http://localhost:5000/";
        /// <summary>
        /// OIDC url where Identity provider is allowed to return tokens or authorization code.
        /// </summary>
        public const string WebAppOidcEndpoint = "http://localhost:48660"; // TODO: change your web app address/port here
        /// <summary>
        /// Where the client is redirected to after identity provider logout.
        /// </summary>
        public const string PostLogoutRedirectUrl = "http://localhost:48660"; // NOTE: http://localhost:48660 and http://localhost:48660/ are different addresses (the backslash at the end)!
        /// <summary>
        /// Is HTTPS required for the metadata endpoint.
        /// </summary>
        public const bool RequireHttpsMetadata = false;
        /// <summary>
        /// How long the web application authentication cookie is valid (in minutes in our example).
        /// </summary>
        public const int AuthCookieValidMinutes = 60;
    }
}