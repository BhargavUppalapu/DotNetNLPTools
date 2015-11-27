using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.DependencyParser
{
    public interface IParser
    {

        public string parse(string sentence);

        /// <summary>
        /// Load the model file and return true if loaded successfully.
        /// </summary>
        /// <param name="modelFile"></param>
        /// <returns></returns>
        public bool LoadModel(string modelFile);

    }
}
