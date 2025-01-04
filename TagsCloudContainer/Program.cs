using System.Drawing;
using System.Reflection;
using System.Threading.Channels;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;

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
            foreach (var word in processor.WordFrequencies())
            {
                Console.WriteLine(word.Key.Value + " : " + word.Value);
            }

            foreach (var imp in FindImplemetations<IPointGenerator>())
            {
                Console.WriteLine(imp.Key + " : " + imp.Value);
            }

            var config = new Config();

            //ConfigureCloudView(config);

            ConfigureColor(config);
            Console.WriteLine(config.Color);
            Console.WriteLine(config.RandomColor);

        }

        private static void ConfigureColor(Config config)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Выборите цвет из возможных:");
            var colors = Enum.GetValues(typeof(RainbowColors));
            foreach (var color in colors)
            {
                var fieldInfo = color.GetType().GetField(color.ToString());
                var attribute = (LabelAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(LabelAttribute));

                var label = attribute.LabelText;
                Console.WriteLine(label);
            }

            Console.WriteLine("В случае неправильного ввода - цвет будет выбираться случайным образом");
            var inp = Console.ReadLine();
            switch (inp)
            {
                case "Красный":
                    config.Color = Color.Red;
                    break;
                case "Оранжевый":
                    config.Color = Color.Orange;
                    break;
                case "Желтый":
                    config.Color = Color.Yellow;
                    break;
                case "Зеленый":
                    config.Color = Color.Green;
                    break;
                case "Голубой":
                    config.Color = Color.Blue;
                    break;
                case "Синий":
                    config.Color = Color.Indigo;
                    break;
                case "Фиолетовый":
                    config.Color = Color.Violet;
                    break;

                default:
                    Console.WriteLine("Цвет будет выбираться случайным образом");
                    config.RandomColor = true;
                    break;
            }




            //// Запрашиваем у пользователя ввод цвета
            //Console.Write("Введите цвет текста (например, Red, Green): ");
            //string userInput = Console.ReadLine();

            //// Пробуем преобразовать ввод в ConsoleColor
            //if (Enum.TryParse(userInput, true, out ConsoleColor selectedColor))
            //{
            //    // Устанавливаем цвет текста
            //    Console.ForegroundColor = selectedColor;

            //    // Выводим сообщение с выбранным цветом
            //    Console.WriteLine("Выбранный цвет текста: " + userInput);
            //}
            //else
            //{
            //    Console.WriteLine("Некорректный цвет. Попробуйте снова.");
            //}

            //// Сбрасываем цвет текста на стандартный
            //Console.ResetColor();

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
