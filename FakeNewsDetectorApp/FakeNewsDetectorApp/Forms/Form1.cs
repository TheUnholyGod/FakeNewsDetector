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
            richTextBox1.Text = baseinfo.Title + "\n";
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
            richTextBox1.Text = baseinfo.All;
            progressBar1.Value = 10;
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

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButton2.ShowDropDown();
        }

        private void tabPage1HotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage1.BackColor = SystemColors.HotTrack;
        }

        private void tabPage1SilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage1.BackColor = Color.Silver;
        }

        private void tabPage1ControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage1.BackColor = SystemColors.ControlDark;
        }

        private void tabPage1MaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage1.BackColor = Color.Maroon;
        }

        private void tabPage1GoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage1.BackColor = Color.Goldenrod;
        }

        private void urlBoxHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox2.BackColor = SystemColors.HotTrack;
        }

        private void urlBoxSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.Silver;
        }

        private void urlBoxControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox2.BackColor = SystemColors.ControlDark;
        }

        private void urlBoxMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.Maroon;
        }

        private void urlBoxGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.Goldenrod;
        }

        private void resultBoxHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = SystemColors.HotTrack;
        }

        private void resultBoxSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.Silver;
        }

        private void resultBoxControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = SystemColors.ControlDark;
        }

        private void resultBoxMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.Maroon;
        }

        private void resultBoxGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.Goldenrod;
        }

        private void urlButtonHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.urlButton.BackColor = SystemColors.HotTrack;
        }

        private void urlButtonSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.urlButton.BackColor = Color.Silver;
        }

        private void urlButtonControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.urlButton.BackColor = SystemColors.ControlDark;
        }

        private void urlButtonMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.urlButton.BackColor = Color.Maroon;
        }

        private void urlButtonGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.urlButton.BackColor = Color.Goldenrod;
        }

        private void trainingBoxHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.BackColor = SystemColors.HotTrack;
        }

        private void trainingBoxSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.BackColor = Color.Silver;
        }

        private void trainingBoxControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.BackColor = SystemColors.ControlDark;
        }

        private void trainingBoxMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.BackColor = Color.Maroon;
        }

        private void trainingBoxGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.BackColor = Color.Goldenrod;
        }

        private void checkButtonHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = SystemColors.HotTrack;
        }

        private void checkButtonSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.Silver;
        }

        private void checkButtonControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = SystemColors.ControlDark;
        }

        private void checkButtonMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.Maroon;
        }

        private void checkButtonGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.Goldenrod;
        }

        private void yesButtonHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = SystemColors.HotTrack;
        }

        private void yesButtonSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Silver;
        }

        private void yesButtonControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = SystemColors.ControlDark;
        }

        private void yesButtonMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Maroon;
        }

        private void yesButtonGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Goldenrod;
        }

        private void noButtonHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = SystemColors.HotTrack;
        }

        private void noButtonSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Silver;
        }

        private void noButtonControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = SystemColors.ControlDark;
        }

        private void noButtonMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Maroon;
        }

        private void noButtonGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Goldenrod;
        }

        private void tabPage2HotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.BackColor = SystemColors.HotTrack;
        }

        private void tabPage2SilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.BackColor = Color.Silver;
        }

        private void tabPage2ControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.BackColor = SystemColors.ControlDark;
        }

        private void tabPage2MaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.BackColor = Color.Maroon;
        }

        private void tabPage2GoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage2.BackColor = Color.Goldenrod;
        }

        private void tabPage3HotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage3.BackColor = SystemColors.HotTrack;
        }

        private void tabPage3SilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage3.BackColor = Color.Silver;
        }

        private void tabPage3ControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage3.BackColor = SystemColors.ControlDark;
        }

        private void tabPage3MaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage3.BackColor = Color.Maroon;
        }

        private void tabPage3GoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabPage3.BackColor = Color.Goldenrod;
        }

        private void positiveTextBoxHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveTextBox.BackColor = SystemColors.HotTrack;
        }

        private void positiveTextBoxSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveTextBox.BackColor = Color.Silver;
        }

        private void positiveTextBoxControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveTextBox.BackColor = SystemColors.ControlDark;
        }

        private void positiveTextBoxMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveTextBox.BackColor = Color.Maroon;
        }

        private void positiveTextBoxGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveTextBox.BackColor = Color.Goldenrod;
        }

        private void negativeTextBoxHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeTextBox.BackColor = SystemColors.HotTrack;
        }

        private void negativeTextBoxSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeTextBox.BackColor = Color.Silver;
        }

        private void negativeTextBoxControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeTextBox.BackColor = SystemColors.ControlDark;
        }

        private void negativeTextBoxMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeTextBox.BackColor = Color.Maroon;
        }

        private void negativeTextBoxGoldenrodToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            this.negativeTextBox.BackColor = Color.Goldenrod;
        }
        
        private void positiveLabelHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = SystemColors.HotTrack;
        }

        private void positiveLabelSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = Color.Silver;
        }

        private void positiveLabelControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = SystemColors.ControlDark;
        }

        private void positiveLabelMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = Color.Maroon;
        }

        private void positiveLabelGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = Color.Goldenrod;
        }

        private void positiveLabelInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.positiveLabel.BackColor = SystemColors.Info;
        }

        private void negativeLabelInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = SystemColors.Info;
        }

        private void negativeLabelHotTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = SystemColors.HotTrack;
        }

        private void negativeLabelSilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = Color.Silver;
        }

        private void negativeLabelControlDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = SystemColors.ControlDark;
        }

        private void negativeLabelMaroonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = Color.Maroon;
        }

        private void negativeLabelGoldenrodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.negativeLabel.BackColor = Color.Goldenrod;
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
