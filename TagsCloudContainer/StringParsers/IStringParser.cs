namespace TagsCloudContainer.StringParsers
{
    public interface IStringParser
    {
        IEnumerable<Word> GetWordsFromString(string input);
    }
}
