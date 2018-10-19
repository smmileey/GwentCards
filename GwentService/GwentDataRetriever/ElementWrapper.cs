using NSoup.Nodes;

namespace GwentDataRetriever
{
    public class ElementWrapper : IElement
    {
        private readonly Element _element;

        public ElementWrapper(Element element)
        {
            _element = element;
        }

        public virtual string OwnText()
        {
            return _element.OwnText();
        }
    }
}