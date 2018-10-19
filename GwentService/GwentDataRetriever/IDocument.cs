namespace GwentDataRetriever
{
    public interface IDocument
    {
        IElements GetAllElements();
        IElements Select(string query);
    }
}