using SQLite;

namespace HbrClient.Model.Dto
{
    public class ClientBookDto: BookDto
    {
        public int? ClientId { get; set; }
    }
}