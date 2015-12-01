using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Summarization.DataStructures;

namespace Summarization
{
    class Processing
    {
        Utils.Configuration _config = new Utils.Configuration();
        


        public void ProcessWorkItem(string InputFilePath, string outFilePath)
        {
            FileStream fs1 = new FileStream(outFilePath, FileMode.Create);
            StreamWriter sw1 = new StreamWriter(fs1);
            
            Document doc = new Document(InputFilePath);
            //Preprocess
            Preprocessor.process(doc, _config);
            //Summerize
            Console.WriteLine("Summerizing the job Descriptions.....\n");
            _config.Summarizer.Summarize(doc ,_config);
            Console.WriteLine("\nDescription:\n{0}\n\nMust Have:\n{1}\n\nGood To Have:\n{2}\n\n", doc.description,doc.MustHave,doc.GoodTohave);
            string allResults = "\nDescription:\n" + doc.description + "\n\nMust Have:\n" + doc.MustHave + "\n\nGood To Have:\n" + doc.GoodTohave + "\n\n";
            sw1.WriteLine(allResults);
            //Extract entities...
            sw1.Close();
        }

     
        public Utils.Configuration InitfromConfig(string configurationFile)
        {
            _config.InitializeFromIni(configurationFile);
            return _config;
        }

    }
}
