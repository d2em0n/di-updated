namespace TagsCloudContainer.StringParsers
{
    public interface IStringParser
    {
        public IEnumerable<Word> GetWordsFromString(string input);
    }
}
