namespace TagsCloudContainer.StringParsers
{
    public interface IStringParser
    {
        Result<IEnumerable<Word>> GetWordsFromString(string input);
    }
}
