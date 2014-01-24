using System;
using System.Collections.Generic;
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
            this.Close();
        }

        private void currentSequence_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != 'A') && (e.KeyChar != 'B') && (e.KeyChar != 'a') && (e.KeyChar != 'b')) e.Handled = true;
        }

        private void addSequence_Click(object sender, EventArgs e)
        {
            sequencesList.Items.Add(currentSequence.Text);
            currentSequence.Clear();
        }

        private void deleteSequence_Click(object sender, EventArgs e)
        {
            foreach (int index in sequencesList.SelectedIndices)
            {
                sequencesList.Items.RemoveAt(index);
            }
        }

        private void sequencesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /*if ((sequencesList.SelectedItems.Count == 0)||((e.CurrentValue== CheckState.Checked)&&(sequencesList.SelectedItems.Count==1)))
                deleteSequence.Enabled = false;
            else
                deleteSequence.Enabled = true;*/
        }

        private void sequencesList_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void accept_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.gridCols = Convert.ToInt32(colCount.Text);
            Properties.Settings.Default.gridRows = Convert.ToInt32(rowCount.Text);
            Properties.Settings.Default.pencilSize = Convert.ToInt32(pencilSize.Value);
            string[] seq = sequencesList.Items.Cast<string>().ToArray();
            if (Properties.Settings.Default.Sequences == null) 
                Properties.Settings.Default.Sequences = new StringCollection();
            Properties.Settings.Default.Sequences.Clear();
            Properties.Settings.Default.Sequences.AddRange(seq);
            Properties.Settings.Default.Save();
            Main m = this.Owner as Main;
            if ((m.gridCols != Properties.Settings.Default.gridCols) || (m.gridRows != Properties.Settings.Default.gridRows))
            {
                m.gridCols = Properties.Settings.Default.gridCols;
                m.gridRows = Properties.Settings.Default.gridRows;
                m.pictureBox1.Image = m.Drow_grid();
            }
            m.pencilSize.Value = Properties.Settings.Default.pencilSize;

            this.Close();

           
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            colCount.Text = Properties.Settings.Default.gridCols.ToString();
            rowCount.Text = Properties.Settings.Default.gridRows.ToString();
            pencilSize.Value = Properties.Settings.Default.pencilSize;
            //StringCollection str = new StringCollection();
            //str = Properties.Settings.Default.Sequences;
            sequencesList.Items.AddRange(Properties.Settings.Default.Sequences.Cast<string>().ToArray());
        }

       

        
    }
}
