using System.Transactions;
using LightCV.BL.Exception;
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
            authBl.ValidateEmail(email).GetAwaiter().GetResult();

            //create user
            var userId = await authBl.CreatUser(new UserModel()
            {
                Email = email,
                Password = "qwe1234"
            });
            
            Assert.Greater(userId, 0);

            var userDalResult = await authDal.GetUserById(userId);
            Assert.AreEqual(email, userDalResult.Email);
            Assert.NotNull(userDalResult.Salt);
            
            var userByEmailDalResult = await authDal.GetUserByEmail(email);
            Assert.AreEqual(email, userByEmailDalResult.Email);

            
            //validate: should  be in DB
            Assert.Throws<DuplicateEmailException>(delegate { authBl.ValidateEmail(email).GetAwaiter().GetResult(); });

            string encPassword = encrypt.HashPassword("qwe1234", userByEmailDalResult.Salt);
            Assert.AreEqual(encPassword, userByEmailDalResult.Password);
        }
        Assert.Pass();
    }
}