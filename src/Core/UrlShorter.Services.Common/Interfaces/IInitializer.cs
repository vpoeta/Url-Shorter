using System.Threading.Tasks;

namespace UrlShorter.Services.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
