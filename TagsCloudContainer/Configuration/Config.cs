using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.PointGenerators;

namespace TagsCloudContainer.Configuration;

public class Config
{
    public Type PointGenerator { get; set; }
    
    public Color? Color { get; set; }

    public string FilePath { get; set; }

    public string PicturePath { get; set; }

    public Point StartPoint { get; set; }

    public Font Font { get; set; }

}