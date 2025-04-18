
using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;

namespace CSharp.Book.Downloader.Lib.Core;

public class ConfigLoader : IConfigLoader {
    public DownloadClientConfig? Config { get; private set; }

    public DownloadClientConfig LoadConfig(Stream stream) {
        throw new NotImplementedException();
    }

    public DownloadClientConfig LoadConfig(string filePath) {
        throw new NotImplementedException();
    }
}
