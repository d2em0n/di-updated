namespace TagsCloudContainer.TagGenerator
{
    public interface ITagsGenerator
    {
        Result<IEnumerable<Tag>> GenerateTags(Result<Dictionary<Word, int>> wordsDictionary);
    }
}
