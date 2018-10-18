using Utilities.Enumerations;

namespace Utilities
{
    public interface ISelector
    {
        string Get(AttributeSelectors attributeSelector);
        string Get(CardSelectors cardSelector);
        string Get(HtmlElementSelectors htmlElementSelector);
    }
}