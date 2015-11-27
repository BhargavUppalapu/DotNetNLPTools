using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Com.Research.NLPCore.Tokenization
{
    class RegexTokenizer :ITokenizer
    {

        private TokenizerOptions _options = TokenizerOptions.Default;
        private Regex sentenceStartRegex = new Regex("\\s[a-z\\d]+[.!?;]+\\s+(?<sentenceStart>[A-Z])");

        public RegexTokenizer()
        {
        }
        public RegexTokenizer(TokenizerOptions options)
        {
            _options = options;
        }


        public string[] Tokenize(string text)
        {
            return TokenizeEnu(text).ToArray();
        }

        public IEnumerable<string> TokenizeEnu(string text)
        {
            text = text.Replace('\r', ' ');
            text = text.Replace('\n', ' ');

            var sentences = sentenceStartRegex
                .Matches(text)
                .Cast<Match>()
                .Select(m => m.Groups["sentenceStart"].Index)
                .OrderBy(v => v)
                .ToList();
            sentences.Add(text.Length);

            int start = 0;
            for (int i = 0; i < sentences.Count; ++i)
            {
                int end = sentences[i];
                if (start < end)
                    foreach (var word in TokenizeSentence(text, start, end - start))
                        yield return word;

                start = end;
            }
        }


        private IEnumerable<string> TokenizeSentence(string text, int start, int length)
        {
            return PennTreebankTokenize(text.Substring(start, length));
        }


        private IEnumerable<string> PennTreebankTokenize(string text)
        {
            //normalize quotation marks
            text = Regex.Replace(text, "[\u2018\u2019\u201A\u201B]", "'");
            text = Regex.Replace(text, "[\u201C\u201D\u201E\u201F]", "\"");


            //normalize dashes
            if (_options.NormalizeDashes)
            {
                text = Regex.Replace(text, "[\u2012\u2013\u2014\u2015\u2053]", "-");
            }

            //# attempt to get correct directional quotes
            //s=^"='' =g
            //s=\([ ([{<]\)"=\1 '' =g
            text = Regex.Replace(text, "^\"", "'' ");
            text = Regex.Replace(text, "([\\s(\\[{<])\"", "$1 '' ");

            //# close quotes handled at end
            //s=\.\.\.= ... =g
            //s=[,;:@#$%&]= & =g
            text = Regex.Replace(text, "\\.\\.\\.", " ... ");
            text = Regex.Replace(text, "[,;:@#$%&]", " $0 ");

            //# Assume sentence tokenization has been done first, so split FINAL periods
            //# only. 
            //s=\([^.]\)\([.]\)\([])}>"']*\)[ 	]*$=\1 \2\3 =g
            text = Regex.Replace(text, "([^.])([.])([\\]\\)}>\"']*)[\\s]*$", "$1 $2$3");

            //# however, we may as well split ALL question marks and exclamation points,
            //# since they shouldn't have the abbrev.-marker ambiguity problem
            //s=[?!]= & =g
            text = Regex.Replace(text, "[?!]", " $0 ");


            //# parentheses, brackets, etc.
            //s=[][(){}<>]= & =g
            text = Regex.Replace(text, "[\\]\\[\\(\\){}<>]", " $0 ");



            //# Some taggers, such as Adwait Ratnaparkhi's MXPOST, use the parsed-file
            //# version of these symbols.
            //# UNCOMMENT THE FOLLOWING 6 LINES if you're using MXPOST.
            //# s/(/-LRB-/g
            //# s/)/-RRB-/g
            //# s/\[/-LSB-/g
            //# s/\]/-RSB-/g
            //# s/{/-LCB-/g
            //# s/}/-RCB-/g
            if (_options.ReplaceBrackets)
            {
                text = Regex.Replace(text, "\\(", "-LRB-");
                text = Regex.Replace(text, "\\)", "-RRB-");
                text = Regex.Replace(text, "\\[", "-LSB-");
                text = Regex.Replace(text, "\\]", "-RSB-");
                text = Regex.Replace(text, "\\{", "-LCB-");
                text = Regex.Replace(text, "\\}", "-RCB-");
            }

            //s=--= -- =g
            text = Regex.Replace(text, "--", " -- ");



            //# NOTE THAT SPLIT WORDS ARE NOT MARKED.  Obviously this isn't great, since
            //# you might someday want to know how the words originally fit together --
            //# but it's too late to make a better system now, given the millions of
            //# words we've already done "wrong".

            //# First off, add a space to the beginning and end of each line, to reduce
            //# necessary number of regexps.
            //s=$= =
            //s=^= =
            text = string.Concat(" ", text, " ");

            //s="= '' =g
            text = Regex.Replace(text, "\"", " '' ");


            //# possessive or close-single-quote
            //s=\([^']\)' =\1 ' =g
            text = Regex.Replace(text, "([^'])'\\s", "$1 ' ");

            //# as in it's, I'm, we'd
            //s='\([sSmMdD]\) = '\1 =g
            //s='ll = 'll =g
            //s='re = 're =g
            //s='ve = 've =g
            //s=n't = n't =g
            //s='LL = 'LL =g
            //s='RE = 'RE =g
            //s='VE = 'VE =g
            //s=N'T = N'T =g
            text = Regex.Replace(text, "'([sSmMdD])\\s", " '$1 ");
            text = Regex.Replace(text, "('ll|'LL|'re|'RE|'ve|'VE|n't|N'T)\\s", " $1 ");

            //s= \([Cc]\)annot = \1an not =g
            //s= \([Dd]\)'ye = \1' ye =g
            //s= \([Gg]\)imme = \1im me =g
            //s= \([Gg]\)onna = \1on na =g
            //s= \([Gg]\)otta = \1ot ta =g
            //s= \([Ll]\)emme = \1em me =g
            //s= \([Mm]\)ore'n = \1ore 'n =g
            //s= '\([Tt]\)is = '\1 is =g
            //s= '\([Tt]\)was = '\1 was =g
            //s= \([Ww]\)anna = \1an na =g
            //# s= \([Ww]\)haddya = \1ha dd ya =g
            //# s= \([Ww]\)hatcha = \1ha t cha =g
            if (_options.ReplaceWords)
            {
                text = Regex.Replace(text, "\\s([Cc])annot\\s", " $1an not ");
                text = Regex.Replace(text, "\\s([Dd])'ye\\s", " $1' ye ");
                text = Regex.Replace(text, "\\s([Gg])imme\\s", " $1im me ");
                text = Regex.Replace(text, "\\s([Gg])onna\\s", " $1on na ");
                text = Regex.Replace(text, "\\s([Gg])otta\\s", " $1ot ta ");
                text = Regex.Replace(text, "\\s([Ll])emme\\s", " $1em me ");
                text = Regex.Replace(text, "\\s([Mm])ore'n\\s", " $1ore 'n ");
                text = Regex.Replace(text, "\\s'([Tt])is\\s", " '$1 is ");
                text = Regex.Replace(text, "\\s'([Tt])was\\s", " '$1 was ");
                text = Regex.Replace(text, "\\s([Ww])anna\\s", " $1an na ");
                text = Regex.Replace(text, "\\s([Ww])haddya\\s", " $1ha dd ya ");
                text = Regex.Replace(text, "\\s([Ww])hatcha\\s", " $1ha t cha ");
            }

            //# clean out extra spaces
            //s=  *= =g
            //s=^ *==g
            return Regex.Split(text, "\\s+").Where(s => !string.IsNullOrEmpty(s));
        }
    }
}
