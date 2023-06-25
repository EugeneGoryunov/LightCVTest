using System.Transactions;
using LightCV.BL.Exception;
using LightCV.DAL.Models;
using LightCVTest.Helpers;

namespace LightCVTest;

public class AuthTest : Helpers.BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistrationTest()
    {
        using (TransactionScope scope = Helper.CreatTransactionScope())
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            //create user
            int userId = await authBl.CreatUser(new UserModel()
            {
                Email = email,
                Password = "qwe1234"
            });

            Assert.Throws<AuthorizationException>(delegate
            {
                authBl.Authenticate("qweasd", "111", false).GetAwaiter().GetResult();
            });
            
            Assert.Throws<AuthorizationException>(delegate
            {
                authBl.Authenticate(email, "111", false).GetAwaiter().GetResult();
            });
            
            Assert.Throws<AuthorizationException>(delegate
            {
                authBl.Authenticate("qweasd", "qwe1234", false).GetAwaiter().GetResult();
            });
            
            await authBl.Authenticate(email, "qwe1234", false);
        }
    }
}