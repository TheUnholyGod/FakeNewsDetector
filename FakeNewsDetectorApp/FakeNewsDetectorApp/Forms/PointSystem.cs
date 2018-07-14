using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsDetectorApp
{
    class PointSystem
    {
        // Dictionary<string, Dictionary<string, int>> point_storage = new Dictionary<string, Dictionary<string, int>>();  //author name -> site name -> points

        Dictionary<string, int> point_storage = new Dictionary<string, int>();  //site name -> points
        public int GetTotalAuthorPoints(string _authorname)
        {
            int total = 0;

            if (point_storage.ContainsKey(_authorname))
            {
                foreach (var dic in point_storage)
                {
                    total += dic.Value;
                }
            }

            return total;
        }

        public int GetTotalSitePoints(string _sitename)
        {
            int total = 0;

            foreach(var dic in point_storage)
            {
                if(dic.Key.Equals(_sitename))
                {
                    total += dic.Value;
                }
            }

            return total;
        }

        public void SetPoint(string _site, bool _trustworthy)
        {
            if(!point_storage.ContainsKey(_site))
            {
                point_storage.Add(_site, 0);
            }

            point_storage[_site] += ((_trustworthy) ? 1 : -1);
        }

        public void LoadData()
        {
            string[] temp = FileManager.LoadFromFile("PointData");

            tokenisation temp_token = new tokenisation();

            foreach (string s in temp)
            {
                List<string> temp_list = temp_token.Separation(s, false);

                string to_lower_temp = temp_list[0].ToLower();              

                int temp_int = 0;
                int.TryParse(temp_list[1], out temp_int);

                if (!point_storage.ContainsKey(to_lower_temp))
                    point_storage.Add(to_lower_temp, temp_int);
            }
        }

        public void SaveData()
        {
            string[] temp = new string[point_storage.Count];
            int i = 0;
            foreach (var p in point_storage)
            {
                string final = p.Key.ToString() + " " + p.Value.ToString();
                temp[i] = final;
                ++i;
            }

            FileManager.SaveToFile("PointData", temp);
        }
    }
}