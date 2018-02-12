using MapsProject.Service.Interfaces;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace MapsProject.WEB.AuthProviders
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUserService userService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            try
            {
                var userInfo = userService.GetUserInfo(context.UserName, context.Password);
                if (context.UserName == userInfo.Name && context.Password == userInfo.Password)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.RoleName));
                    identity.AddClaim(new Claim("username", userInfo.Name));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
            }
            catch
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
    }
}