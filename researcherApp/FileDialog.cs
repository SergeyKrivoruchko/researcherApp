using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace researcherApp
{
    public partial class FileDialog : Form
    {
        
        public FileDialog(string fileName)
        {
            InitializeComponent();


            textBox1.Text = fileName.Remove(fileName.Length - 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = this.Owner as Main;
            main.FileName = textBox1.Text;
            this.Close();
        }
    }
}
