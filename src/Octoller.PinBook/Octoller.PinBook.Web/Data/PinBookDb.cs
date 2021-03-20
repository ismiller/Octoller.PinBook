using Microsoft.EntityFrameworkCore;
using Octoller.PinBook.Web.Data.Base;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data
{
    public class PinBookDb : DbContextBase<User, PinBookDb> 
    {
        public PinBookDb(DbContextOptions<PinBookDb> options) 
            : base(options)
        { }
    }
}
