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
            if (textBox1.Text == "") return;
           
            value =Convert.ToInt32(textBox1.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == Convert.ToChar(Keys.Back))) return;
            else
                e.Handled = true;
        }


    }
}
