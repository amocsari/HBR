using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public interface IHbrDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbSet<Book> Books { get; set; }
        DbSet<Bookmark> Bookmarks { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<UserBook> UserBooks { get; set; }
    }
}
