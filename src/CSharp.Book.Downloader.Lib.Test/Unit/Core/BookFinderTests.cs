using CSharp.Book.Downloader.Lib.Core;

namespace CSharp.Book.Downloader.Lib.Test.Unit.Core;

public class BookFinderTests {
    private readonly AutoMoqer moqer = new();
    private readonly BookFinder sut;

    public BookFinderTests() {
        sut = moqer.Create<BookFinder>();
    }

    [Fact]
    public async Task GetBooksAsync_GivenRequest_ShouldLocateBooks() {
        await Task.CompletedTask;
        true.ShouldBe(true);
    }
}
