using System;

namespace Extensions
{
    public static class Extensions
    {
        public delegate bool TryParser<TResult>(string s, out TResult result);

        public static T TryParse<T>(this string text, TryParser<T> tryParser)
        {
            if (string.IsNullOrWhiteSpace(text)) return default(T);

            tryParser(text, out var outResult);
            return outResult;
        }
    }
}
