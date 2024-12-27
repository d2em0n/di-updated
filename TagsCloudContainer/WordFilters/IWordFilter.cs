namespace TagsCloudContainer.WordFilters
{
    public interface IWordFilter
    {
        public bool Skips(Word word);
    }
}
