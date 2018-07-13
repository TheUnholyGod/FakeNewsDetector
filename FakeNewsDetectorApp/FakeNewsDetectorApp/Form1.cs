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

            Form2 f2 = new Form2();
            f2.ShowDialog();
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
