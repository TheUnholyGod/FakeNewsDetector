using System;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace FakeNewsDetectorApp
{
    public class GoogleSearch
    {
        string apikey = "AIzaSyBy-_5XSuxpRnoaOVf6um2a6eD4q1lhkXU";
        string searchengine = "001556608615224035032:lxiuybrekty";
        CustomsearchService svc = null;

        public bool Init()
        {
            svc = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apikey });
            return true;
        }

        public string[] Search(string _query)
        {
            CseResource.ListRequest listRequest = svc.Cse.List(_query);
            listRequest.Cx = searchengine;
            Search req = listRequest.Execute();
            string[] ret = new string[req.Items.Count];
            int i = 0;
            foreach (Result result in req.Items)
            {
                ret[i] = result.Link;
                ++i;
            }
            return ret;
        }
    }
}
