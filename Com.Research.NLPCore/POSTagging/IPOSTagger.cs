using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.POSTagging
{
    public interface IPOSTagger
    {

        string[] POSTag(string[] tokens);
        bool LoadModel(string ModelFile);


    }
}
