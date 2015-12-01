using EnglishStemmer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Stemming
{
    class EnglishStemmer : IStemmer
    {
        public string Stem(string Word)
        {

            var english = new EnglishWord(Word);
            string stem = english.Stem;

            return stem;
                           

        }
    }
}
