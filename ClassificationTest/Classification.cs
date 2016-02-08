using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Research;
//using Com.Research.ML.VowpalWabbit;
using Com.Research.ML;


namespace ClassificationTest
{
    public class Classification : IDisposable
    {
       private VowpalWabbit _vw;
        private WordDictionary _dictionary;
        private float _cutoff;

        public Classification(string modelName, string dictionary, int dictionarySize, float cutoff)
        {
            //--quiet
           _vw = new VowpalWabbit(string.Format("-t -i {0} --quiet", modelName));
           _dictionary = new WordDictionary(dictionary, dictionarySize);
           _cutoff = cutoff;
        }


        public void Predict(string features)
        {
            float predicted = _vw.Learn(features);
        
        }
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
