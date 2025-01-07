namespace TagsCloudContainer.TextProcessor
{
    public interface ITextProcessor
    {
        public Dictionary<Word, int> WordFrequencies();
    }
}
