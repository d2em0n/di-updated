using System.Text.RegularExpressions;

namespace TagsCloudContainer.StringParsers
{
    public class RegexParser : IStringParser
    {
        private readonly Regex _regex = new("\\b(?:\\w|-)+\\b", RegexOptions.Compiled);
        public Result<IEnumerable<Word>> GetWordsFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Fail<IEnumerable<Word>>("Input cannot be empty");
            return Result.Ok(_regex.Matches(input)
                .Select(w => new Word(w.Value)));
        }
    }
}
