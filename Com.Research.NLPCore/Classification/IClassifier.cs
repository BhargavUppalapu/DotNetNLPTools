using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Classification
{
    interface IClassifier
    {

        /// <summary>
        /// Preprocess and fill the Document with the required content from the input file
        /// </summary>
        /// <param name="doc">Document that contains the preprocessed content</param>
        /// <param name="config">configuration details for classification module</param>
        void preprocessing(String doc);

        float classify(String doc, string modelFile);

        void LoadModel(string modelPath);
    }
}
