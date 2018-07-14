using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsDetectorApp
{
    public class pair
    {
        public int first, second;

        public pair(int _first, int _second)
        {
            first = _first;
            second = _second;
        }
    }

    public class brain
    {
        Dictionary<string, pair> storage = new Dictionary<string, pair>();

        public Dictionary<string,double> GetTFIDF(string[] docs)
        {
            Dictionary<string, double> returner = new Dictionary<string, double>();
            tokenisation temp_token = new tokenisation();
            List<string[]> seperatedinputs = new List<string[]>();
            foreach (string s in docs)
            {
               seperatedinputs.Add(temp_token.Separation(s).ToArray());
            }
            Dictionary<int, Dictionary<string, int>> keywordcounter = new Dictionary<int, Dictionary<string, int>>();
            int i = 0;
            foreach (string[] container in seperatedinputs)
            {
                keywordcounter.Add(i, new Dictionary<string, int>());
                foreach (string s in container)
                {
                    string lowered_s = s.ToLower();
                    if (!keywordcounter[i].ContainsKey(lowered_s))
                        keywordcounter[i].Add(lowered_s, 0);
                    keywordcounter[i][lowered_s]++;
                }
                ++i;
            }
            Dictionary<string, int> basechecker = keywordcounter[0];
            
            int totalDocs = keywordcounter.Count;
            i = 0;
            foreach (KeyValuePair<string, int> word in basechecker)
            {
                int NoDocuments = 0;

                foreach (KeyValuePair<int, Dictionary<string, int>> key in keywordcounter)
                {
                    if (key.Value.ContainsKey(word.Key))
                        ++NoDocuments;
                }
                double frac = (double)(totalDocs / (NoDocuments));
                returner[word.Key] = word.Value * Math.Log10(frac);
            }
            return returner;
        }

        public void Store(string input, char decision)
        {
            tokenisation temp_token = new tokenisation();
            
            List<string> separted_input = temp_token.Separation(input);

            foreach (string s in separted_input)
            {
                string lowered_s = s.ToLower();

                if (storage.ContainsKey(lowered_s))
                {
                    if (decision == 'y')
                    {
                        ++(storage[lowered_s].first);
                    }
                    else
                    {
                        ++(storage[lowered_s].second);

                    }
                }
                else
                {
                    storage.Add(lowered_s, new pair(0, 0));

                    if (decision == 'y')
                    {
                        ++(storage[lowered_s].first);
                    }
                    else
                    {
                        ++(storage[lowered_s].second);

                    }
                }
            }
        }

        public bool Result(string input)
        {
            int total = 0;
            tokenisation temp_token = new tokenisation();

            List<string> separted_input = temp_token.Separation(input);

            foreach (string s in separted_input)
            {
                string lowered_s = s.ToLower();

                if (storage.ContainsKey(lowered_s))
                {

                    float positive_average = 0.0f, negative_average = 0.0f;

                    positive_average = (float)storage[lowered_s].first / (float)(storage[lowered_s].first + storage[lowered_s].second);
                    negative_average = (float)storage[lowered_s].second / (float)(storage[lowered_s].first + storage[lowered_s].second);

                    Console.WriteLine("Word is: " + lowered_s + " and has " + positive_average.ToString() + " positive rating and " + negative_average.ToString() + " negative rating!");

                    if (positive_average > negative_average)
                    {
                        total += 1;
                    }
                    else if (positive_average < negative_average)
                    {
                        total -= 1;
                    }
                }
            }

            Console.WriteLine("Total rating is: " + total);

            return ((total > 0) ? true : false);
        }

        public void LoadData()
        {
            string[] temp = FileManager.LoadFromFile("WordData");

            tokenisation temp_token = new tokenisation();

            foreach(string s in temp)
            {
                List<string> temp_list = temp_token.Separation(s, false);

                string to_lower_temp = temp_list[0].ToLower();

                if(!storage.ContainsKey(to_lower_temp))
                    storage.Add(to_lower_temp, new pair(0, 0));
                int.TryParse(temp_list[1], out storage[to_lower_temp].first);
                int.TryParse(temp_list[2], out storage[to_lower_temp].second);

            }
        }

        public void SaveData()
        {
            string[] temp = new string[storage.Count];
            int i = 0;
            foreach (var p in storage)
            {
                string final = p.Key.ToString() + " " + p.Value.first.ToString() + " " + p.Value.second.ToString();
                temp[i] = final;
                ++i;
            }

            FileManager.SaveToFile("WordData", temp);
        }
    }
}
