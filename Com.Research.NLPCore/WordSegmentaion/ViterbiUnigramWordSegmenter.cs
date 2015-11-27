using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Com.Research.NLPCore.Logging;


namespace Com.Research.NLPCore.WordSegmentaion
{
    public class ViterbiUnigramWordSegmenter : IWordSegmentation
    {


        Dictionary<string, decimal> probabilities = new Dictionary<string, decimal>();

        
        public List<string> ExtractSegments(string sentence)
        {
             //Forward Step
            List<edge> best_edge = new List<edge>();
            for (int i = 0; i < sentence.Length; i++)
            {
                best_edge.Add(null);
            }

            decimal[] best_score = new decimal[sentence.Length];
            //best_edge[0] = null;
            //best_score[0] = 0m;

            for (int word_end = 1; word_end <= sentence.Length - 1; word_end++)
            {

                best_score[word_end] = 10000000;
                for (int word_begin = 0; word_begin < word_end - 1; word_begin++)
                {
                    string word = sentence.Substring(word_begin, word_end - word_begin + 1);
                    if (probabilities.ContainsKey(word) || word.Length == 1)
                    {
                        decimal prob = probabilities[word];
                        decimal logProb = (decimal)Math.Log((double)prob, (double)2);
                        decimal my_score;
                        if (word_begin > 0)
                        {
                            my_score = ((decimal)best_score[word_begin - 1] - logProb);
                        }
                        else
                        {
                            my_score = ((decimal)best_score[word_begin] - logProb);
                        }
                        if (my_score < best_score[word_end])
                        {
                            best_score[word_end] = my_score;
                            edge ne = new edge(word_begin, word_end);
                            best_edge[word_end] = ne;
                        }
                    }
                }
            }


            //Backward Step
            List<string> words = new List<string>();
            edge next_edge = best_edge[best_edge.Count - 1];
            while (next_edge != null)
            {
                string word = sentence.Substring(next_edge.begin, (next_edge.end - next_edge.begin) + 1);
                words.Add(word);
                int next;
                if(next_edge.begin >0)
                 next = next_edge.begin -1;
                else
                 next = 0;
                next_edge = best_edge[next];
            }
            words.Reverse();


            return words;
        }



        public bool LoadModel(string modelFile)
        {

            try
            {
                using (StreamReader sr_as = new StreamReader(modelFile, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr_as.ReadLine()) != null)
                    {
                        string[] wordsInline = line.Split('\t');
                        probabilities[wordsInline[0]] = Convert.ToDecimal(wordsInline[1]);
                    }

                }
                return true;
            }
            catch(Exception ex)
            {
                //Log the error and then return false. 
                ErrorLogger.logError(ex.Message);
                return false;
            }


        }

        public static void trainUnigram(string dictFilePath, string modelFilePath)
        {

            FileStream fs = new FileStream(modelFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

            List<string> lines = new List<string>();

            Dictionary<string, int> counts = new Dictionary<string, int>();
            int total_count = 0;

            using (StreamReader sr_as = new StreamReader(dictFilePath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr_as.ReadLine()) != null)
                {
                    List<string> words = new List<string>();
                    foreach (string word in line.Split(' '))
                    {
                        words.Add(word);
                        if (counts.ContainsKey(word))
                        {
                            counts[word]++;
                        }
                        else
                        {
                            counts.Add(word, 1);
                        }
                        total_count++;
                    }
                    words.Add("</S>");
                    // counts.Add("</S>", 1);

                }
            }

            foreach (string word in counts.Keys)
            {
                float prob;
                prob = (float)counts[word] / (float)total_count;
                sw.WriteLine(word + "\t" + prob);
            }

            sw.Close();
        }


        public static void testAndPrint(string testFilePath, string modelPath)
        {
            Dictionary<string, decimal> probabilities = new Dictionary<string, decimal>();
            using (StreamReader sr_as = new StreamReader(modelPath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr_as.ReadLine()) != null)
                {
                    string[] words = line.Split('\t');
                    probabilities[words[0]] = Convert.ToDecimal(words[1]);
                }

            }

            Decimal lambda1 = 0.95m;
            Decimal lambdaunk = 1 - lambda1;
            Decimal V = 1000000m;
            int W = 0; //No of words
            Decimal H = 0m; //variable used to calculate entrophy;
            Decimal P;
            int unk = 0;
            using (StreamReader sr_as = new StreamReader(testFilePath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr_as.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    words[words.Length + 1] = "</S>";

                    for (int i = 0; i < words.Length; i++)
                    {

                        W++;
                        P = lambdaunk / V;

                        if (probabilities.ContainsKey(words[i]))
                        {
                            P = P + (lambda1 * probabilities[words[i]]);
                        }
                        else
                        {
                            unk++;
                        }

                        H = (Decimal)((double)H + (-Math.Log((double)P, 2)));

                    }
                    Console.WriteLine("entrophy = " + Convert.ToString(H / W));
                    Console.WriteLine("Coverage = " + Convert.ToString((W - unk) / W));
                }
            }

        }


    }
}
