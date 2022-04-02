using Elsa.Bookmarks;
using Elsa.CustomActivityLibrary.Activities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.CustomActivityLibrary.BookMarks
{
    public class FileReceivedBookmarkProvider : 
        BookmarkProvider<FileReceivedBookmark, FileReceived>
    {
        public FileReceivedBookmarkProvider()
        {

        }
        //public virtual IEnumerable<IBookmark> GetBookmarks(BookmarkProviderContext<TActivity> context);
        //public virtual ValueTask<IEnumerable<IBookmark>> GetBookmarksAsync(BookmarkProviderContext<TActivity> context, CancellationToken cancellationToken);

        //
        //public override IEnumerable<IBookmark> GetBookmarks(BookmarkProviderContext<FileReceived> context)
        //CustomBookmarkProviderContext<FileReceived> context
        public override IEnumerable<IBookmark> GetBookmarks(BookmarkProviderContext<FileReceived> context)
        {
            //var asdf = (CustomBookmarkProviderContext<FileReceived>)context;
            //var bookMarkFinderResult = new BookmarkFinderResult();
            //return new[] { new BookmarkFinderResult(new FileReceivedBookmark()) };
            var bookmarkList = new List<IBookmark>() { new FileReceivedBookmark() };
            return bookmarkList;
        }

        // The following is not 
        //public override async ValueTask<IEnumerable<IBookmark>> GetBookmarksAsync(BookmarkProviderContext<FileReceived> context, CancellationToken cancellationToken)
        //{
        //    var supportedExtensions = 
        //        (await 
        //            context.ReadActivityPropertyAsync<FileReceived, ICollection<string>>(x => x.SupportedFileExtensions, cancellationToken)
        //        )?.ToList() ?? new List<string>();

        //    //return !supportedExtensions.Any()
        //    //    ? new[] { Result(new FileReceivedBookmark()) }
        //    //    : supportedExtensions.Select(x => Result(new FileReceivedBookmark(x)));
        //}

        //public override async ValueTask<IEnumerable<IBookmark>> GetBookmarksAsync(BookmarkProviderContext<FileReceived> context, CancellationToken cancellationToken)
        //{
        //    //var supportedExtensions = (await context.ReadActivityPropertyAsync<FileReceived, ICollection<string>>(x => x.SupportedFileExtensions, cancellationToken))?.ToList() ?? new List<string>();
        //    var supportedExtensions = (await context.ReadActivityPropertyAsync<FileReceived, ICollection<string>>(x => x.SupportedFileExtensions, cancellationToken))?.ToList() ?? new List<string>();

        //    return !supportedExtensions.Any()
        //        ? new[] { Result(new FileReceivedBookmark()) }
        //        : supportedExtensions.Select(x => Result(new FileReceivedBookmark(x)));
        //    return !supportedExtensions.Any()
        //        ? new[] { Result(new FileReceivedBookmark()) }
        //        : supportedExtensions.Select(x => Result(new FileReceivedBookmark(x)));
        //}
    }
}
