namespace TagsCloudContainer.WordFilters
{
    public class ShortWordFilter : IWordFilter
    {
        public IEnumerable<Word> Process(IEnumerable<Word> words)
        {
            return words.Where(w => w.Value.Length > 2);
        }
    }
}
