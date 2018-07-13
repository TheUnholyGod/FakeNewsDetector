using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsDetectorApp
{
    class PointSystem
    {
        Dictionary<string, Dictionary<string, int>> point_storage = new Dictionary<string, Dictionary<string, int>>();  //author name -> site name -> points

        public int GetTotalAuthorPoints(string _authorname)
        {
            int total = 0;

            if (point_storage.ContainsKey(_authorname))
            {
                foreach (var dic in point_storage[_authorname])
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
                if(dic.Value.ContainsKey(_sitename))
                {
                    total += dic.Value[_sitename];
                }
            }

            return total;
        }

        public void SetPoint(string _author, string _site, bool _trustworthy)
        {
            if(!point_storage.ContainsKey(_author))
            {
                point_storage.Add(_author, new Dictionary<string, int>());
            }

            if (!point_storage[_author].ContainsKey(_site))
            {
                point_storage[_author].Add(_site, 0);
            }

            point_storage[_author][_site] += ((_trustworthy) ? 1 : -1);
        }
    }
}
