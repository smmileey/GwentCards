namespace GwentDataRetriever
{
    public interface IPageContentProvider
    {
        IDocument Provide(string requestUrl);
    }
}