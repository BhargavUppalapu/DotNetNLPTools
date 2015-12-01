using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


using Summarization.Utils;
using Summarization.DataStructures;


namespace Summarization.Summarizer
{
    class ExtractionSummarizer : ISummarizer
    {
        public void Summarize(DataStructures.Document doc, Configuration config)
        {

            TFIDF.Transform(doc, config, 0);
            DescriptionSummary(doc);
            MustHaveSummary(doc);
            GoodToHaveSummary(doc);
            //?? Future work
            //Imporrtant Words count
            //Nouns Count
            //Verbs Count

        }


        void DescriptionSummary(Document doc)
        {

            string summary = "";

            Dictionary<int, double> DescriptionSentScores = new Dictionary<int, double>();
            Dictionary<int, string> sentences = new Dictionary<int, string>();
            foreach (Sentence S in doc.sentences)
            {
                double isDescription = 0;
                //string parsedOutput = config.Parser.parse(S.sent.ToLower());
                if (S.paraHeading.ToLower().Contains("description"))
                {
                    isDescription = 5;
                }
                //This must be done more effectively using any Classification ML algorithm. For that we need manually annotated data. So leaving that for future..
                S.Descriptionscore = S.TFIDFScore + S.Lenght * 0.001 + S.JJCount * 0.003 + (1 / S.NoSent) * 0.5 + S.upperCaseLettersCount * 0.05 + isDescription;

                // Console.WriteLine("Sentence: {0} \t Score: {1}\n", S.NoSent, S.score);
                DescriptionSentScores.Add(S.NoSent, S.Descriptionscore);
                sentences.Add(S.NoSent, S.sent);
            }


            var sortedsents = from sent in DescriptionSentScores orderby sent.Value descending select sent;
            int top = 0;
            SortedDictionary<int, string> extracted = new SortedDictionary<int, string>();
            foreach (var sortedSent in sortedsents)
            {
                if (top > 4)
                    break;
                //summary = summary + sentences[sortedSent.Key].ToString() + "\n";
                extracted.Add(sortedSent.Key, sentences[sortedSent.Key].ToString());
                //Console.WriteLine("{0}",sentences[sortedSent.Key].ToString());
                top++;
            }

            foreach (var sent in extracted)
            {
                summary = summary + sent.Value;
            }
            doc.description = summary;

        }


        void MustHaveSummary(Document doc)
        {

            Dictionary<int, double> QualificationSentScores = new Dictionary<int, double>();
            Dictionary<int, string> Qualificationsentences = new Dictionary<int, string>();
            foreach (Sentence S in doc.sentences)
            {
                //Getting the Must Have scores...
                double isQualifiation = 0;
                if (S.paraHeading.ToLower().Contains("qualification") ||
                    !S.paraHeading.ToLower().Contains("description"))
                {
                    isQualifiation = 10;
                }
                string MustHaveRegex = "(must|required|should|minimum|strong)";
                Match m = Regex.Match(S.sent, MustHaveRegex);
                double MustHaveWords = 0;
                if (m.Success)
                {
                    MustHaveWords = 5;
                }
                S.Qualificationscore = S.TFIDFScore + S.Lenght * 0.001 + S.JJCount * 0.003 + S.nounCount * 0.05 + S.upperCaseLettersCount * 0.5 + isQualifiation + MustHaveWords;
                QualificationSentScores.Add(S.NoSent, S.Qualificationscore);
                Qualificationsentences.Add(S.NoSent, S.sent);
            }
            string qualificationSumm = "";
            int Qtop = 0;

            var qualSortedSents = from sent in QualificationSentScores orderby sent.Value descending select sent;
            foreach (var sortedSent in qualSortedSents)
            {
                if (Qtop > 3)
                    break;
                qualificationSumm = qualificationSumm + Qualificationsentences[sortedSent.Key].ToString() + "\n";
                //Console.WriteLine("{0}",sentences[sortedSent.Key].ToString());
                Qtop++;
            }
            doc.MustHave = qualificationSumm;

        }


        void GoodToHaveSummary(Document doc)
        {

            Dictionary<int, double> QualificationSentScores = new Dictionary<int, double>();
            Dictionary<int, string> Qualificationsentences = new Dictionary<int, string>();
            foreach (Sentence S in doc.sentences)
            {
                //Getting the Must Have scores...
                double isQualifiation = 0;
                if (S.paraHeading.ToLower().Contains("qualification") ||
                    !S.paraHeading.ToLower().Contains("description"))
                {
                    isQualifiation = 10;
                }
                string MustHaveRegex = "(must|required|should|minimum|strong)";
                Match m = Regex.Match(S.sent, MustHaveRegex);
                double MustHaveWords = 0;
                if (!m.Success)
                {
                    MustHaveWords = 5;
                }
                S.Qualificationscore = S.TFIDFScore + S.Lenght * 0.001 + S.JJCount * 0.003 + S.nounCount * 0.05 + S.upperCaseLettersCount * 0.5 + isQualifiation + MustHaveWords;
                QualificationSentScores.Add(S.NoSent, S.Qualificationscore);
                Qualificationsentences.Add(S.NoSent, S.sent);
            }
            string qualificationSumm = "";
            int Qtop = 0;

            var qualSortedSents = from sent in QualificationSentScores orderby sent.Value descending select sent;
            foreach (var sortedSent in qualSortedSents)
            {
                if (Qtop > 2)
                    break;
                qualificationSumm = qualificationSumm + Qualificationsentences[sortedSent.Key].ToString() + "\n";
                //Console.WriteLine("{0}",sentences[sortedSent.Key].ToString());
                Qtop++;
            }
            doc.GoodTohave = qualificationSumm;

        }


    }
}

