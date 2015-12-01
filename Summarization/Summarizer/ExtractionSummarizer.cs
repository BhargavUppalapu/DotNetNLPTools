using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Summarization.Utils;
using Summarization.DataStructures;


namespace Summarization.Summarizer
{
    class ExtractionSummarizer : ISummarizer
    {
        public string Summarize(DataStructures.Document doc, Configuration config)
        {
            string summary = "";
            TFIDF.Transform(doc, config, 0);
            Dictionary<int, double> sentenceScores = new Dictionary<int, double>();
            Dictionary<int, string> sentences = new Dictionary<int, string>();
            foreach (Sentence S in doc.sentences)
            {
                if (S.paraHeading == "" | S.paraHeading.ToLower().Contains("description"))
                {
                    S.score = S.TFIDFScore + S.Lenght * 0.01 + S.JJCount * 0.03 + (1 / S.NoSent) + S.upperCaseLettersCount * 0.2;
                    // Console.WriteLine("Sentence: {0} \t Score: {1}\n", S.NoSent, S.score);
                    sentenceScores.Add(S.NoSent, S.score);
                    sentences.Add(S.NoSent, S.sent);
                }
            }
            var sortedsents = from sent in sentenceScores orderby sent.Value descending select sent;
            int top = 0;
            foreach (var sortedSent in sortedsents)
            {
                if(top > 4)
                    break;
                summary = summary + sentences[sortedSent.Key].ToString();
                //Console.WriteLine("{0}",sentences[sortedSent.Key].ToString());
                top++;
            }
            

            //?? Future work
            //Imporrtant Words count
            //Nouns Count
            //Verbs Count

            return summary;
        }
    }
}

