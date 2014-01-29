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
    public partial class InputBox : Form
    {
        public int value = 0;
        public InputBox(string caption, string message, string val)
        {
            InitializeComponent();
            this.Text = caption;
            this.label1.Text = message;
            this.textBox1.Text = val;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            value =Convert.ToInt32(textBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            
        }
    }
}
