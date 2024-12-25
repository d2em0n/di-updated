using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.TextProcessor
{
    public interface ITextProcessor
    {
        public Dictionary<Word, int> Words { get; set; }
    }
}
