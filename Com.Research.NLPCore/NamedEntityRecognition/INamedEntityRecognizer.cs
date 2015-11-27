using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.NamedEntityRecognition
{
    public interface INamedEntityRecognizer
    {

        /// <summary>
        /// Extract names from the tokenized text.
        /// </summary>
        /// <param name="tokens">A sequence of tokens</param>
        /// <returns>Text in which name entities are embedded inline format <COM>A Company Name</COM> .. </returns>
        string ExtractEntities(string[] tokens);

        /// <summary>
        /// Load the model file and return true if loaded successfully.
        /// </summary>
        /// <param name="modelFile"></param>
        /// <returns></returns>
        bool LoadModel(string modelFile);

    }
}
