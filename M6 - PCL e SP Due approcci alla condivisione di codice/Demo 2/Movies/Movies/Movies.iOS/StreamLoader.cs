using System.IO;
using System.Threading.Tasks;

namespace Movies.iOS
{
    public class StreamLoader : IStreamLoader
    {
        public async Task<Stream> GetStreamForFilename(string filename)
        {
            await Task.Yield();
            return File.OpenRead(filename);
        }
    }
}