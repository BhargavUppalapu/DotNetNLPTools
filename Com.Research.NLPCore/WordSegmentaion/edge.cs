using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.WordSegmentaion
{
    public class edge
    {
        public int begin;
        public int end;


        public edge(int b, int e)
        {
            begin = b;
            end = e;
        }

    }
}
