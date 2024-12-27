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
        public bool Skips(Word word)
        {
            return !_forbiddenWords.Contains(word.Value);
        }

        public void AddBoringWord(Word word)
        {
            _forbiddenWords.Add(word.Value);
        }

        public void AddBoringWord(string word)
        {
            _forbiddenWords.Add(word);
        }
    }
}
