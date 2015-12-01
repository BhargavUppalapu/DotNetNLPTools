using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

            string targetDirectory = @"../../../resources/Corpus/";
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                        P.ProcessWorkItem(fileName, fileName+"_Summary");
        }
    }
}
