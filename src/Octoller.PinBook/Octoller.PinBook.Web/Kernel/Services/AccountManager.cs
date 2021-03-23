using Microsoft.AspNetCore.Identity;
using Octoller.PinBook.Web.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Kernel.Services
{
    public class AccountManager
    {
        private IUserValidator<User>  UserValidator { get; }
        private IPasswordValidator<User> PasswordValidator { get; }
        private IPasswordHasher<User> PasswordHasher { get; }
        private UserManager<User> UserManager { get; }

        public AccountManager(
            IUserValidator<User> userValidator,
            IPasswordValidator<User> passwordValidator,
            IPasswordHasher<User> passwordHasher,
            UserManager<User> userManager)
        {
            UserValidator = userValidator;
            PasswordValidator = passwordValidator;
            PasswordHasher = passwordHasher;
            UserManager = userManager;
        }

        public async Task<IdentityResult> CreateAccount(string email, string password)
        {
            var resultCreate = await UserManager.CreateAsync(
                new User { UserName = email, Email = email }, password);

            if (resultCreate.Succeeded)
            {
                await AddToRole(await UserManager.FindByEmailAsync(email));

                return IdentityResult.Success;
            }

            return IdentityResult.Failed(resultCreate.Errors.ToArray());
        }

        public async Task<IdentityResult> CreateAccount(string email)
        {
            var resultCreate = await UserManager.CreateAsync(
                new User { UserName = email, Email = email });

            if (resultCreate.Succeeded)
            {
                await AddToRole(await UserManager.FindByEmailAsync(email));

                return IdentityResult.Success;
            }

            return IdentityResult.Failed(resultCreate.Errors.ToArray());
        }

        public async Task<IdentityResult> UpdateAccount(string id, string email, string password)
        {
            if (string.IsNullOrEmpty(id))
            {
                return FailedResult("Не указан Id");
            }

            var errors = new List<IdentityError>();

            var user = await UserManager.FindByIdAsync(id);
            if (user is not null)
            {
                user.Email = email;

                var validEmailResult = await UserValidator.ValidateAsync(UserManager, user);

                if (!validEmailResult.Succeeded)
                {
                    errors.AddRange(validEmailResult.Errors);
                }

                IdentityResult validPasswordResult = null;

                if (!string.IsNullOrEmpty(password))
                {
                    validPasswordResult = await PasswordValidator.ValidateAsync(UserManager, user, password);

                    if (validPasswordResult.Succeeded)
                    {
                        user.PasswordHash = PasswordHasher.HashPassword(user, password);
                    } else
                    {
                        errors.AddRange(validPasswordResult.Errors);
                    }
                }

                if (validEmailResult.Succeeded && (validPasswordResult is not null || validPasswordResult.Succeeded))
                {
                    var updateResult = await UserManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                    else
                    {
                        errors.AddRange(updateResult.Errors);
                    }
                }
                return FailedResult(errors);
            }

            return FailedResult("Пользователь не найден");
        }

        private async Task AddToRole(User user)
        {
            if (!await UserManager.IsInRoleAsync(user, AppData.RolesData.UserRoleName))
            {
                _ = await UserManager.AddToRoleAsync(user, AppData.RolesData.UserRoleName);
            }
        }

        private IdentityResult FailedResult(string description)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "",
                Description = description
            });
        }

        private IdentityResult FailedResult(IEnumerable<IdentityError> errors) =>
            IdentityResult.Failed(errors.ToArray());
    }
}
