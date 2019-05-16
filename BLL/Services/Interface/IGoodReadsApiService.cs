using DAL.Entity;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IGoodReadsApiService
    {
        Task TryGetGoodReadsData(string isbn, Book entity);
    }
}
