using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Summarization.Utils;
using Summarization.DataStructures;

namespace Summarization.Summerizer
{
    interface ISummerizer
    {
        void Summarize(Configuration config, List<Document> Jobs);
    }
}
