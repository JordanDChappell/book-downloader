using CSharp.Book.Downloader.Lib.Interfaces;
using CSharp.Book.Downloader.Lib.Models;

namespace CSharp.Book.Downloader.Lib.Core;

public class BookDownloader : IBookDownloader {
    public Task<FileInfo> DownloadBookAsync(BookResponse book) {
        throw new NotImplementedException();
    }
}
