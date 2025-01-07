using System.Text.RegularExpressions;

namespace TagsCloudContainer.StringParsers
{
    public class RegexParser : IStringParser
    {
        public IEnumerable<Word> GetWordsFromString(string input)
        {
            var regex = new Regex("\\b(?:\\w|-)+\\b");

            return regex.Matches(input)
                .Cast<Match>()
                .Select(w => new Word(w.Value));
        }
    }
}
