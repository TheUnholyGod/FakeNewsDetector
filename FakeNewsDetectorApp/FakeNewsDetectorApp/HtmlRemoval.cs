using System;
using System.Text.RegularExpressions;

namespace FakeNewsDetectorApp
{
    /// <summary>
    /// Methods to remove HTML from strings.
    /// </summary>
    public static class HtmlRemoval
    {
        public static ArticleInfo ExtractInfo(string _url)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(_url);
            string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
            string imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
            string info = "";
            foreach (HtmlAgilityPack.HtmlNode n in doc.DocumentNode.SelectNodes("//p"))
            {
                info += n.InnerText;
                info += "\n\n";
            }
            return new ArticleInfo(title, imgUrl, info);
        }
    }

    public class ArticleInfo
    {
        string title;
        string imgurl;
        string articleinfo;

        public ArticleInfo(string _title, string _imgurl, string _articleinfo)
        {
            title = _title;
            imgurl = _imgurl;
            articleinfo = _articleinfo;
        }

        public string Title { get => title; set => title = value; }
        public string Imgurl { get => imgurl; set => imgurl = value; }
        public string Articleinfo { get => articleinfo; set => articleinfo = value; }
    }
}
