using NSoup.Select;

namespace GwentDataRetriever
{
    public interface IElements
    {
        IElement Last { get; }
        IElements Select(string query);
        Elements GetValue();
    }
}