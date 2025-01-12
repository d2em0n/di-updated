namespace TagsCloudContainer.WordFilters
{
    public interface IWordFilter
    {
        IEnumerable<Word> Process(IEnumerable<Word> words);
    }
}
