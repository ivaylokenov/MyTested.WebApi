using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Books.Api.Models;

namespace Books.Api
{
    using Books.Models;
    using Data;
    using Infrastructure;

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<Author>
    {
        public ApplicationUserManager(IUserStore<Author> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = ObjectFactory.TryGetInstance<ApplicationUserManager>() ??
                          new ApplicationUserManager(new UserStore<Author>(context.Get<BooksDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Author>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<Author>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
