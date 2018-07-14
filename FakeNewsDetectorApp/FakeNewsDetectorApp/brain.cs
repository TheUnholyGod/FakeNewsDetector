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
        public List<int> positive = new List<int>();
        public List<int> negative = new List<int>();

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

        public bool Result(List<string> input)
        {
            int total = 0;
           
            List<string> separted_input = input;

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

        public bool Relevance(string _main, string _main_source, string []_sources)
        {
            positive.Clear();
            negative.Clear();
            float total = 0;
            int total_positive = 0, total_negative = 0;
            tokenisation temp_token = new tokenisation();

            List<string> separted_main_input = temp_token.Separation(_main);
            Stack<List<string>> separted_sources_input = new Stack<List<string>>();

            foreach(string s in _sources)
            {
                List<string> separted_sources = temp_token.Separation(s);
                separted_sources_input.Push(separted_sources);
            }
            int index = 0;
            while(separted_sources_input.Count > 0)
            {
                List<string> separted_sources = separted_sources_input.Pop();
                Dictionary<string, int> temp = new Dictionary<string, int>();

                foreach(string s in separted_sources)
                {
                    if(separted_main_input.Contains(s))
                    {
                        if(!temp.ContainsKey(s))
                        {
                            temp.Add(s, (int)(((100.0f / (float)separted_main_input.Count))));
                        }
                        else
                        {
                            temp[s] += 1;
                        }
                    }
                }

                foreach(var i in temp)
                {
                    total += i.Value;
                }

                if(total > 50)
                {
                    if (Result(separted_sources))
                    {
                        positive.Add(index);
                        ++total_positive;
                    }
                    else
                    {
                        negative.Add(index);
                        ++total_negative;
                    }

                    total = 0;
                }
                ++index;
            }


            if(Result(_main_source))
            {
                if(total_positive < total_negative)
                {
                    return false;
                }
                else if (total_positive == 0 && 0 == total_negative)
                {
                    throw new Exception("Unable to tell from searches,most likely unreliable");
                }
                else
                {
                    return true;
                }
            }
            else 
            {
                if (total_positive > total_negative)
                {
                    return false;
                }
                else if(total_positive == 0 && 0 == total_negative)
                {
                    throw new Exception("Unable to tell from searches,most likely unreliable");
                }
                else
                {
                    return true;
                }
            }
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
