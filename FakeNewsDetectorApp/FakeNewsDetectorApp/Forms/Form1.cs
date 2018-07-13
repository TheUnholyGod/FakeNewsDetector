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
            richTextBox1.DetectUrls = true;

            richTextBox1.Text = "";

            string[] urls = Program.searcher.Search(textBox1.Text);

            foreach (string url in urls)
            {
                richTextBox1.Text += url + "\n";
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
