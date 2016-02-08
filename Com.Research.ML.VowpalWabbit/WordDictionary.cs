using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Com.Research.ML.VowpalWabbit
{
     public class WordDictionary
    {
        private List<string> _words = new List<string>();

        public WordDictionary(string path, int size)
        {
            _words = LoadDictionary(path, size);
        }

        public IEnumerable<string> Filter(IEnumerable<string> tokens)
        {
            var tokensSet = new HashSet<string>(tokens);
            for (int i = 0; i < _words.Count; ++i)
                if (tokensSet.Contains(_words[i]))
                    yield return _words[i];
        }

        public BitArray ToFeaturesVector(IEnumerable<string> tokens)
        {
            var result = new BitArray(_words.Count);
            var tokensSet = new HashSet<string>(tokens);

            for (int i = 0; i < _words.Count; ++i)
                if (tokensSet.Contains(_words[i]))
                    result.Set(i, true);

            return result;
        }

        private static List<string> LoadDictionary(string path, int maxSize)
        {
            return File.ReadAllLines(path)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => s.Split(' '))
                .Select(kv => kv[1])
                .Take(maxSize)
                .ToList();
        }
    }
}
