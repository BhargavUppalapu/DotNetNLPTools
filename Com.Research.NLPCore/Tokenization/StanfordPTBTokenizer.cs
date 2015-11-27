using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using edu.stanford.nlp.ie;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.process;


using java.util;
using java.io;


namespace Com.Research.NLPCore.Tokenization
{
    public class StanfordPTBTokenizer : ITokenizer
    {
        public string[] Tokenize(string input)
        {
            try
            {
                PTBTokenizer ptb = new PTBTokenizer(new StringReader(input), new WordTokenFactory(), "normalizeParentheses=false,normalizeOtherBrackets=false,asciiQuotes=true,unicodeQuotes=true,untokenizable=noneDelete");
                object[] tokens = ptb.tokenize().toArray();

                return tokens.Select(t => t.ToString()).ToArray();
            }
            catch (Exception e)
            {
                Logging.ErrorLogger.logError(e.Message);
                return null;

            }
        }
    }
}
