using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Com.Research.NLPCore.Classification
{
    public class VWClassifier : IClassifier
    {

        ////private static readonly object synclock = new object();
        ////protected string modelFilePath = null;

        ////private string _regexMatchNameSpace = "";
        //////protected IntPtr vw;          
        ////private VowpalWabbit vw;
        ////protected HashSet<string> stopWords = null;

        
        public void preprocessing(string doc )
        {
        ////    lock (synclock)
        ////    {
        ////        sentences = config.SentenceDetector.SplitSentences(doc.Text);
        ////    }
        ////    if (sentences != null)
        ////    {
        ////        foreach (var sentence in sentences)
        ////        {
        ////            doc.Sentences.Add(sentence);
        ////        }
        ////    }
        ////    if (string.IsNullOrEmpty(config.RegexToMatch))
        ////        throw new Exception("Regex cannot be null");
        ////    _regex = new Regex(config.RegexToMatch, RegexOptions.Compiled);
        ////    _regexMatchNameSpace = config.RegexMatchNameSpace;
        ////    stopWords = new HashSet<string>();
        ////    try
        ////    {
        ////        using (StreamReader reader = new StreamReader(config.StopWordsFile))
        ////        {
        ////            while (reader.EndOfStream == false)
        ////            {
        ////                stopWords.Add(reader.ReadLine());
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new ApplicationException(
        ////            string.Format("Error processing stop words list: {0}",
        ////                ex.Message ?? string.Empty));
        ////    }

        }

        ////*/
        ////public string extractFeatures(string doc)
        ////{

        ////    //Read the Test Features file...


        ////    HashSet<string> positiveFeatures = new HashSet<string>();
        ////    HashSet<string> otherFeatures = new HashSet<string>();
        ////    ////foreach (var sentence in sentences)
        ////    ////{
        ////    ////    if (sentence.Length < 1000)
        ////    ////    {
        ////    ////        bool positiveMatch = false;
        ////    ////        if (_regex.IsMatch(sentence))
        ////    ////        {
        ////    ////            positiveMatch = true;
        ////    ////            AddUnigramFeatures(sentence, positiveFeatures, stopWords);
        ////    ////        }
        ////    ////        if (!positiveMatch)
        ////    ////            AddUnigramFeatures(sentence, otherFeatures, stopWords);
        ////    ////    }
        ////    ////}

        ////    StringBuilder sb = new StringBuilder();
        ////    sb.Append(" |" + _regexMatchNameSpace + " ");
        ////    foreach (string word in positiveFeatures)
        ////    {
        ////        sb.AppendFormat(" {0}:1", word);
        ////    }

        ////    sb.Append(" |Other ");
        ////    foreach (string word in otherFeatures)
        ////    {
        ////        sb.AppendFormat(" {0}:1", word);
        ////    }
        ////    sb.AppendLine();

        ////    return sb.ToString();
        ////}

        ////private static void AddUnigramFeatures(string sentence, HashSet<string> featureSet, ICollection<string> stopWords)
        ////{
        ////    string[] words = sentence.Split();
        ////    List<string> cleanWords = words.Where(word => !string.IsNullOrWhiteSpace(word) && !Regex.IsMatch(word, @"[^\w]") && !word.Contains(":") && !word.Contains("|") && !stopWords.Contains(word)).ToList();
        ////    featureSet.UnionWith(cleanWords);
        ////}

        public float classify(string doc, string modelFile)
        {

            ////    String featureString = extractFeatures(doc);
            ////    float prediction = 0.0f;
            ////    try
            ////    {
            ////        prediction = vw.Learn(featureString); //VowpalWabbitInterface.Learn(vw, example);
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        throw new ApplicationException(
            ////            string.Format("Error evaluating content predicton: {0}",
            ////                ex),
            ////            ex);
            ////    }

            ////    return prediction;
            return 0.0f;
        }

        public void LoadModel(string modelPath)
        {

            ////    try
            ////    {
            ////        modelFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(modelPath));
            ////        File.Copy(modelPath, modelFilePath, true);
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        throw new ApplicationException(
            ////            string.Format("Unable to create temporary training model file copy: {0}",
            ////                ex.ToString()),
            ////            ex);
            ////    }

            ////   // string commandArgs = string.Format("-t -i {0} --quiet", modelFilePath);
            ////    //_vw = new VowpalWabbit(string.Format("-f {0} --loss_function logistic --passes 25 -c -l2",modelName));
            ////    //vw = new VowpalWabbitInterface.Initialize(commandArgs);
            ////    vw = new VowpalWabbit(string.Format("-t -i {0} --quiet", modelFilePath));
        }



    }
}
