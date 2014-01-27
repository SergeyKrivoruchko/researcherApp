using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using excel=Microsoft.Office.Interop.Excel;



namespace researcherApp
{
    public partial class Main : Form
    {
       
        public struct ValueProps
        {

            public ValueProps(int p1, int p2) 
            { 
                prop1=p1;
                prop2=p2;
            }

            public int prop1;
            public int prop2;
        }

        public Dictionary<int, ValueProps> grid_values = new Dictionary<int, ValueProps>();

        public string FileName = "";
        TextBox dinamicTextBox;
        bool isPainting = false, isPencilChecked=false, canDelete=true, isErasing=false, isEraseCheked=false;
        Bitmap forPainting;
        Point PaintFrom = new Point(0, 0);
        string[] baseColors = {"Red", "Green", "Blue", "Yellow"};
        int gridHeight = 20, gridWidth = 70;
        public int gridRows, gridCols, currentPencilSize;
        public StringCollection sequences = new StringCollection();
        private BufferedGraphicsContext context;
        public BufferedGraphics grafx;

        
        public Main()
        {
            InitializeComponent();
            gridRows = Properties.Settings.Default.gridRows;
            gridCols = Properties.Settings.Default.gridCols;
            pencilSize.Value = Properties.Settings.Default.pencilSize;
            sequences = Properties.Settings.Default.Sequences;
            if (sequences == null) sequences = new StringCollection();
            
            

            context = BufferedGraphicsManager.Current;
            
            panel1.Width = (gridCols+1) * gridWidth;
            panel1.Height = (gridRows+2) * gridHeight;
            context.MaximumBuffer = new Size(panel1.Width + 1, panel1.Height + 1);
            grafx = context.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width + 1, panel1.Height + 1));

            ReadTables();
            Drow_grid(grafx.Graphics);
            DataGridFill();
            GetColors();
           
           
        }

        private void highliteSequence(Graphics g)

        {
            
           

            for (int i = 0; i < grid_values.Count - 1; i++)
            {
                StringBuilder str = new StringBuilder(new string('A', gridRows));
                for (int j = 0; j < Math.Min(i, gridRows); j++)
                    if (grid_values.ElementAt(i).Value.prop2 == grid_values.ElementAt(i-(j+1)).Value.prop1)
                        str[j] = 'B';
                for (int j = 0; j < sequences.Count; j++)
                {
                    
                    Regex rgx = new Regex(sequences[j]);
                    string sentence = str.ToString();
                    foreach (Match match in rgx.Matches(sentence))
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), gridWidth * (i+1), gridHeight * (match.Index+2), gridWidth, gridHeight * sequences[j].Length);
                    }
                }
                
            }
        }

        private void DataGridFill()
        {

            if (dataGridView1.Rows.Count < grid_values.Count)
                dataGridView1.Rows.Add(grid_values.Count - dataGridView1.Rows.Count);
            else
            {
                canDelete = false;
                for (int i = 0; i < dataGridView1.Rows.Count - grid_values.Count; i++)
                    dataGridView1.Rows.RemoveAt(i);
            }
            canDelete = true;


            int j = 0;
            foreach (KeyValuePair<int, ValueProps> pair in grid_values)
            {
                dataGridView1.Rows[j].Cells[0].Value = pair.Key;
                dataGridView1.Rows[j].Cells[1].Value = pair.Value.prop1;
                dataGridView1.Rows[j].Cells[2].Value = pair.Value.prop2;
                j++;
            }
        }

        private void ReadTables()
        {
            comboBox1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo("tables");
            foreach (var item in dir.GetFiles())
                comboBox1.Items.Add(item.Name);
            comboBox1.SelectedIndex = 0;
        }
        private void initializeBitMap(int w, int h)
        {
            Graphics g;
            forPainting = new Bitmap(w, h);
            g = Graphics.FromImage(forPainting);
            forPainting.MakeTransparent(Color.Coral);
            g.Clear(Color.White);
            g.Dispose();
        }

        private void GetColors()
        {
            foreach (String c in baseColors)
            {
                colorComboBox.Items.Add(c);
            }
            colorComboBox.SelectedIndex = 0;
        }

        public void Drow_grid(Graphics g)
        {
            
           


             if (grid_values.Count>gridCols)
             gridCols = grid_values.Count;

          
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < panel1.Height; i++)
                g.DrawLine(Pens.LightGray, new Point(0, i * gridHeight), new Point(panel1.Width, i * gridHeight));

            for (int i = 0; i < panel1.Width; i++)
                g.DrawLine(Pens.LightGray, new Point(i * gridWidth, 0), new Point(i * gridWidth, panel1.Height));

            Font f = new Font("Arial", gridHeight - 6, GraphicsUnit.Pixel);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;





            for (int i = 1; i <= grid_values.Count; i++)
            {
                g.DrawString(grid_values.ElementAt(i-1).Key.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(i-1).Key.ToString(), f, i, 0));
                g.DrawString(grid_values.ElementAt(i-1).Value.prop1.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(i-1).Value.prop1.ToString(), f, i, 1));
            }

            for (int j = 1; j <= grid_values.Count; j++)
                for (int i = 2; i <= gridRows+1; i++)

                    g.DrawString(grid_values.ElementAt(j-1).Value.prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(j-1).Value.prop1.ToString(), f, j, i));
                    
                
            

            f.Dispose();
            f = new Font("Arial", gridHeight - 9, GraphicsUnit.Pixel);
            for (int i=1; i<=gridRows; i++)
                g.DrawString(i.ToString(), f, Brushes.DarkSlateGray, Cell_Position(i.ToString(), f, 0, i+1));
           
            g.DrawString("Значения", f, Brushes.Black, Cell_Position("Значения", f, 0,0));
            g.DrawString("Свойство 1", f, Brushes.Black, Cell_Position("Значения", f, 0, 1));
            
           
            Find_Similar(g);
            highliteSequence(g);
           
        }

        private void Find_Similar(Graphics g)
        {
            
            
          

            
            Font f = new Font("Arial", gridHeight - 6, GraphicsUnit.Pixel);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            for (int i = 0; i < grid_values.Count; i++)
                for (int j = i + 1; j < grid_values.Count; j++)
                    if (grid_values.ElementAt(i).Value.prop1 == grid_values.ElementAt(j).Value.prop2)
                    {
                        // if ((gridWidth * (j) +gridWidth <=pictureBox1.Image.Width) && ( gridHeight * (j - i + 1)  +gridHeight <=pictureBox1.Image.Height ))
                        g.FillRectangle(Brushes.Yellow, new Rectangle(gridWidth * (j + 1) + 1, gridHeight * (j - i + 1) + 1, gridWidth - 1, gridHeight - 1));
                        g.DrawString(grid_values.ElementAt(j).Value.prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(j).Value.prop2.ToString(), f, j + 1, j - i + 1));
                    }
           
            
        }

        private PointF Cell_Position(string s, Font f, int posX, int posY)
        {
            PointF p = new PointF();
            p.X = posX * gridWidth + gridWidth / 2 - TextRenderer.MeasureText(s, f).Width / 2;
            p.Y = posY * gridHeight + gridHeight / 2 - TextRenderer.MeasureText(s, f).Height / 2;
            return p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileDialog f = new FileDialog(comboBox1.Text);
            f.Owner = this;
            f.ShowDialog();
            
            if (FileName!="")
            {
                StreamWriter writer = new StreamWriter("tables/"+FileName+".txt");

                
                    foreach (KeyValuePair<int, ValueProps> pair in grid_values)
                    {
                        writer.Write(pair.Key + " " + pair.Value.prop1 + " " + pair.Value.prop2);
                        writer.WriteLine();
                    }
                    

                
                writer.Close();
            }
            ReadTables();
        }

        private void colorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if ( (e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
                 e.DrawBackground();
            
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                
                    g.DrawString(n, f, Brushes.Black, rect.X, rect.Top + 5);
                g.FillRectangle(b, rect.X + 70, rect.Y + 5, rect.Width - 10, rect.Height - 10);
                
            
        }

        private void pencil_CheckedChanged(object sender, EventArgs e)
        {
            
            isPencilChecked = !isPencilChecked;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (isPencilChecked)
            {
                isPainting = true;
                isErasing = false;
              
            }

            if (isEraseCheked)
            {
                isErasing = true;
                isPainting = false;

            }
            PaintFrom.X = e.X;
            PaintFrom.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= gridWidth) return;
            if (isPainting)
            {
                Graphics g;
                g = grafx.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen p = new Pen(Color.FromName(colorComboBox.Text), Convert.ToInt32(pencilSize.Value));
                g.DrawLine(p, PaintFrom, new Point(e.X, e.Y));
                panel1.Invalidate(new Rectangle(Math.Min(e.X, PaintFrom.X) - 10, Math.Min(e.Y, PaintFrom.Y) - 10, Math.Abs(e.X - PaintFrom.X) + 20, Math.Abs(e.Y - PaintFrom.Y) + 20));
                PaintFrom.X = e.X;
                PaintFrom.Y = e.Y;
                
                
            
            }

            if (isErasing)
            {

                Bitmap b = new Bitmap(gridWidth * 2, gridHeight * 2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                int leftBorder = e.X / gridWidth;
                int bottomBorder = e.Y / gridHeight;


                
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int i = 0; i < 3; i++)
                    g.DrawLine(Pens.LightGray, new Point(0, i * gridHeight), new Point(b.Width, i * gridHeight));

                for (int i = 0; i < 3; i++)
                    g.DrawLine(Pens.LightGray, new Point(i * gridWidth, 0), new Point(i * gridWidth, b.Height));
                
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Font f = new Font("Arial", gridHeight-6, GraphicsUnit.Pixel);
               

                for (int i=0; i<2; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        if (bottomBorder>1)

                        if (((leftBorder + i) - (bottomBorder + j) >= 0)&&(leftBorder + i - 1<grid_values.Count))
                            if (grid_values.ElementAt(leftBorder + i - 1).Value.prop2 == grid_values.ElementAt((leftBorder + i -1) - (bottomBorder+j-1)).Value.prop1)
                                g.FillRectangle(Brushes.Yellow, new Rectangle(gridWidth * i + 1, gridHeight * j + 1, gridWidth - 1, gridHeight - 1));
                        if (leftBorder-1<grid_values.Count)
                        if (bottomBorder==0)
                            g.DrawString(grid_values.ElementAt(leftBorder-1).Key.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder-1).Key.ToString(), f, i, 0));
                        else
                        if (bottomBorder == 1)
                            g.DrawString(grid_values.ElementAt(leftBorder - 1).Value.prop1.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder - 1).Value.prop1.ToString(), f, i, 0));
                        else
                        
                                g.DrawString(grid_values.ElementAt(leftBorder - 1).Value.prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder - 1).Value.prop2.ToString(), f, i, 0));
                    }

                if (leftBorder - 1 < grid_values.Count)
                for (int i = 0; i <2; i++)
                {
                    StringBuilder str = new StringBuilder(new string('A', gridRows));
                    for (int j = 0; j < Math.Min(leftBorder-2, gridRows); j++)
                        if (grid_values.ElementAt(leftBorder - i-1).Value.prop2 == grid_values.ElementAt((leftBorder - i-1) - (j + 1)).Value.prop1)
                            str[j] = 'B';
                    for (int j = 0; j < sequences.Count; j++)
                    {

                        Regex rgx = new Regex(sequences[j]);
                        string sentence = str.ToString();
                        foreach (Match match in rgx.Matches(sentence))
                        {
                            if ((match.Index<=bottomBorder-2)&&(match.Index+sequences[j].Length>=bottomBorder-1))
                            g.DrawRectangle(new Pen(Color.Red, 2), gridWidth * i, (match.Index-(bottomBorder-2))*gridHeight, gridWidth, gridHeight * sequences[j].Length);
                        } 
                    }

                }

                
                
                GraphicsPath clipPath = new GraphicsPath();
                
                
                
                clipPath.AddEllipse(e.X-5, e.Y-5, 10, 10);
               
                
                
               Graphics g1 = grafx.Graphics;
               g1.SetClip(clipPath);

               g1.DrawImage(b, e.X - 5, e.Y - 5, new Rectangle(e.X-leftBorder*gridWidth - 5, e.Y -bottomBorder*gridHeight- 5, 10, 10), GraphicsUnit.Pixel);
              
               panel1.Invalidate(new Rectangle(e.X - 5, e.Y - 5, 10, 10));
                
            



            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
            isErasing = false;
        }

        private void ClearAllNotes_Click(object sender, EventArgs e)
        {
            Drow_grid(grafx.Graphics);
            panel1.Invalidate();
        }

        private void уменшитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight -= 5;
            gridWidth -= 5;
            Drow_grid(grafx.Graphics);
            panel1.Invalidate();
        }

        private void увеличитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight += 5;
            gridWidth += 5;
            Drow_grid(grafx.Graphics);
            panel1.Invalidate();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            grid_values.Remove(grid_values.ElementAt(e.RowIndex).Key);
            grid_values.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value), new ValueProps(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value)));
            Drow_grid(grafx.Graphics);
            panel1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_values.Clear();
            using (var reader = new System.IO.StreamReader(@"tables/" + comboBox1.Text))
            {
                string line;



                while ((line = reader.ReadLine()) != null)
                {
                    ValueProps props;
                    
                    string[] result = line.Split(' ');
                    props.prop1= Convert.ToInt32(result[1]);
                    props.prop2= Convert.ToInt32(result[2]);
                    grid_values.Add(Convert.ToInt32(result[0]), props);
                }
               
            }
            Drow_grid(grafx.Graphics);
            panel1.Invalidate();
            DataGridFill();
        }

        private void addToGrid_Click(object sender, EventArgs e)
        {

            if (grid_values.ContainsKey(Convert.ToInt32(addKey.Text)))
                MessageBox.Show("Таблица данных не может содержать два одинаковых значения!", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                grid_values.Add(Convert.ToInt32(addKey.Text), new ValueProps(Convert.ToInt32(addProp1.Text), Convert.ToInt32(addProp2.Text)));

                dataGridView1.Rows.Add(Convert.ToInt32(addKey.Text), grid_values[Convert.ToInt32(addKey.Text)].prop1, grid_values[Convert.ToInt32(addKey.Text)].prop2);
                Drow_grid(grafx.Graphics);
                panel1.Invalidate();
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (canDelete)
            {
                grid_values.Remove(grid_values.ElementAt(e.RowIndex).Key);
                
                Drow_grid(grafx.Graphics);
            }
        }

        private void addKey_TextChanged(object sender, EventArgs e)
        {
            if ((addKey.Text != "") && (addProp1.Text != "") && (addProp2.Text != ""))
                addToGrid.Enabled = true;
            else
                addToGrid.Enabled = false;

        }



        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X / gridWidth != 0)
            {
                panel1.Controls.Remove(dinamicTextBox);
                dinamicTextBox = new TextBox();
                dinamicTextBox.Height = gridHeight;
                dinamicTextBox.Width = gridWidth;
                dinamicTextBox.Parent = panel1;
                dinamicTextBox.Location = new Point((e.X / gridWidth) * gridWidth, (e.Y / gridHeight) * gridHeight);
                
                dinamicTextBox.Leave += new EventHandler(deleteTextBox);
                if (grid_values.Count>e.X / gridWidth - 1)
                switch (e.Y / gridHeight)
                {
                    case 0: dinamicTextBox.Text = grid_values.ElementAt(e.X / gridWidth - 1).Key.ToString();
                        break;
                    case 1: dinamicTextBox.Text = grid_values.ElementAt(e.X / gridWidth - 1).Value.prop1.ToString();
                        break;
                    default: dinamicTextBox.Text = grid_values.ElementAt(e.X / gridWidth - 1).Value.prop2.ToString();
                        break;
                }
                dinamicTextBox.Focus();


                dinamicTextBox.Show();
            }
        }



        private void deleteTextBox(object sender, EventArgs e)
        {
            int newVal = Convert.ToInt32(dinamicTextBox.Text);
            int col = dinamicTextBox.Location.X/gridWidth;
            int row = dinamicTextBox.Location.Y/gridHeight;
            if (grid_values.Count > col - 1)
            {
                if (row == 0)
                {
                    if ((grid_values.ContainsKey(newVal))&&(newVal!=grid_values.ElementAt(col-1).Key))
                    {
                        MessageBox.Show("Таблица данных не может содержать два одинаковых значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            
                    ValueProps prop = grid_values.ElementAt(col - 1).Value;
                    grid_values.Remove(grid_values.ElementAt(col - 1).Key);
                    grid_values.Add(newVal, prop);
                    dataGridView1.Rows[col - 1].Cells[0].Value = dinamicTextBox.Text;
                }
                else
                    if (row == 1)
                    {
                        grid_values[(grid_values.ElementAt(col - 1).Key)] = new ValueProps(newVal, (grid_values.ElementAt(col - 1).Value.prop2));
                        dataGridView1.Rows[col - 1].Cells[1].Value = dinamicTextBox.Text;
                    }
                    else
                    {
                        grid_values[(grid_values.ElementAt(col - 1).Key)] = new ValueProps((grid_values.ElementAt(col - 1).Value.prop1), newVal);
                        dataGridView1.Rows[col - 1].Cells[2].Value = dinamicTextBox.Text;
                    }
            }
            else
            {
                if (row == 0)
                {
                    grid_values.Add(newVal, new ValueProps());
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = newVal;
                }
            }
            splitContainer2.Panel2.Focus();
            panel1.Controls.Remove(dinamicTextBox);
            dinamicTextBox.Dispose();
            Drow_grid(grafx.Graphics);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canDelete = false;
            grid_values.Clear();
            dataGridView1.Rows.Clear();
            Drow_grid(grafx.Graphics);
            canDelete = true;
        }

 


        private void eraser_Click(object sender, EventArgs e)
        {
            pencil.Checked = false;
            isEraseCheked = true;
            isPencilChecked = false;
        }

        private void pencil_Click(object sender, EventArgs e)
        {
            eraser.Checked = false;
            isEraseCheked = false;
            isPencilChecked = true; ;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Focus();
           if (panel1.Controls.Contains(dinamicTextBox))
               panel1.Controls.Remove(dinamicTextBox);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString()!=dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString())
            if (grid_values.ContainsKey(Convert.ToInt32(e.FormattedValue)))
            {
                MessageBox.Show("Таблица данных не может содержать два одинаковых значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                
            }
        }

        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            f.Owner = this;
            f.ShowDialog();
        }

        private void показатьВерхнююПанельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = !splitContainer2.Panel1Collapsed;
        }

        private void показатьЛевуюПанельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void експортТаблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel;
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjExcel = new excel.Application();
                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);

                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                ObjExcel.Cells[1, 1] = "Значения";
                ObjExcel.Cells[2, 1] = "Свойство1";
                for (int i = 1; i <= gridRows; i++)
                    ObjExcel.Cells[i+2, 1]=i;
                for (int i=0; i<grid_values.Count; i++)
                {
                    
                    ObjExcel.Cells[1,i+2]=grid_values.ElementAt(i).Key;
                    ObjExcel.Cells[2,i+2]=grid_values.ElementAt(i).Value.prop1;
                    for (int j=1; j<=gridRows; j++)
                       
                        ObjExcel.Cells[j+2,i+2] = grid_values.ElementAt(i).Value.prop2;
                        
                }
                ObjWorkBook.SaveAs(saveFileDialog.FileName+".xlsx");
                ObjWorkBook.Close();
                ObjExcel.Quit();
                ObjWorkBook = null;
                ObjWorkSheet = null;
                ObjExcel = null;



            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            grafx.Render(e.Graphics);
        }

    }
}
