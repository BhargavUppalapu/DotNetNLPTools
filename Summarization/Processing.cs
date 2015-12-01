using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Summarization.DataStructures;

namespace Summarization
{
    class Processing
    {
        Utils.Configuration _config = new Utils.Configuration();
        


        public void ProcessWorkItem(string InputFilePath, string outFilePath)
        {

            Document doc = new Document(InputFilePath);
            //Preprocess
            Preprocessor.process(doc, _config);
            //Summerize
            Console.WriteLine("Summerizing the job Descriptions.....\n");
            string summary = _config.Summarizer.Summarize(doc ,_config);
            
            Console.WriteLine("Summary for the Job is as Below:\n{0}",summary);
            //Extract entities...
            Console.Read();
        }

        public Utils.Configuration InitfromConfig(string configurationFile)
        {
            _config.InitializeFromIni(configurationFile);
            return _config;
        }

    }
}
