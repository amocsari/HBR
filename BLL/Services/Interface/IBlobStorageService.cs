using System.IO;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBlobStorageService
    {
        Task<MemoryStream> GetFileFromStorageAsStream(int bookId, string extension);
    }
}
