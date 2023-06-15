using LightCV.BL.Auth;
using LightCV.DAL;
using Microsoft.AspNetCore.Http;

namespace LightCVTest.Helpers;

public class BaseTest
{
    protected readonly IAuthDal authDal = new AuthDal();
    protected readonly IEncrypt encrypt = new Encrypt();
    protected readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected readonly IAuthBL authBl;

    public BaseTest()
    {
        authBl = new AuthBL(authDal, encrypt, httpContextAccessor);
    }
}