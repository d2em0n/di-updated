using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new TxtTextProvider();
            var filter = new SimpleFilter();
            var processor = new TextProcessor.TextProcessor();
            foreach (var word in processor.Words(@"C:\test\test.txt", provider, filter))
            {
                Console.WriteLine(word.Key.Value + " : " + word.Value);
            }
        }
    }
}
