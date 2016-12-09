using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FollowTheTask.Web.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace FollowTheTask.Web
{
    public class ApplicationUserManager : UserManager<AuthUser>
    {
        public ApplicationUserManager(IUserStore<AuthUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore());

            manager.UserValidator = new UserValidator<AuthUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true
            };
            manager.PasswordHasher = new CustomPasswordHasher();

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
            manager.MaxFailedAccessAttemptsBeforeLockout = 7;

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AuthUser>(dataProtectionProvider.Create("FollowTheTask Auth"));
            }
            return manager;
        }


        public override async Task<bool> CheckPasswordAsync(AuthUser user, string password)
        {
            var res = await base.CheckPasswordAsync(user, password);
            if (res)
            {
                user.Dto.Auth.AccessGrantedTotal++;
                user.Dto.Auth.LastAccessGrantedDateUtc = DateTime.UtcNow;
            }
            else
            {
                user.Dto.Auth.AccessFailedTotal++;
                user.Dto.Auth.LastAccessFailedDateUtc = DateTime.UtcNow;
            }
            await Store.UpdateAsync(user);
            return res;
        }
    }


    public class ApplicationSignInManager : SignInManager<AuthUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }


    public class ApplicationRoleManager : RoleManager<UserRole, int>
    {
        public ApplicationRoleManager(IRoleStore<UserRole, int> store) : base(store)
        {
        }


        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            return new ApplicationRoleManager(new CustomRoleStore());
        }
    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var from = new MailAddress(ConfigurationManager.AppSettings["EmailAdress"], "Follow The Task");
            var to = new MailAddress(message.Destination);
            var m = new MailMessage(from, to) { Subject = message.Subject, Body = message.Body, IsBodyHtml = true };
            var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"], 587)
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAdress"],
                    ConfigurationManager.AppSettings["EmailPassword"]),
                EnableSsl = true
            };
            return smtpClient.SendMailAsync(m);
        }
    }
}