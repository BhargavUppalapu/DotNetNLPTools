using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Research.NLPCore;
using Com.Research.NLPCore.NamedEntityRecognition;
using Com.Research.NLPCore.POSTagging;
using Com.Research.NLPCore.Tokenization;
using Com.Research.NLPCore.Stemming;
using Com.Research.NLPCore.SentenceDetector;
using Summarization.Summarizer;


namespace Summarization.Utils
{
    public class Configuration
    {
        
        public string InputFileName;
        public string OutputFolder;
        public string NaesForTrends;


        private String _configName;
        public String ConfigName
        {
            get { return _configName; }

        }
        public Configuration() { }


        /// <summary>
        /// Name for Tokenizer
        /// </summary>
        private string _tokenizerName = null;
        private bool _isTokenizerCreated = false;
        private readonly FactoryClass<ITokenizer> _myTokenizerFactory = new FactoryClass<ITokenizer>();
        private ITokenizer _myTokenizer;

        public ITokenizer Tokenizer
        {
            get { return _myTokenizer; }
        }


        /// <summary>
        /// Name for SentenceDetector
        /// </summary>
        private string _sentneceDetectorName = null;
        private string _sentenceDetectorModelFile;
        private bool _isSentenceDetectorCreated = false;
        private readonly FactoryClass<IsentenceDetector> _mySentenceDetectorFactory = new FactoryClass<IsentenceDetector>();
        private IsentenceDetector _mySentenceDetector;

        public IsentenceDetector SentenceDetector
        {
            get { return _mySentenceDetector; }
        }



        /// <summary>
        /// Name for Stemmer
        /// </summary>
        private string _stemmerName = null;
        private bool _isStemmerCreated = false;
        private readonly FactoryClass<IStemmer> _myStemmerFactory = new FactoryClass<IStemmer>();
        private IStemmer _myStemmer;

        public IStemmer Stemmer
        {
            get { return _myStemmer; }
        }




        /// <summary>
        /// Name for POS Tagger
        /// </summary>
        private string _posTaggerName = null;
        private bool _isposTaggerCreated = false;
        private string _posTaggerModelFile;

        private readonly FactoryClass<IPOSTagger> _myPOSTaggerFactory = new FactoryClass<IPOSTagger>();
        private IPOSTagger _myposTagger;

        public IPOSTagger POSTagger
        {
            get { return _myposTagger; }
        }


        /// <summary>
        /// Name for Summarizer
        /// </summary>
        private string _summarizerName = null;
        private bool _issummarizerCreated = false;
        private string _summarizerModelFile;
        private readonly FactoryClass<ISummarizer> _mySummarizerFactory = new FactoryClass<ISummarizer>();
        private ISummarizer _mysummarizer;

        public ISummarizer Summarizer
        {
            get { return _mysummarizer; }
        }


        public void InitiateModules()
        {


            //Initializing Stanford CRFNER model.
            if (!string.IsNullOrEmpty(_tokenizerName))
                _isTokenizerCreated = CreateTokenizer();
            
            if (!string.IsNullOrEmpty(_posTaggerName))
                _isposTaggerCreated = CreatePOSTagger();


            if (!string.IsNullOrEmpty(_summarizerName))
                _issummarizerCreated = CreateSummerizer();


            if (!string.IsNullOrEmpty(_stemmerName))
                _isStemmerCreated = CreateStemmer();


            if (!string.IsNullOrEmpty(_sentneceDetectorName))
                _isSentenceDetectorCreated = CreateSentenceDetector();

        }

        public bool CreateSentenceDetector()
        {
            _mySentenceDetector = _mySentenceDetectorFactory.Create(_sentneceDetectorName);
            return (_mySentenceDetector.LoadModel(_sentenceDetectorModelFile) != null) ? true : false;
        }
        public bool CreateStemmer()
        {
            _myStemmer = _myStemmerFactory.Create(_stemmerName);
            return (_myStemmer != null) ? true : false;
        }
        public bool CreateSummerizer()
        {
            _mysummarizer = _mySummarizerFactory.Create(_summarizerName);
            return (_mysummarizer != null) ? true : false;
        }

        public bool CreateTokenizer()
        {
            _myTokenizer = _myTokenizerFactory.Create(_tokenizerName);
            return (_myTokenizer != null) ? true : false;
        }

        public bool CreatePOSTagger()
        {
            Console.WriteLine("Loading POS Tagger Model. This may take few seconds.");
            _myposTagger = _myPOSTaggerFactory.Create(_posTaggerName);
            return (_myposTagger.LoadModel(_posTaggerModelFile));
        }

        //Summarizer..
        public void InitializeFromIni(string configureFile)
        {
            IniParser parser = new IniParser(@configureFile);
            
            _tokenizerName = parser.GetSetting("appSettings", "COMPANY.Modules.Tokenizer");
            
            _stemmerName = parser.GetSetting("appSettings", "COMPANY.Modules.Stemmer");
            
            _summarizerName = parser.GetSetting("appSettings", "COMPANY.Modules.Summerizer");
            _sentneceDetectorName = parser.GetSetting("appSettings", "COMPANY.Modules.SenteceDetector");
            _sentenceDetectorModelFile = parser.GetSetting("appSettings", "COMPANY.Modules.SentenceDetectorModelFile");
            _posTaggerModelFile = parser.GetSetting("appSettings", "COMPANY.Modules.POSTaggerModelFile");
            _posTaggerName = parser.GetSetting("appSettings", "COMPANY.Modules.POSTagging");
            
            InputFileName = parser.GetSetting("appSettings", "COMPANY.Modules.InputFile");
            OutputFolder = parser.GetSetting("appSettings", "COMPANY.Modules.OutPutFolder");
            InitiateModules();
        }




    }
}
