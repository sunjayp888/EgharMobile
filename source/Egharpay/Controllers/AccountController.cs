using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.EmailServiceReference;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Enums;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Egharpay.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Egharpay.Business.Enum;
using Egharpay.Extensions;

namespace Egharpay.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private IPersonnelBusinessService PersonnelBusinessService { get; set; }
        private IPersonnelEmailBusinessService PersonnelEmailBusinessService { get; set; }
        private ISellerBusinessService SellerBusinessService { get; set; }
        private readonly IEmailBusinessService _emailBusinessService;
        private readonly IOtpBusinessService _otpBusinessService;

        public AccountController(IPersonnelBusinessService personnelBusinessService, IEmailBusinessService emailBusinessService, ISellerBusinessService sellerBusinessService, IPersonnelEmailBusinessService personnelEmailBusinessService, IConfigurationManager configurationManager, IOtpBusinessService otpBusinessService) : base(configurationManager)
        {
            PersonnelBusinessService = personnelBusinessService;
            SellerBusinessService = sellerBusinessService;
            PersonnelEmailBusinessService = personnelEmailBusinessService;
            _otpBusinessService = otpBusinessService;
            _emailBusinessService = emailBusinessService;
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;

        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);

                if (model.UserName.IsValidEmail())
                {
                    var userByEmail = await UserManager.FindByEmailAsync(model.UserName);
                    if (userByEmail == null)
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                        return View(model);
                    }
                    model.UserName = userByEmail.UserName;
                    user = await UserManager.FindAsync(userByEmail.UserName, model.Password);

                }
                if (user != null)
                {
                    if (User.IsSeller() && user.EmailConfirmed)
                    {
                        ModelState.AddModelError("", "Confirm Email Address.");
                        return View(model);
                    }
                    var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }

                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
        }


        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var personnelResult = await CreatePersonnel(model);
        //        if (!personnelResult.Succeeded)
        //        {
        //            foreach (var error in personnelResult.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //            return View(model);
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PersonnelId = personnelResult.Entity.PersonnelId };
        //        var role = model.IsSeller ? Role.Seller.ToString() : Role.Personnel.ToString();
        //        var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == role).Id;
        //        user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            model.PersonnelId = personnelResult.Entity.PersonnelId;
        //            if (model.IsSeller)
        //                CreateSeller(model);
        //            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            //  await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //            await SendConfirmationMail(personnelResult.Entity, callbackUrl);
        //            return RedirectToAction("Confirm", "Account", new { email = user.Email });
        //        }
        //        AddErrors(result);
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var otpValidationResult = await _otpBusinessService.IsValidOtp(Convert.ToInt32(model.OTP), Convert.ToDecimal(model.MobileNumber), (int)OtpReason.Login, DateTime.UtcNow);
                if (!otpValidationResult.Succeeded)
                {
                    model.HasError = true;
                    ModelState.AddModelError("", otpValidationResult.Message);
                    return View(model);
                }
                var user = new ApplicationUser { UserName = model.MobileNumber, Email = model.Email };
                var role = model.IsSeller ? Role.Seller.ToString() : Role.Personnel.ToString();
                var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == role).Id;
                user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var personnelResult = await CreatePersonnel(model);
                    if (!personnelResult.Succeeded)
                    {
                        model.HasError = true;
                        foreach (var error in personnelResult.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                        return View(model);
                    }
                    model.PersonnelId = personnelResult.Entity.PersonnelId;
                    user.PersonnelId = personnelResult.Entity.PersonnelId;
                    await UserManager.UpdateAsync(user);
                    if (model.IsSeller)
                        CreateSeller(model);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //  await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    await SendConfirmationMail(personnelResult.Entity, callbackUrl);
                    return RedirectToAction("Confirm", "Account", new { email = user.Email });
                }
                model.HasError = true;
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task<ValidationResult<Personnel>> CreatePersonnel(RegisterViewModel model)
        {
            var personnel = new Personnel()
            {
                Email = model.Email,
                Forenames = model.FirstName,
                Surname = model.LastName,
                Postcode = model.Pincode,
                IsSeller = model.IsSeller,
                UserId = model.AspNetUserId,
                Mobile = model.MobileNumber
            };
            return await PersonnelBusinessService.CreatePersonnel(personnel);
        }

        private void CreateSeller(RegisterViewModel model)
        {
            var watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(100));

            var coordinates = watcher.Position.Location;

            var seller = new Seller()
            {
                PersonnelId = model.PersonnelId,
                Owner = string.Format("{0} {1}", model.FirstName, model.LastName),
                Email = model.Email,
                Pincode = model.Pincode,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ApprovalStateId = (int)ApprovalState.Pending
            };
            SellerBusinessService.CreateSeller(seller);
        }

        private async Task<ValidationResult> SendConfirmationMail(Personnel personnel, string callbackUrl)
        {
            var validationResult = new ValidationResult();
            var personnelConfirmedEmail = new PersonnelCreatedEmail()
            {
                FullName = personnel.FullName,
                CallBackUrl = callbackUrl,
                Subject = "Confirm your account",
                TemplateName = "PersonnelCreatedEmail",
                ToAddress = new List<string>() { personnel.Email }
            };
            try
            {
                await PersonnelEmailBusinessService.SendConfirmationMail(personnelConfirmedEmail);
                validationResult.Succeeded = true;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                return validationResult;
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            ApplicationUser user = this.UserManager.FindById(userId);

            if (user.Id == userId)
            {
                user.EmailConfirmed = true;
                await UserManager.UpdateAsync(user);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Confirm", "Account", new { email = user.Email });
        }

        [AllowAnonymous]
        public ActionResult Confirm(string email)
        {
            ViewBag.Email = email; return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //---------------------------------------------------------------------
                var validationResult = new ValidationResult();
                var forgotEmail = new PersonnelCreatedEmail()
                {
                    FullName = user.Email,
                    CallBackUrl = callbackUrl,
                    Subject = "Confirm your account",
                    TemplateName = "ForgotPassword",
                    ToAddress = new List<string>() { model.Email },
                    FromAddress = "sunjayp88@gmail.com"
                };
                try
                {
                    await PersonnelEmailBusinessService.SendForgotMail(forgotEmail);
                    validationResult.Succeeded = true;
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                catch (Exception ex)
                {
                    validationResult.Succeeded = false;
                    //return validationResult;
                }
                //var userEmailData = new EmailData()
                //{
                //    BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                //    Body = String.Format("To reset your password by clicking < a href =\"" + callbackUrl + "\">here</a>"),
                //    Subject = "Reset Password (Mumbile.com)",
                //    IsHtml = true,
                //    ToAddressList = new List<string> { user.Email }
                //};
                //_emailBusinessService.SendEmail(userEmailData);
                //return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string code)
        {
            //return code == null ? View("Error") : View();
            var resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(User.Identity.GetUserId());
            var model = new ResetPasswordViewModel() { Code = resetPasswordToken };
            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            //var personnelData = PersonnelBusinessService.RetrievePersonnel(centreId,user.PersonnelId);
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                //var emailData = new EmailData()
                //{
                //    BCCAddressList = new List<string> { "developer@nidantech.com" },
                //    Body = String.Format("Dear {0}.{1} {2}, Your Password has been changed successfully. And your New Password is {3}", personnelData.Title, personnelData.Forenames, personnelData.Surname, model.Password),
                //    Subject = "Changed Password For Nidan ERP",
                //    IsHtml = true,
                //    ToAddressList = new List<string> { personnelData.Email }

                //};
                //_emailService.SendEmail(emailData);
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "Inland12!";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}