using Models;
using NSoup.Nodes;

namespace GwentDataRetriever
{
    public interface ICardInfoProvider
    {
        CardInfoDto GetCardInfo(Element paginationItem);
    }
}