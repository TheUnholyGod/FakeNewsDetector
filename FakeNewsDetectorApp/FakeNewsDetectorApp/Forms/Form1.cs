using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeNewsDetectorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);

            textBox1.Parent = tabPage1;
            Search.Parent = tabPage1;
            richTextBox1.Parent = tabPage1;

        }

        private void Search_Click(object sender, EventArgs e)
        {
            //richTextBox1.DetectUrls = true;

            //richTextBox1.Text = "";

            //string[] urls = Program.searcher.Search(textBox1.Text);

            //foreach (string url in urls)
            //{
            //    richTextBox1.Text += url + "\n";
            //}

            ArticleInfo baseinfo = HtmlRemoval.ExtractInfo(textBox1.Text);
            string[] urls = Program.searcher.Search(baseinfo.Title);
            ArticleInfo[] checkerinfo = new ArticleInfo[urls.Length];
            int i = 0;
            richTextBox1.Text = baseinfo.All;

            int positive_article = 0, negative_article = 0;
            float percentage = 0;

            foreach (string url in urls)
            {
                checkerinfo[i] = HtmlRemoval.ExtractInfo(url);

                if (Program.b_brain.Result(checkerinfo[i].Articleinfo))
                {
                    ++positive_article;
                }
                else
                {
                    ++negative_article;
                }

                richTextBox1.Text += checkerinfo[i].All;
                ++i;
            }

            if(Program.b_brain.Result(baseinfo.Articleinfo))
            {
                percentage = ((float)positive_article / (float)(positive_article + negative_article)) * 100f;
            }
            else
            {
                percentage = ((float)negative_article / (float)(positive_article + negative_article)) * 100f;
            }

            if(percentage > 70)
            {
                //Program.ps_pointSystem.SetPoint(baseinfo.)
            }
        }

        private void richTextBox1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
          System.Diagnostics.Process.Start(e.LinkText);
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButton1.ShowDropDown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Text = richTextBox2.Text;

            richTextBox2.Text += "\n" + Program.b_brain.Result(richTextBox2.Text).ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Text = richTextBox2.Text;
            Program.b_brain.Store(richTextBox2.Text, 'y');
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Text = richTextBox2.Text;

            Program.b_brain.Store(richTextBox2.Text, 'n');
        }

        //private void Thesaurus_Click(object sender, EventArgs e)
        //{
        //    string test = SourceGetter.GetSource(textBox2.Text);

        //    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        //    doc.LoadHtml(test);
        //    string prnt = "";
        //    string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
        //    prnt += title;
        //    string imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
        //    foreach(HtmlAgilityPack.HtmlNode n in doc.DocumentNode.SelectNodes("//p"))
        //    {
        //        prnt += n.InnerText;
        //    }


        //    richTextBox2.Text = prnt;
        //}
    }
}
