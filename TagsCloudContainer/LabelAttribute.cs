using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class LabelAttribute(string labelText) : Attribute
    {
        public string LabelText { get; set; } = labelText;
    }
}
