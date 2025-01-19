namespace TagsCloudContainer.TextProcessor
{
    public interface ITextProcessor
    {
        public Result<Dictionary<Word, int>> WordFrequencies();
    }
}
