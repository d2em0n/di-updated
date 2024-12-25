using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.TextProviders
{
    public interface ITextProvider
    {
        public string ReadFile(string filePath);
    }
}
