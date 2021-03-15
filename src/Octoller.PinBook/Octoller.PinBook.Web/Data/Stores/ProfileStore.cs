using Microsoft.EntityFrameworkCore;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Data.Stores.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Data.Stores
{
    public class ProfileStore : BaseStore<DatabaseAppContext>
    {
        public ProfileStore(DatabaseAppContext context) : base(context)
        {
        }

        private DbSet<Profile> ProfileSet => Context.Set<Profile>();
        public IQueryable<Profile> Accounts => ProfileSet;

        public async Task<bool> CreateAsync(Profile account, string initiator = null)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await Context.AddAsync(account);
            await SaveChangeAsync(initiator);

            return true;
        }

        public async Task<bool> UpdateAsync(Profile account, string initiator = null)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            Context.Attach(account);
            Context.Update(account);

            try
            {
                await SaveChangeAsync(initiator);
            } catch
            {
                ///TODO: передача ошибки выше по стеку
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Profile account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            Context.Remove(account);

            try
            {
                await SaveChangeAsync();
            } catch
            {
                ///TODO: передача ошибки выше по стеку
                return false;
            }

            return true;
        }

        public async Task<Profile> GetByIdAsync(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await ProfileSet.FindAsync(new[] { id });
        }

        public async Task<Profile> GetByUserIdAsync(string userId)
        {
            if (userId is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await ProfileSet
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }
    }
}
