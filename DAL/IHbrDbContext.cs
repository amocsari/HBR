using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public interface IHbrDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbSet<Book> Book { get; set; }
        DbSet<Bookmark> Bookmark { get; set; }
        DbSet<Genre> Genre { get; set; }
        DbSet<UserBook> UserBooks { get; set; }
        #region tmp ki lesz veve
        DbSet<User> User { get; set; }
        #endregion
    }
}
