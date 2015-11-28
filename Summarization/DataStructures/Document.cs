using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summarization.DataStructures
{


    public class wordDetails
    {
        public string word;
        public string POSTag;
        public int nameGroup;
        public List<string> trends = new List<string>();

    }

    
    class Sentence
    {
        public string sent ="";
        public string parsedOut = "";
        public List<wordDetails> words = new List<wordDetails>();

        public int rank;
    }

    class Document
    {
        public List<Sentence> sentences = new List<Sentence>();
    }
}
