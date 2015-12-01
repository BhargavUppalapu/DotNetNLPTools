using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.SentenceDetector
{
    public interface IsentenceDetector
    {

        /// <summary>
        /// Split the text into sentences
        /// </summary>
        /// <param name="text">Text in which sentences will be segmented</param>
        /// <returns>A list of sentences</returns>
        string[] SplitSentences(string text);

        /// <summary>
        /// Load the sentence detector model if needed
        /// </summary>
        /// <param name="model">
        /// path of the model
        /// </param>
        /// <returns>
        /// If loaded successfully, returns true else false.
        /// </returns>
        bool LoadModel(string model);
    }
}
