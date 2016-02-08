using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
//using Com.Research.ML.VowpalWabbit;
using Com.Research.ML.TextExtraction;

namespace Com.Research.ML.TextExtraction
{
    public class DisclaimerPredict : IDisposable
    {
        private VowpalWabbit _vw;
        private WordDictionary _dictionary;
        private float _cutoff;

        public DisclaimerPredict(string modelName, string dictionary, int dictionarySize, float cutoff)
        {
            //--quiet
            // _vw = new VowpalWabbit(string.Format("-t -i {0} --quiet", modelName));
            _vw = new VowpalWabbit(string.Format("-f {0} --loss_function logistic --passes 25 -c -l2", modelName));
            _dictionary = new WordDictionary(dictionary, dictionarySize);
            _cutoff = cutoff;
        }


        public void Predict(string features)
        {

            float predicted = _vw.Learn(features);

        }



        ////public static string ParagraphVWFeatures(WordDictionary dictionary, string paragraph, int page, int pageCount)
        ////{
        ////    StringBuilder features = new StringBuilder();

        ////    //add word features
        ////    var wordFeatures = dictionary.ToFeaturesVector(TokenizeText(paragraph));

        ////    features.Append("|w");
        ////    for (int i = 0; i < wordFeatures.Count; ++i)
        ////        if (wordFeatures[i])
        ////            features.AppendFormat(" {0}", i + 1);

        ////    //add page features
        ////    features.AppendFormat(" |f 1:{0} 2:{1} 3:{2}", pageCount, page, ((double)page) / pageCount);
        ////    return features.ToString();
        ////}

        ////public static IEnumerable<string> TokenizeText(string text)
        ////{
        ////    return text.Split(' ', '\n', '\r').Select(w => TokenClass.NormalizeToken(w)).Where(w => !string.IsNullOrEmpty(w));
        ////}

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vw.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
