using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsDetectorApp
{
    public class tokenisation
    {
        List<string> lis_wordStop = new List<string>();

        public tokenisation()
        {
            if (lis_wordStop.Count == 0)
            {
                string[] word_stops = FileManager.LoadFromFile("WordStop");

                foreach(string s in word_stops)
                {
                    lis_wordStop.Add(s);
                }
            }
        }

        public List<string> Separation(string input)
        {
            OpenNLP.Tools.Tokenize.EnglishRuleBasedTokenizer tokenizer = new OpenNLP.Tools.Tokenize.EnglishRuleBasedTokenizer(false);
            var tokens = tokenizer.Tokenize(input);

            List<string> temp = new List<string>();

            foreach (var c in tokens)
            {
                if(IsWordEmotional(c))
                {
                    temp.Add(c);
                }
            }

            return temp;
        }

        public bool IsWordEmotional(string _input)
        {
            if (lis_wordStop.Contains(_input))
                return false;
            return true;
        }
    }
}
