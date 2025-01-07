namespace TagsCloudContainer.WordFilters
{
    public class BoringWordFilter : IWordFilter
    {
        private readonly HashSet<string> _forbiddenWords =
        [
            "я",
            "мы",
            "он",
            "она",
            "оно",
            "они"
        ];

        public void AddBoringWord(Word word)
        {
            _forbiddenWords.Add(word.Value);
        }

        public void AddBoringWord(string word)
        {
            _forbiddenWords.Add(word);
        }

        public IEnumerable<Word> Process(IEnumerable<Word> words)
        {
            return words.Where(w => !_forbiddenWords.Contains(w.Value));
        }
    }
}
