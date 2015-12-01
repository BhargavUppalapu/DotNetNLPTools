using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


using Summarization.DataStructures;
using Summarization.Utils;

namespace Summarization
{
    public static class Preprocessor
    {

        public static void process(Document doc, Configuration config)
        {

            //Split into Paragraphs

            string[] paragraphs = Regex.Split(doc.Text, @"^\s*$", RegexOptions.Multiline);
            string Heading = "";
            int paraNo = 0;
            int sentNo = 0;
            foreach (string para in paragraphs)
            {
                //Paragraph p = new Paragraph();
                string HeadingRegex = "((.*):|.*Job Description.*\n|.*Job Summary.*)";//Have to add more Known headings so that we can find the headings if they are not followed by :
                Match m = Regex.Match(para, HeadingRegex);
                if (m.Success)
                {
                    Heading = m.Value;
                }
                //p.text = para;
                //p.heading = Heading;

                string[] segmented = config.SentenceDetector.SplitSentences(para);
                int JJCount = 0;
                for (int k = 0; k < segmented.Length; k++)
                {
                    Sentence S = new Sentence();
                    S.sent = segmented[k];
                    string[] parts2 = config.Tokenizer.Tokenize(segmented[k]);//??????
                    string[] posTag = config.POSTagger.POSTag(parts2);
                    S.Lenght = parts2.Count();
                    for (int j = 0; j < parts2.Count(); j++)
                    {
                        wordDetails wordDetail = new wordDetails();
                        string stem = config.Stemmer.Stem(parts2[j]);
                        wordDetail.word = parts2[j];
                        wordDetail.stem = stem;
                        wordDetail.pOSTag = posTag[j];
                        if (posTag[j] == "JJ")
                        {
                            JJCount++;
                        }
                        S.words.Add(wordDetail);
                    }
                    int upper = 0;
                    for (int loop = 0; loop < segmented[k].Length; loop++)
                    {
                        if (Char.IsUpper(segmented[k][loop]))
                            upper++;
                    }
                    S.upperCaseLettersCount = upper;
                    S.NoSent = sentNo + 1;
                    S.JJCount = JJCount;
                    S.paraNo = paraNo;
                    S.paraHeading = Heading;
                    //p.sentences.Add(S);
                    doc.sentences.Add(S);
                    sentNo++;
                }
                paraNo++;
                
            }
        }

    }
}
