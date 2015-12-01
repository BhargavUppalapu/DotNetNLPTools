using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Summarization.DataStructures;
using Summarization.Utils;

namespace Summarization.Summarizer
{
    public interface ISummarizer
    {
        string Summarize(Document doc,Configuration config);


    }
}
