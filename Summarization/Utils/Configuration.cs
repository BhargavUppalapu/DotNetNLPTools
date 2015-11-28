using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Research.NLPCore;
using Com.Research.NLPCore.NamedEntityRecognition;
using Com.Research.NLPCore.POSTagging;
using Com.Research.NLPCore.Tokenization;


namespace Summarization.Utils
{
    class Configuration
    {
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
        /// Name for POS Tagger
        /// </summary>
        private string _posTaggerNameName = null;
        private bool _isposTaggerCreated = false;
        private string _posTaggerModelFile;
        private readonly FactoryClass<IPOSTagger> _myPOSTaggerFactory = new FactoryClass<IPOSTagger>();
        private IPOSTagger _myposTagger;

        public IPOSTagger POSTagger
        {
            get { return _myposTagger; }
        }



        public string InputFileName;
        public string OutputFolder;
        public string NaesForTrends;


        public void InitiateModules()
        {


            //Initializing Stanford CRFNER model.
            if (!string.IsNullOrEmpty(_tokenizerName))
                _isTokenizerCreated = CreateTokenizer();
            //OutputLogInfo(_isTokenizerCreated, "Tokenizer");

            
            if (!string.IsNullOrEmpty(_posTaggerNameName))
                _isposTaggerCreated = CreatePOSTagger();


        }


        public bool CreateTokenizer()
        {

            _myTokenizer = _myTokenizerFactory.Create(_tokenizerName);
            return (_myTokenizer != null) ? true : false;

        }

        public bool CreatePOSTagger()
        {
            Console.WriteLine("Loading POS Tagger Model. This may take few seconds.");
            _myposTagger = _myPOSTaggerFactory.Create(_posTaggerNameName);
            return (_myposTagger.LoadModel(_posTaggerModelFile));

        }


        public void InitializeFromIni(string configureFile)
        {
            IniParser parser = new IniParser(@configureFile);
            _tokenizerName = parser.GetSetting("appSettings", "COMPANY.Modules.Tokenizer");
            _posTaggerModelFile = parser.GetSetting("appSettings", "COMPANY.Modules.POSTaggerModelFile");
            _posTaggerNameName = parser.GetSetting("appSettings", "COMPANY.Modules.POSTagging");
            NaesForTrends = parser.GetSetting("appSettings", "COMPANY.Modules.NamesForTrends");
            InputFileName = parser.GetSetting("appSettings", "COMPANY.Modules.InputFile");
            OutputFolder = parser.GetSetting("appSettings", "COMPANY.Modules.OutPutFolder");
            InitiateModules();
        }



    }
}
