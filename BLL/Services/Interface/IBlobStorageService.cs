using System.IO;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBlobStorageService
    {
        Task<MemoryStream> GetFileFromStorageAsStream(int bookId, string extension);
        Task UploadBook(MemoryStream fileStream, int bookId, string extension = "pdf");
    }
}
