using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNLP.Tools.PosTagger;


using java.util;
using java.io;



namespace Com.Research.NLPCore.POSTagging
{
    class OpenNLPPOSTagger : IPOSTagger
    {


        /// <summary>
        /// The model file for POS Tagger
        /// </summary>
        private static string _MaxentModelFile = "";
        public string MaxentModelFile
        {
            get { return _MaxentModelFile; }
            set { _MaxentModelFile = value; }
        }


        EnglishMaximumEntropyPosTagger posTagger;



        /// <summary>
        /// Is the model for Stanford named entity recognizer successfully loaded
        /// </summary>
        private bool _isMAxentModelLoaded = false;

        public bool IsMaxentModelLoaded
        {
            get { return _isMAxentModelLoaded; }
            set { _isMAxentModelLoaded = value; }
        }


        public string[] POSTag(string[] Tokens)
        {
            string[] tags = posTagger.Tag(Tokens);
            return tags;
        }






        public bool LoadModel(string ModelFile)
        {

            try
            {
                posTagger = new EnglishMaximumEntropyPosTagger(ModelFile);
                _isMAxentModelLoaded = true;
                return true;
            }
            catch
            {
                System.Console.WriteLine("Uable to load the Stanford CRF Model... ");
                _isMAxentModelLoaded = false;
                return false;
            }
        }
    }
}
