using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Stemming
{
    public interface IStemmer
    {

        string Stem(string Word);

    }
}
