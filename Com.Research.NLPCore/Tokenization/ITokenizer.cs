using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Tokenization
{
    public interface ITokenizer
    {
        /// <summary>
        /// Tokenizes the given input string and returns the array of tokens
        /// </summary>
        /// <param name="input">
        /// Input text to tokanize
        /// </param>
        /// <returns>
        /// Array of strings
        /// </returns>
        string[] Tokenize(string input);
    }
}
