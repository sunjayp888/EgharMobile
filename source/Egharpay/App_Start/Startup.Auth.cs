using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Authorization;
using Microsoft.Owin.Security.Authorization.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Egharpay.Interfaces;
using Egharpay.Models.Authorization;
using Egharpay.Models.Authorization.Handlers;
using Egharpay.Models.Identity;

using Microsoft.Practices.Unity;
using Owin;

namespace Egharpay
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieSecure = CookieSecureOption.SameAsRequest,
                CookieHttpOnly = true,
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie)),
                    OnException = context => { }
                },
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = false
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            var options = new AuthorizationOptions();
            //Permission policy
            options.AddPolicy(nameof(Policies.Permission.SuperUser), policy => { policy.Requirements.Add(new PermissionsRequirement("SuperUser")); });
            options.AddPolicy(nameof(Policies.Permission.Personnel), policy => { policy.Requirements.Add(new PermissionsRequirement("Personnel")); });
            //Resource Policy
            options.AddPolicy(nameof(Policies.Permission.Personnel), policy => { policy.Requirements.Add(new PermissionsRequirement("Personnel")); });

            var container = UnityConfig.GetConfiguredContainer();



            container.RegisterInstance(options, new ContainerControlledLifetimeManager());

            container.RegisterInstance<IAuthorizationPolicyProvider>(
                new DefaultAuthorizationPolicyProvider(container.Resolve<AuthorizationOptions>()),
                new ContainerControlledLifetimeManager());

            container.RegisterInstance<IEnumerable<IAuthorizationHandler>>(
                new IAuthorizationHandler[]
                {
                    new PersonnelAuthorizationHandler(container.Resolve<IAuthorizationBusinessService>()), 
                },
                new ContainerControlledLifetimeManager());

            container.RegisterInstance<IAuthorizationService>(
                new DefaultAuthorizationService(container.Resolve<IAuthorizationPolicyProvider>(), container.Resolve<IEnumerable<IAuthorizationHandler>>()),
                new ContainerControlledLifetimeManager()
            );

            options.Dependencies = new UnityAuthorizationDependencies(container);

            app.UseAuthorization(options);
            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            // Configure Authorization
        }
    }
}