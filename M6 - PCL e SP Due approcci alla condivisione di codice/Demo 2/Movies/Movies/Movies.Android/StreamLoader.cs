using System.IO;
using System.Threading.Tasks;
using Android.Content;

namespace Movies.Droid
{
	public class StreamLoader : IStreamLoader
	{
	    private readonly Context _context;

	    public StreamLoader(Context context)
	    {
	        this._context = context;
	    }
	    public async Task<Stream> GetStreamForFilename(string filename)
	    {
	        await Task.Yield();
	        return _context.Assets.Open(filename);
	    }
	}
}