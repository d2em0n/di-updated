using System.Drawing;

namespace TagsCloudContainer.Configuration;

public class Config
{
    public Type PointGenerator { get; set; }
    
    public Color? Color { get; set; }

    public string FilePath { get; set; }

    public string PicturePath { get; set; }

    public Point StartPoint { get; set; }

    public Font Font { get; set; }

    public Dictionary<string, Type> SupportedReadingFormats { get; set; }

}