using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Webgentle.Models;

namespace Webgentle.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel usermodel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task SignOutAsync();

    }
}