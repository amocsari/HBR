using System.IO;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBlobStorageService
    {
        Task<Stream> GetFileFromStorageAsStream(int bookId, string extension);
    }
}
