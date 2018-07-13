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
    }
}
