using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Summarization.DataStructures
{

    /// <summary>
    /// Contains Words after removing the Stop words.
    /// </summary>
    public class wordDetails
    {
        public string word;
        public string pOSTag;
        public string stem; // The Stem of the word after stemming
    }


    public class Sentence
    {
        public int paraNo = 0;
        public string paraHeading = "";
        public string sent = "";
        public string parsedOut = "";
        public List<wordDetails> words = new List<wordDetails>();
        public double TFIDFScore = 0;
        public int Lenght = 0;
        public int upperCaseLettersCount = 0;
        public int NoSent = 0;
        public int JJCount = 0;
        public int nounCount = 0;
        //public int verbCount = 0;
        public double Descriptionscore = 0;
        public double Qualificationscore = 0;

        
    }

    public class Paragraph
    {
        public List<Sentence> sentences = new List<Sentence>();
        public string heading = "";
        public string text;
    }


    public class Document
    {

        /// <summary>
        /// The path to the text file
        /// </summary>
        private string _path;

        /// <summary>
        /// Get the path to the text file </summary>
        /// <returns> the path to the text file </returns>
        public virtual string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }


        /// <summary>
        /// The content of the document
        /// </summary>
        private string _text;
        /// <summary>
        /// Get the content of the document </summary>
        /// <returns> the content of the document </returns>
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }


        /// <summary>
        /// The content of the document
        /// </summary>
        private string _searchString;
        /// <summary>
        /// Get the content of the document </summary>
        /// <returns> the content of the document </returns>
        public virtual string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
            }
        }

        public string description = "";
        
        public string MustHave = "";
        public string GoodTohave = "";

       // public List<Paragraph> paragraphs = new List<Paragraph>();
        public List<Sentence> sentences = new List<Sentence>();
        public Document(string path)
        {
            this.Path = path;
            StreamReader f = new StreamReader(path, System.Text.Encoding.UTF8);
            this.Text = f.ReadToEnd();
           

        }
    }


}
