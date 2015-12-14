using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Summarization.DataStructures;
using Summarization.Utils;

namespace Summarization.SearchEntityExtractor
{
    interface ISearchEntityExtractor
    {

        void Extract(Document doc, Configuration config);
    }
}
