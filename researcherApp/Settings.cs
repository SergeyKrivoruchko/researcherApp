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
            currentSequence.Focus();
        }

        private void deleteSequence_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sequencesList.Items.Count; i++)
                if (sequencesList.GetItemCheckState(i) == CheckState.Checked)
                    sequencesList.Items.RemoveAt(i);
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
            Main m = this.Owner as Main;
          /*  DialogResult res;
            if (m.grid_values.Count > Convert.ToInt32(colCount.Text))
            {
                res = MessageBox.Show("Количество устанавливаемых столбцов меньше количества столбцов в таблице. Все равно продолжить?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.OK)
                    for (int i = Convert.ToInt32(colCount.Text); i < m.grid_values.Count; i++)
                        m.grid_values.Remove(m.grid_values.ElementAt(m.grid_values.Count-1).Key);
                else
                    return;
            }*/
            
            Properties.Settings.Default.gridCols = Convert.ToInt32(colCount.Text);
            Properties.Settings.Default.gridRows = Convert.ToInt32(rowCount.Text);
            Properties.Settings.Default.pencilSize = Convert.ToInt32(pencilSize.Value);
            string[] seq = sequencesList.Items.Cast<string>().ToArray();
            if (Properties.Settings.Default.Sequences == null) 
                Properties.Settings.Default.Sequences = new StringCollection();
            Properties.Settings.Default.Sequences.Clear();
            Properties.Settings.Default.Sequences.AddRange(seq);
            Properties.Settings.Default.Save();
            
            
            //if ((m.gridCols != Properties.Settings.Default.gridCols) || (m.gridRows != Properties.Settings.Default.gridRows) || (m.sequences!=Properties.Settings.Default.Sequences))
            //{
            
                m.gridCols = Properties.Settings.Default.gridCols;
                m.gridRows = Properties.Settings.Default.gridRows;
                m.reSizeDrawBuffer();
                m.Draw_grid(m.grafx.Graphics);
                
            //}
            m.pencilSize.Value = Properties.Settings.Default.pencilSize;
            m.sequences = Properties.Settings.Default.Sequences;

            this.Close();

           
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            colCount.Text = Properties.Settings.Default.gridCols.ToString();
            rowCount.Text = Properties.Settings.Default.gridRows.ToString();
            pencilSize.Value = Properties.Settings.Default.pencilSize;
            //StringCollection str = new StringCollection();
            //str = Properties.Settings.Default.Sequences;
            if (Properties.Settings.Default.Sequences!=null)
            sequencesList.Items.AddRange(Properties.Settings.Default.Sequences.Cast<string>().ToArray());
        }

       

        
    }
}
