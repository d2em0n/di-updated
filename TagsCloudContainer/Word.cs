namespace TagsCloudContainer
{
    public class Word(string word)  
    {
        public string Value { get; set; } = word;

        public override bool Equals(object? obj)
        {
            var other = obj as Word;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
