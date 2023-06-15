using System.Transactions;
using LightCV.DAL.Models;
using LightCVTest.Helpers;

namespace LightCVTest;

public class RegisterTest : Helpers.BaseTest
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
            //validate: should not be in DB
            var emailValidationResult = await authBl.ValidateEmail(email);
            Assert.IsNull(emailValidationResult);

            //create user
            int userId = await authBl.CreatUser(new UserModel()
            {
                Email = email,
                Password = "qwe1234"
            });
            
            Assert.Greater(userId, 0);
            
            //validate: should  be in DB
            var userValidationResult = await authBl.ValidateEmail(email);
            Assert.IsNotNull(userValidationResult);
        }
        Assert.Pass();
    }
}