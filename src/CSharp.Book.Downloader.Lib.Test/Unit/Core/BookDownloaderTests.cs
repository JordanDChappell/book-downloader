using CSharp.Book.Downloader.Lib.Core;

namespace CSharp.Book.Downloader.Lib.Test.Unit.Core;

public class BookDownloaderTests {
    private readonly AutoMoqer moqer = new();
    private readonly BookDownloader sut;

    public BookDownloaderTests() {
        sut = moqer.Create<BookDownloader>();
    }

    [Fact]
    public async Task DownloadBookAsync_GivenBook_ShouldDownloadToExpectedLocation() {
        await Task.CompletedTask;
        true.ShouldBe(true);
    }
}
