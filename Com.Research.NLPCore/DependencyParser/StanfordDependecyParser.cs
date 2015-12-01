extern alias ParserGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ParserGlobal::edu.stanford.nlp.process;
using ParserGlobal::edu.stanford.nlp.ling;
using ParserGlobal::edu.stanford.nlp.trees;
using ParserGlobal::edu.stanford.nlp.parser.lexparser;
using java.util;
using java.io;

namespace Com.Research.NLPCore.DependencyParser
{
    class StanfordDependecyParser :IParser
    {


        /// <summary>
        /// The model file for Stanford Dependency parser
        /// </summary>
        private static LexicalizedParser _sdpModel ;


        /// <summary>
        /// Is the model for Stanford named entity recognizer successfully loaded
        /// </summary>
        private bool _isSDPModelLoaded = false;

        public bool IsSDPModelLoaded
        {
            get { return _isSDPModelLoaded; }
            set { _isSDPModelLoaded = value; }
        }



        public string parse(string sentence)
        {
            string parsedout = "";
            // This option shows loading and using an explicit tokenizer
            var sent2 = sentence;
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(sent2);
            var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            var parse = _sdpModel.apply(rawWords2);

            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(parse);
            var tdl = gs.typedDependenciesCCprocessed();

            System.Console.WriteLine();
            for (var it = tdl.iterator(); it.hasNext(); )
            {
                parsedout = parsedout + "\n" +it.next();
            }
                //System.Console.WriteLine("{0}", it.next());
            //System.Console.WriteLine();

            //var tp = new TreePrint("penn,typedDependenciesCollapsed");
           
            return parsedout;
            
        }

        public bool LoadModel(string modelFile)
        {

            try
            {
                _sdpModel = LexicalizedParser.loadModel(modelFile);
                _isSDPModelLoaded = true;
                return true;
            }
            catch
            {
                System.Console.WriteLine("Uable to load the Model englishPCFG.ser.gz... ");
                _isSDPModelLoaded = false;
                return false;
            }


           



        }
    }
}
