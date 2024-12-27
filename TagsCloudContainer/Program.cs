using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new TxtTextProvider();
            var filter = new BoringWordFilter();
            var parser = new RegexParser();
            var processor = new TextProcessor.TextProcessor(@"C:\test\test.txt", provider, parser, filter);
            foreach (var word in processor.Words())
            {
                Console.WriteLine(word.Key.Value + " : " + word.Value);
            }
        }
    }
}
