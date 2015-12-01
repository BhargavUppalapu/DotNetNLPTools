using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summarization
{
    class Program
    {
        static void Main(string[] args)
        {
            //Summerize
            ////Extract the Most important and Good to have from Job description..
            Processing P = new Processing();
            P.InitfromConfig(@"../../../resources/configuration/config.ini");

            P.ProcessWorkItem(@"../../../resources/Corpus/Job-1" , @"../../../resources/Corpus/Job-1_Summary");
        }
    }
}
