namespace TagsCloudContainer.WordFilters
{
    public interface IWordFilter
    {
        public IEnumerable<Word> Process(IEnumerable<Word> words);
    }
}
