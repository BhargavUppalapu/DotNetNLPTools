using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.WordSegmentaion
{
    interface IWordSegmentation
    {

        List<string> ExtractSegments(string sentence);

        bool LoadModel(string modelFile);

    }
}
