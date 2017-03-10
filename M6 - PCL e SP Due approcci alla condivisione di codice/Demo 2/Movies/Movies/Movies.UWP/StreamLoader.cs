using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Movies.UWP
{
    public class StreamLoader : IStreamLoader
    {
        public async Task<Stream> GetStreamForFilename(string filename)
        {
            var file = await Package.Current.InstalledLocation.GetFileAsync($@"Assets\{filename}");
            return await file.OpenStreamForReadAsync();
        }
    }
}
