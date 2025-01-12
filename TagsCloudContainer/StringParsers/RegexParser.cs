using System.Text.RegularExpressions;

namespace TagsCloudContainer.StringParsers
{
    public class RegexParser : IStringParser
    {
        private readonly Regex _regex = new("\\b(?:\\w|-)+\\b", RegexOptions.Compiled);
        public IEnumerable<Word> GetWordsFromString(string input)
        {
            return _regex.Matches(input)
                .Cast<Match>()
                .Select(w => new Word(w.Value));
        }
    }
}
