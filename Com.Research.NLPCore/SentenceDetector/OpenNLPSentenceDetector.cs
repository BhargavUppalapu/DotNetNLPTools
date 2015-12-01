using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNLP.Tools.SentenceDetect;

namespace Com.Research.NLPCore.SentenceDetector
{
    class OpenNLPSentenceDetector : IsentenceDetector
    {

        private string _maxentModelFile;
        private MaximumEntropySentenceDetector _openNLPSentenceDetector;


        private bool _isMaxentModelLoaded = false;

        public bool IsMaxentModelLoaded
        {
            get { return _isMaxentModelLoaded; }
            set { _isMaxentModelLoaded = value; }
        }

        public string MaxentModelFile
        {
            get { return _maxentModelFile; }
            set { _maxentModelFile = value; }
        }


        public string[] SplitSentences(string text)
        {
            if (!_isMaxentModelLoaded)
                return null;

            try
            {
                return _openNLPSentenceDetector.SentenceDetect(text);
            }
            catch
            {
                return null;
            }
        }

        public bool LoadModel(string model)
        {
            try
            {
                _openNLPSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(model);
                _isMaxentModelLoaded = true;
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unable to Load the Maxent model" + e.ToString());
                _isMaxentModelLoaded = false;
                return false;
            }
        }
    }
}
