namespace Books.Tests.Mocks.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Api;
    using Microsoft.AspNet.Identity;
    using Models;
    using Moq;

    public class ApplicationUserManagerMock
    {
        public static ApplicationUserManager Create()
        {
            // create our mocked user
            var user = new Author { UserName = "TestAuthor", Email = "TestAuthor@test.com" };

            // mock the application user manager with mocked user store
            var mockedUserStore = new Mock<IUserStore<Author>>();
            var applicationUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            // mock the application user manager to always return our user object with any username and password
            applicationUserManager.Setup(x => x.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            // mock the application user manager create identity in order to generate valid access token when requested
            applicationUserManager.Setup(x => x.CreateIdentityAsync(It.IsAny<Author>(), It.IsAny<string>()))
                .Returns<Author, string>(
                    (author, password) =>
                        Task.FromResult(new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, author.UserName)},
                            DefaultAuthenticationTypes.ApplicationCookie)));

            return applicationUserManager.Object;
        }
    }
}
