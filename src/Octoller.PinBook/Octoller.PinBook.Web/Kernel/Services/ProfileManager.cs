using Microsoft.AspNetCore.Identity;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Data.Stores;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Kernel.Services
{
    public class ProfileManager
    {
        private ProfileStore ProfileStore { get; }

        public ProfileManager(ProfileStore profileStore)
        {
            ProfileStore = profileStore;
        }

        /// <summary>
        /// Создает профиль информации о пользователе.
        /// </summary>
        /// <param name="targetUser">Пользователь, для которого создается профиль.</param>
        /// <param name="newProfile">Профиль.</param>
        /// <returns><see cref="IdentityResult"/>.</returns>
        public async Task<IdentityResult> CreateProfileAsync(User targetUser, Profile newProfile)
        {
            if (targetUser is null)
            {
                return await FailedResult("User not set");
            }

            if (newProfile is null)
            {
                return await FailedResult("Profile not set");
            }

            var profile = await ProfileStore.GetByUserIdAsync(targetUser.Id);

            if (profile is null)
            {
                newProfile.Name = string.IsNullOrEmpty(newProfile.Name) 
                    ? targetUser.UserName 
                    : newProfile.Name;

                newProfile.UserId = targetUser.Id;

                var result = await ProfileStore.CreateAsync(newProfile, targetUser.UserName);

                if (!result)
                {
                    return await FailedResult("Ошибка записи");
                }

                return IdentityResult.Success;
            }

            return await FailedResult("Profile already created");
        }

        /// <summary>
        /// Возвращает объект с информацией профиля пользователя по Id.
        /// </summary>
        /// <param name="id">Id профиля.</param>
        /// <returns>Объект <see cref="Profile"/>.</returns>
        public async Task<Profile> FindProfileByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return default;
            }

            return await ProfileStore.GetByIdAsync(id);
        }

        /// <summary>
        /// Возвращает объект с информацией профиля пользователя.
        /// </summary>
        /// <param name="user">Данные пользователя, профиль которого требуется вернуть.</param>
        /// <returns>Объект <see cref="Profile"/>.</returns>
        public async Task<Profile> FindProfileByUserAsync(User user)
        {
            if (user is null)
            {
                return default;
            }

            return await ProfileStore.GetByUserIdAsync(user.Id);
        }

        private Task<IdentityResult> FailedResult(string description)
        {
            return Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "",
                Description = description
            }));
        }
    }
}
