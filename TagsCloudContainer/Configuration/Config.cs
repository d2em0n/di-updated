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

    public bool RandomColor;
    public Color Color { get; set; }

}