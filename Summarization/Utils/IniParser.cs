using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;


namespace Summarization.Utils
{
    class IniParser
    {
        public static readonly string ROOT = string.Empty;
        private Dictionary<string, Dictionary<string, string>> _values = new Dictionary<string, Dictionary<string, string>>();

        public IniParser(string file)
        {
            Parse(file);
        }


        public void AddSetting(string section, string key)
        {
            AddSetting(section, key, string.Empty);
        }

        public void AddSetting(string section, string key, string value)
        {
            if (!_values.ContainsKey(section))
                _values.Add(section, new Dictionary<string, string>());

            _values[section][key] = value;
        }

        public void DeleteSetting(string section, string key)
        {
            if (!_values.ContainsKey(section))
                return;

            if (_values[section].Remove(key))
            {
                if (_values[section].Count == 0)
                    _values.Remove(section);
            }
        }

        public string[] EnumSection(string section)
        {
            if (!_values.ContainsKey(section))
                return new string[0];

            return _values[section].Keys.ToArray();
        }

        public string GetSetting(string key)
        {
            return GetSetting(ROOT, key);
        }

        public string GetSetting(string section, string key)
        {
            if (!_values.ContainsKey(section))
                return null;
            if (!_values[section].ContainsKey(key))
                return null;

            return _values[section][key];
        }

        private void Parse(string file)
        {
            string section = ROOT;
            foreach (var line in File.ReadAllLines(file))
            {
                var trimedLine = line.Trim(' ', '\t');
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                else if (trimedLine.StartsWith(";"))
                {
                    continue;
                }
                else if (trimedLine.StartsWith("[") && trimedLine.EndsWith("]"))
                {
                    section = trimedLine.Substring(1, trimedLine.Length - 2);
                    continue;
                }

                string key = trimedLine;
                string value = string.Empty;

                int equalsIndex = trimedLine.IndexOf('=');
                if (equalsIndex >= 0)
                {
                    key = trimedLine.Substring(0, equalsIndex);

                    if (equalsIndex + 1 < trimedLine.Length)
                        value = trimedLine.Substring(equalsIndex + 1, trimedLine.Length - equalsIndex - 1);
                }

                AddSetting(section, key, value);
            }
        }


    }
}
