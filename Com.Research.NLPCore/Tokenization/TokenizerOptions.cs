using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Tokenization
{
    public class TokenizerOptions
    {
        public static TokenizerOptions Default = new TokenizerOptions
        {
            ReplaceBrackets = false,
            ReplaceWords = false,
            NormalizeDashes = true
        };
        public bool ReplaceBrackets { get; set; }
        public bool ReplaceWords { get; set; }
        public bool NormalizeDashes { get; set; }
    }



}
