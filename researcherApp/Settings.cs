using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace researcherApp
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void currentSequence_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != 'A') && (e.KeyChar != 'B') && (e.KeyChar != 'a') && (e.KeyChar != 'b')) e.Handled = true;
        }

        private void addSequence_Click(object sender, EventArgs e)
        {
            sequencesList.Items.Add(currentSequence.Text);
            currentSequence.Clear();
            currentSequence.Focus();
        }

        private void deleteSequence_Click(object sender, EventArgs e)
        {
            int i = 0;
            while( i<sequencesList.Items.Count)
            {
                if (sequencesList.GetItemCheckState(i) == CheckState.Checked)
                    sequencesList.Items.RemoveAt(i);
                else
                    i++;
            }
        }



        private void accept_Click(object sender, EventArgs e)
        {
            Main m = this.Owner as Main;
              
            if (Convert.ToInt32(colCount.Text)>0)
                Properties.Settings.Default.gridCols = Convert.ToInt32(colCount.Text);
            if (Convert.ToInt32(rowCount.Text)>0)
                Properties.Settings.Default.gridRows = Convert.ToInt32(rowCount.Text);
            Properties.Settings.Default.pencilSize = Convert.ToInt32(pencilSize.Value);
            string[] seq = sequencesList.Items.Cast<string>().ToArray();
            if (Properties.Settings.Default.Sequences == null) 
                Properties.Settings.Default.Sequences = new StringCollection();
            Properties.Settings.Default.Sequences.Clear();
            Properties.Settings.Default.Sequences.AddRange(seq);
            Properties.Settings.Default.pencilColor = pencilColor.BackColor;
            Properties.Settings.Default.sequenceColor = sequenceColor.BackColor;
            Properties.Settings.Default.alertColor = alertColor.BackColor;
            Properties.Settings.Default.Save();
            
            
            //if ((m.gridCols != Properties.Settings.Default.gridCols) || (m.gridRows != Properties.Settings.Default.gridRows) || (m.sequences!=Properties.Settings.Default.Sequences))
            //{
            
                m.gridCols = Properties.Settings.Default.gridCols;
                m.gridRows = Properties.Settings.Default.gridRows;
                m.reSizeDrawBuffer();
                m.Draw_grid(m.grafx.Graphics);
                
            //}
            m.pencilSize.Value = Properties.Settings.Default.pencilSize;
            m.size.Text = pencilSize.Value.ToString();
            m.sequences = Properties.Settings.Default.Sequences;

            this.DialogResult = DialogResult.OK;

           
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            colCount.Text = Properties.Settings.Default.gridCols.ToString();
            rowCount.Text = Properties.Settings.Default.gridRows.ToString();
            pencilSize.Value = Properties.Settings.Default.pencilSize;
            //StringCollection str = new StringCollection();
            //str = Properties.Settings.Default.Sequences;

             pencilColor.BackColor=Properties.Settings.Default.pencilColor;
             sequenceColor.BackColor= Properties.Settings.Default.sequenceColor ;
             alertColor.BackColor = Properties.Settings.Default.alertColor;

            if (Properties.Settings.Default.Sequences!=null)
            sequencesList.Items.AddRange(Properties.Settings.Default.Sequences.Cast<string>().ToArray());
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
             colorDialog.Color=(sender as Button).BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                (sender as Button).BackColor = colorDialog.Color;
        }

       

        
    }
}
