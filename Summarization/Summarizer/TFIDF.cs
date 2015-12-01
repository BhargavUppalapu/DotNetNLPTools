using EnglishStemmer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Summarization.DataStructures;
using Summarization.Utils;

namespace Summarization.Summarizer
{
    /// <summary>
    /// Description:
    /// Performs a TF*IDF (Term Frequency * Inverse Document Frequency) transformation on an array of documents.
    /// Each document string is transformed into an array of doubles, cooresponding to their associated TF*IDF values.

    /// </summary>
    public static class TFIDF
    {

        //Total unique words in the document along with Inverse Document Frequency.
        private static Dictionary<string, double> _IDF = new Dictionary<string, double>();


        public static void Transform(Document doc, Configuration config, int vocabularyThreshold = 3)
        {
            List<List<string>> stemmedDocs;
            List<string> vocabulary;

            // Get the vocabulary and stem the documents at the same time.

            vocabulary = GetVocabulary(doc, out stemmedDocs, config, vocabularyThreshold);

            if (_IDF.Count == 0)
            {
                // Calculate the IDF for each vocabulary term.
                foreach (var term in vocabulary)
                {
                    double numberOfDocsContainingTerm = stemmedDocs.Where(d => d.Contains(term)).Count();
                    _IDF[term] = Math.Log((double)stemmedDocs.Count / ((double)1 + numberOfDocsContainingTerm));
                }
            }

            // Transform each document into a vector of tfidf values.
            TransformToTFIDFVectors(doc, config, _IDF);
        }

        private static void TransformToTFIDFVectors(Document doc, Configuration config, Dictionary<string, double> vocabularyIDF)
        {

            foreach (Sentence S in doc.sentences)
            {

                List<double> vector = new List<double>();

                foreach (var vocab in vocabularyIDF)
                {
                    // Term frequency = count how many times the term appears in this document.
                    double tf = S.words.Where(d => d.word == vocab.Key).Count();
                    double tfidf = tf * vocab.Value;
                    vector.Add(tfidf);
                }
                double[] tfids = vector.Select(v => v).ToArray();
                tfids = L2Normalization.Normalize(tfids);
                foreach (double tfidf in tfids)
                {
                    S.TFIDFScore = S.TFIDFScore + tfidf;
                }
            }

        }


        private static List<string> GetVocabulary(Document doc, out List<List<string>> stemmedDocs, Configuration config, int vocabularyThreshold)
        {
            List<string> vocabulary = new List<string>();
            Dictionary<string, int> wordCountList = new Dictionary<string, int>();
            stemmedDocs = new List<List<string>>();

            int docIndex = 0;
            foreach (Sentence sentence in doc.sentences)
            {
                List<string> stemmedDoc = new List<string>();
                docIndex++;
                //string[] parts2 = config.Tokenizer.Tokenize(sentence.sent);//??????
                //List<string> words = new List<string>();
                foreach (wordDetails part in sentence.words)
                {
                    // Strip non-alphanumeric characters.
                    string stripped = Regex.Replace(part.word, "[^a-zA-Z0-9]", "");
                    if (!StopWords.stopWordsList.Contains(stripped.ToLower()))
                    {
                        try
                        {
                            string stem = config.Stemmer.Stem(stripped);
                            if (stem.Length > 0)
                            {
                                // Build the word count list.
                                if (wordCountList.ContainsKey(stem))
                                {
                                    wordCountList[stem]++;
                                }
                                else
                                {
                                    wordCountList.Add(stem, 0);
                                }
                                stemmedDoc.Add(stem);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("There is some error in Stemming");
                        }
                    }

                }
                stemmedDocs.Add(stemmedDoc);
            }


            // Get the top words.
            var vocabList = wordCountList.Where(w => w.Value >= vocabularyThreshold);

            foreach (var item in vocabList)
            {
                vocabulary.Add(item.Key);
            }

            return vocabulary;


        }
    }

}
