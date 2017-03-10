using System.IO;
using System.Threading.Tasks;

namespace Movies
{
    public interface IStreamLoader
    {
        Task<Stream> GetStreamForFilename(string filename);
    }
}