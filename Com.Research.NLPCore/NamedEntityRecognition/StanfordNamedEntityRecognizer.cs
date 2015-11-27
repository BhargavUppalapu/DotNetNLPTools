using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using edu.stanford.nlp.ie;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.process;

using java.util;
using java.io;


namespace Com.Research.NLPCore.NamedEntityRecognition
{
    public class StanfordNamedEntityRecognizer : INamedEntityRecognizer
    {

        /// <summary>
        /// The model file for Stanford named entity recognizer
        /// </summary>
        private static string _crfModelFile = "";
        public string CrfModelFile
        {
            get { return _crfModelFile; }
            set { _crfModelFile = value; }
        }



        /// <summary>
        /// Constructor
        /// </summary>
        public StanfordNamedEntityRecognizer()
        {


        }

        /// <summary>
        /// The model for Stanford named entity recognizer
        /// </summary>
        private AbstractSequenceClassifier _crfModel;

        /// <summary>
        /// Is the model for Stanford named entity recognizer successfully loaded
        /// </summary>
        private bool _isCRFModelLoaded = false;

        public bool IsCRFModelLoaded
        {
            get { return _isCRFModelLoaded; }
            set { _isCRFModelLoaded = value; }
        }
        /// <summary>
        /// Create CRF model from the model file
        /// </summary>
        /// <param name="crfSerializedClassifier">The model file</param>
        /// <returns>If loaded successfully, returns true else false.</returns>
        public bool LoadModel(string crfSerializedClassifier)
        {
            try
            {
                _crfModel = CRFClassifier.getClassifierNoExceptions(crfSerializedClassifier);
                _isCRFModelLoaded = true;
                return true;
            }
            catch
            {
                System.Console.WriteLine("Uable to load the Stanford CRF Model... ");
                _isCRFModelLoaded = false;
                return false;
            }

        }


        /// <summary>
        /// Train a Stanford NER model from a configuration file
        /// </summary>
        /// <param name="prop">Configuration file</param>

        public bool Train(string prop)
        {

            try
            {
                java.util.Properties props = new java.util.Properties();
                InputStream st = new BufferedInputStream(new FileInputStream(prop));
                InputStreamReader reader = new InputStreamReader(st, "utf-8");


                props.load(reader);
                _crfModel = new CRFClassifier(props);

                _crfModel.train();
                String serializeTo = _crfModel.flags.serializeTo;
                if (serializeTo != null)
                {
                    _crfModel.serializeClassifier(serializeTo);
                }

                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Unable to train the Standford CRF model" + e.ToString());
                return false;

            }
        }

        /// <summary>
        /// Extract name entities from the text
        /// </summary>
        /// <param name="tokens">A sequence of tokens</param>
        /// <returns>Text in which name entities are embedded with inline format, i.e.,  ...<COM>A company name</COM>...</returns>
        public string ExtractEntities(string[] tokens)
        {
            if (!_isCRFModelLoaded)
                return null;
            try
            {
                string inputText = string.Join(" ", tokens);
                //Stanford CRF model applies its own tokenizer
                string taggedText = _crfModel.classifyWithInlineXML(inputText);
                return taggedText;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
