using CSharp.Book.Downloader.Lib.Config;

namespace CSharp.Book.Downloader.Lib.Interfaces;

public interface IConfigLoader {
    DownloadClientConfig? Config { get; }
    DownloadClientConfig LoadConfig(Stream stream);
    DownloadClientConfig LoadConfig(string filePath);
}
