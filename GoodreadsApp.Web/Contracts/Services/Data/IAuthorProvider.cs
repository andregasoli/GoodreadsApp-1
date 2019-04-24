using GoodreadsApp.Contracts.Persistence.Domain;
using System.Threading.Tasks;

namespace GoodreadsApp.Web.Contracts.Services.Data
{
    public interface IAuthorProvider
    {
        Task<Author> GetAuthorDetailsAsync(string path);
    }
}
