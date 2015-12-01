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
        void Summarize(Document doc,Configuration config);


    }
}
