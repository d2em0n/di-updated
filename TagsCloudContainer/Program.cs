using System.Reflection;
using System.Threading.Channels;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.PointGenerators;
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

            foreach (var imp in FindImplemetations<IPointGenerator>())
            {
                Console.WriteLine(imp.Key + " : " + imp.Value);
            }

            var config = new Config();

            ConfigureCloudView(config);
            Console.WriteLine(config.PointGenerator);
            
        }

        private static void ConfigureCloudView(Config config)
        {
            Console.WriteLine("Выберите внешний вид облака из возможных:");
            var pointGenerators = FindImplemetations<IPointGenerator>();
            foreach (var point in pointGenerators)
                Console.WriteLine(point.Key);
            Console.WriteLine("Введите, соблюдая орфографию");
            var pointGenerator = Console.ReadLine();
            if (pointGenerators.ContainsKey(pointGenerator))
                config.PointGenerator = pointGenerators[pointGenerator];
            else
            {
                Console.WriteLine("Такой формы не предусмотрено");
                ConfigureCloudView(config);
            }
        }

        private static Dictionary<string, Type> FindImplemetations<T>()
            {
                var assembly = Assembly.GetExecutingAssembly();
                var type = typeof(T);
                return assembly.GetTypes()
                    .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
                    .ToDictionary(x => x.GetCustomAttribute<LabelAttribute>().LabelText, x => x);
            }
        }
    }
