using BLL.Services.Interface;
using DAL.Entity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace BLL.Services.Implementation
{
    public class GoodReadsApiService : IGoodReadsApiService
    {
        public async Task TryGetGoodReadsData(string isbn, Book entity)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://www.goodreads.com");
                var response = await client.GetAsync($"/search/index.xml?key=Kn9jyCFyPgYJUgV4B1bsw&q={isbn}&search[field]=isbn");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<XmlElement>();
                    var authorName = result.SelectSingleNode("/search/results/work/best_book/author/name").FirstChild.Value;
                    var title = result.SelectSingleNode("/search/results/work/best_book/title").FirstChild.Value;

                    entity.Author = authorName;
                    entity.Title = title;
                }
                else
                {
                    throw new HbrException("Error during goodreads query!");
                }
            }
        }
    }
}
