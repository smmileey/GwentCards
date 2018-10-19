using NSoup;

namespace GwentDataRetriever
{
    public class PageContentProvider : IPageContentProvider
    {
        public IDocument Provide(string requestUrl)
        {
            return new DocumentWrapper(NSoupClient.Connect(requestUrl)?.Get());
        }
    }
}