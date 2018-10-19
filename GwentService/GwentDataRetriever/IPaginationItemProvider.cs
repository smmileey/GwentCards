using NSoup.Select;

namespace GwentDataRetriever
{
    public interface IPaginationItemProvider
    {
        Elements Provide(int pageNumber);
    }
}