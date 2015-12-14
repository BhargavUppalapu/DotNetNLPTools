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
                //string[] lines = Regex.Split(text, "\r\n");
                string[] segmented = config.SentenceDetector.SplitSentences(Regex.Replace(para, "$•", ""));
                int JJCount = 0;
                for (int k = 0; k < segmented.Length; k++)
                {
                    string[] lines = { "" };
                    if (segmented[k].Length > 500)
                    {
                        lines = Regex.Split(segmented[k], "\r\n");
                    }
                    else
                    {
                        lines[0] = segmented[k];
                    }

                    for (int l = 0; l < lines.Length; l++)
                    {
                        Sentence S = new Sentence();
                        S.sent = lines[l];
                        string[] parts2 = config.Tokenizer.Tokenize(lines[l]);
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
                            if (posTag[j] == "NN" || posTag[j] == "NNP")
                            {
                                S.nounCount++;
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
                        JJCount = 0;
                    }
                }
                paraNo++;

            }
        }

        public static void PrepareTraining(Document doc, Configuration config)
        {

            Match m = Regex.Match(doc.Text.ToLower(), doc.SearchString);
            if (m.Success)
            {
                if (m.Value.StartsWith(" "))
                {
                    String matched = doc.Text.Substring(0, m.Index + 1) + "<REGEX>" + m.Value.Trim()
                        + "</REGEX>" + doc.Text.Substring(m.Index + m.Value.Length, doc.Text.Length - m.Index - m.Value.Length);
                    Console.WriteLine(matched);
                }
                else
                {
                    String matched = doc.Text.Substring(0, m.Index) + "<REGEX>" + m.Value + "</REGEX>" + doc.Text.Substring(m.Index + m.Value.Length, doc.Text.Length - m.Index - m.Value.Length);
                    Console.WriteLine(matched);
                }

            }
        }

        public static void extractSearchString(Document doc)
        {
            string[] paragraphs = Regex.Split(doc.Text, @"(Search String|Search string).*", RegexOptions.Multiline);
            string[] searchstring = Regex.Split( Regex.Replace( paragraphs[2] ,".*:",""), @"\r\n", RegexOptions.Multiline);
            doc.SearchString = searchstring[0].Replace(" or ","|").Replace(" and ","|");
            doc.SearchString   = Regex.Replace(  doc.SearchString,"\r", "");
            doc.SearchString = Regex.Replace(doc.SearchString, "\n", "");
            doc.SearchString = doc.SearchString.Replace("\"", "");
            doc.Text = paragraphs[0];
           
        }
    }
}
