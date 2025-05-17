using CSharp.Book.Downloader.Lib.Models;

namespace CSharp.Book.Downloader.Lib.Interfaces;

public interface IBookDownloader {
    Task<FileInfo> DownloadBookAsync(BookResponse book, string? directory);
}
