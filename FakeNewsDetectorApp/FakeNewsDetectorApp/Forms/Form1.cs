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
        Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);

            stopwatch = new Stopwatch();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            ArticleInfo baseinfo = HtmlRemoval.ExtractInfo(textBox2.Text);
            baseinfo.url = textBox2.Text;
            baseinfo.websitename = baseinfo.url.Substring(baseinfo.url.IndexOf('.') + 1);
            baseinfo.websitename = baseinfo.websitename.Substring(0, baseinfo.websitename.IndexOf('.'));
            richTextBox1.Text = baseinfo.websitename + "\n";
            string full = "";
            foreach (KeyValuePair<string,double> prnt in Program.b_brain.GetTFIDF(new string[] { baseinfo.Title, baseinfo.Articleinfo }))
            {
                richTextBox1.Text += prnt.Key + " : " + prnt.Value.ToString() + "\n";
                if(prnt.Value == 0)
                    full += prnt.Key + " ";
            }
            richTextBox1.Text += full;
            
            string[] urls = Program.searcher.Search(baseinfo.Title);
            ArticleInfo[] checkerinfo = new ArticleInfo[urls.Length];
            int i = 0;
            richTextBox1.Text += baseinfo.All;

            int positive_article = 0, negative_article = 0;
            float percentage = 0;

            string[] temp_array = new string[checkerinfo.Length];

            

            foreach (string url in urls)
            {
                
                //if (i == 0)
                //{
                //    ++i;
                //    continue;
                //}
                richTextBox1.Text += url + "\n";
                checkerinfo[i] = HtmlRemoval.ExtractInfo(url);
                temp_array[i] = checkerinfo[i].Articleinfo;
                checkerinfo[i].url = url;
                checkerinfo[i].websitename = checkerinfo[i].url.Substring(checkerinfo[i].url.IndexOf('.') + 1);
                try
                {
                    checkerinfo[i].websitename = checkerinfo[i].websitename.Substring(0, checkerinfo[i].websitename.IndexOf('.'));
                }
                catch
                {
                    checkerinfo[i].websitename = checkerinfo[i].url.Substring(0,checkerinfo[i].url.IndexOf('.'));
                }
                //richTextBox1.Text += checkerinfo[i].All;
                ++i;
            }
            try
            {
                if (Program.b_brain.Relevance(full, baseinfo.Articleinfo, temp_array))
                {
                    richTextBox1.Text += "\n" + "RELIABLE!";
                    Program.ps_pointSystem.SetPoint(baseinfo.websitename,true);
                    foreach(int i1 in Program.b_brain.positive)
                    {
                        Program.ps_pointSystem.SetPoint(checkerinfo[i1].websitename, true);
                    }
                    foreach (int i1 in Program.b_brain.negative)
                    {
                        Program.ps_pointSystem.SetPoint(checkerinfo[i1].websitename, false);
                    }
                }
                else
                {
                    richTextBox1.Text += "\n" + "UNRELIABLE";
                    Program.ps_pointSystem.SetPoint(baseinfo.websitename, false);
                    foreach (int i1 in Program.b_brain.positive)
                    {
                        Program.ps_pointSystem.SetPoint(checkerinfo[i1].websitename, false);
                    }
                    foreach (int i1 in Program.b_brain.negative)
                    {
                        Program.ps_pointSystem.SetPoint(checkerinfo[i1].websitename, true);
                    }
                }
            }
            catch (Exception exp)
            {
                richTextBox1.Text += exp.Message;
            }
           
            richTextBox3.Text = stopwatch.GetElapsedTime().ToString();
            stopwatch.Stop();
        }

        private void richTextBox1_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
          System.Diagnostics.Process.Start(e.LinkText);
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

        private void urlButton_Click(object sender, EventArgs e)
        {
            
        }

        private void x576ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1024, 576);
        }

        private void x720ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1280, 720);
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButton1.ShowDropDown();
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
