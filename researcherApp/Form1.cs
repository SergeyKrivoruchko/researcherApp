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
        List<int> table_values = new List<int>();

        public string FileName = "";
        TextBox dinamicTextBox;
        bool isPainting = false, isPencilChecked=false, canDelete=true, isErasing=false, isEraseCheked=false;
        Point PaintFrom = new Point(0, 0);
        string[] baseColors = { "Red", "Green", "Blue", "Yellow", "Orange", "BlueViolet" };
        int gridHeight = 20, gridWidth = 70;
        public int gridRows, gridCols, currentPencilSize;
        public StringCollection sequences = new StringCollection();
        private BufferedGraphicsContext context;
        public BufferedGraphics grafx;
        int eraserSize = 10;
        bool isNewRow = false;
        int OldValue = 0; int newRowPos = 0;

        
        public Main()
        {
            InitializeComponent();
            gridRows = Properties.Settings.Default.gridRows;
            gridCols = Properties.Settings.Default.gridCols;
            pencilSize.Value = Properties.Settings.Default.pencilSize;
            sequences = Properties.Settings.Default.Sequences;
            if (sequences == null) sequences = new StringCollection();
           
            

            context = BufferedGraphicsManager.Current;
            
            panel1.Width = (gridCols+1) * gridWidth+1;
            panel1.Height = (gridRows+2) * gridHeight+1;
            context.MaximumBuffer = new Size(panel1.Width + 1, panel1.Height + 1);
            grafx = context.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width + 1, panel1.Height + 1));
            grafx.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            grafx.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            ReadTables();
            Draw_grid(grafx.Graphics);
           
            GetColors();
           
           
        }
        public void reSizeDrawBuffer()
        {
            panel1.Width = (gridCols + 1) * gridWidth + 1;
            panel1.Height = (gridRows + 2) * gridHeight + 1;
            context.MaximumBuffer = new Size(panel1.Width + 1, panel1.Height + 1);
            grafx = context.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width + 1, panel1.Height + 1));
            grafx.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            grafx.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

        private void highliteSequence(Graphics g)

        {


            if (sequences.Count == 0) return;
            for (int i = 0; i < gridRows ; i++)
            {
                StringBuilder str = new StringBuilder(new string('A', table_values.Count));
                for (int j = i+1; j < table_values.Count; j++)
                    if (grid_values[table_values[j]].prop2 == grid_values[table_values[j-i-1]].prop1)
                        str[j] = 'B';
                for (int j = 0; j < sequences.Count; j++)
                {
                    
                    Regex rgx = new Regex(sequences[j]);
                    string sentence = str.ToString();
                    foreach (Match match in rgx.Matches(sentence))
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), gridWidth* (match.Index+1), gridHeight * (i+2) , gridWidth * sequences[j].Length, gridHeight);
                    }
                }
                
            }
        }

        private void DataGridFill()
        {

           /* if (dataGridView1.Rows.Count < grid_values.Count)
                dataGridView1.Rows.Add(grid_values.Count - dataGridView1.Rows.Count);
            else*/
            {
                canDelete = false;
                if (dataGridView1.Rows.Count > 1)
                {
                    int count = dataGridView1.Rows.Count-1;
                    for (int i = 0; i <count; i++)
                        dataGridView1.Rows.RemoveAt(0);
                }
            }
            canDelete = true;


            int j = 0;
            foreach (KeyValuePair<int, ValueProps> pair in grid_values)
            {
                dataGridView1.Rows.Add();
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


        private void GetColors()
        {
            foreach (String c in baseColors)
            {
                colorComboBox.Items.Add(c);
            }
            colorComboBox.SelectedIndex = 0;
        }


        public void Draw_grid(Graphics g)
        {
              


             if (table_values.Count>gridCols)
                 gridCols = table_values.Count;

             g.Clear(Color.White);
            
            for (int i = 0; i <= gridRows+2; i++)
                g.DrawLine(Pens.LightGray, new Point(0, i * gridHeight), new Point(panel1.Width, i * gridHeight));

            for (int i = 0; i <= gridCols+1; i++)
                g.DrawLine(Pens.LightGray, new Point(i * gridWidth, 0), new Point(i * gridWidth, panel1.Height));

            Font f = new Font("Arial", gridHeight - 6, GraphicsUnit.Pixel);
           
            
            for (int i = 0; i < table_values.Count; i++)
            {
                g.DrawString(table_values[i].ToString(), f, Brushes.Black, Cell_Position(table_values[i].ToString(), f, i+1, 0));
                g.DrawString(grid_values[table_values[i]].prop1.ToString(), f, Brushes.Black, Cell_Position(grid_values[table_values[i]].prop1.ToString(), f, i+1, 1));
            }

            for (int j =0; j < table_values.Count; j++)
                for (int i = 2; i <= gridRows+1; i++)

                    g.DrawString(grid_values[table_values[j]].prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values[table_values[j]].prop1.ToString(), f, j+1, i));
                    
                
            

            f.Dispose();
            f = new Font("Arial", gridHeight - 9, GraphicsUnit.Pixel);
            for (int i=1; i<=gridRows; i++)
                g.DrawString(i.ToString(), f, Brushes.DarkSlateGray, Cell_Position(i.ToString(), f, 0, i+1));
            g.DrawString("Значения", f, Brushes.Black, Cell_Position("Значения", f, 0,0));
            g.DrawString("Свойство 1", f, Brushes.Black, Cell_Position("Значения", f, 0, 1));
            
           
            Find_Similar(g);
            highliteSequence(g);
            panel1.Invalidate();
           
        }

        private void Find_Similar(Graphics g)
        {            
            Font f = new Font("Arial", gridHeight - 6, GraphicsUnit.Pixel);
            for (int i = 0; i < table_values.Count; i++)
                for (int j = i + 1; j < table_values.Count; j++)
                    if (grid_values[table_values[i]].prop1 == grid_values[table_values[j]].prop2)
                    {
                        g.FillRectangle(Brushes.Yellow, new Rectangle(gridWidth * (j + 1) + 1, gridHeight * (j - i + 1) + 1, gridWidth - 1, gridHeight - 1));
                        g.DrawString(grid_values[table_values[j]].prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values[table_values[j]].prop2.ToString(), f, j + 1, j - i + 1));
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



        private void eraser_Click(object sender, EventArgs e)
        {
            pencil.Checked = false;
            isPencilChecked = false;
            isEraseCheked = !isEraseCheked;
        }

        private void pencil_Click(object sender, EventArgs e)
        {
            eraser.Checked = false ;
            isEraseCheked = false;
            isPencilChecked = !isPencilChecked;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X <= gridWidth) return;
            
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
            PaintFrom = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= gridWidth) return;
            if (isPainting)
            {
                Graphics g;
                g = grafx.Graphics;
                Pen p = new Pen(Color.FromName(colorComboBox.Text), Convert.ToInt32(pencilSize.Value));
                g.DrawLine(p, PaintFrom, e.Location);
                panel1.Invalidate(new Rectangle(Math.Min(e.X, PaintFrom.X) - 10, Math.Min(e.Y, PaintFrom.Y) - 10, Math.Abs(e.X - PaintFrom.X) + 20, Math.Abs(e.Y - PaintFrom.Y) + 20));
                PaintFrom = e.Location;
              
                
            
            }

            if (isErasing)
            {
                
                int leftBorder = e.X / gridWidth;
                int bottomBorder = e.Y / gridHeight;

                Bitmap b = new Bitmap(gridWidth * 2, gridHeight * 2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                


               
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int i = 0; i < 3; i++)
                    g.DrawLine(Pens.LightGray, new Point(0, i * gridHeight), new Point(b.Width, i * gridHeight));

                for (int i = 0; i < 3; i++)
                    g.DrawLine(Pens.LightGray, new Point(i * gridWidth, 0), new Point(i * gridWidth, b.Height));

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Font f = new Font("Arial", gridHeight-6, GraphicsUnit.Pixel);
               
                if (table_values.Count>0)
                for (int i=0; i<2; i++)
                    for (int j = 0; j < 2; j++)
                    {
                        if (bottomBorder>1)

                        if (((leftBorder + i) - (bottomBorder + j) >= 0)&&(leftBorder + i - 1<table_values.Count))
                            if (grid_values[table_values[leftBorder + i - 1]].prop2 == grid_values[table_values[(leftBorder + i -1) - (bottomBorder+j-1)]].prop1)
                                g.FillRectangle(Brushes.Yellow, new Rectangle(gridWidth * i + 1, gridHeight * j + 1, gridWidth - 1, gridHeight - 1));
                        if (leftBorder-1<table_values.Count)
                        if (bottomBorder==0)
                            g.DrawString(table_values[leftBorder - 1].ToString(), f, Brushes.Black, Cell_Position(table_values[leftBorder - 1].ToString(), f, i, 0));
                        else
                        if (bottomBorder == 1)
                            g.DrawString(grid_values[table_values[leftBorder - 1]].prop1.ToString(), f, Brushes.Black, Cell_Position(grid_values[table_values[leftBorder - 1]].prop1.ToString(), f, i, 0));
                        else

                            g.DrawString(grid_values[table_values[leftBorder - 1]].prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values[table_values[leftBorder - 1]].prop2.ToString(), f, i, 0));
                    }

                if (bottomBorder-2< table_values.Count)
                    for (int i = bottomBorder - 3; i < bottomBorder+1; i++)
                {
                    StringBuilder str = new StringBuilder(new string('A', table_values.Count));
                    
                    for (int j = i+1; j < table_values.Count; j++)
                        if ((bottomBorder + i > 0) && (bottomBorder + i - 1<table_values.Count))
                        if (grid_values[table_values[j]].prop2 == grid_values[table_values[j-i-1]].prop1)
                            str[j] = 'B';
                    for (int j = 0; j < sequences.Count; j++)
                    {

                        Regex rgx = new Regex(sequences[j]);
                        string sentence = str.ToString();
                        foreach (Match match in rgx.Matches(sentence))
                        {
                           
                            g.DrawRectangle(new Pen(Color.Red, 2), (match.Index-(leftBorder-1))*gridWidth , gridHeight* (i-(bottomBorder-2)), gridWidth * sequences[j].Length, gridHeight);
                        } 
                    }

                }



                GraphicsPath clipPath = new GraphicsPath();
                
                
                
                clipPath.AddEllipse(e.X-eraserSize/2, e.Y-eraserSize/2, eraserSize, eraserSize);
               
                
                
               Graphics g1 = grafx.Graphics;
               g1.SetClip(clipPath);

               g1.DrawImage(b, e.X-eraserSize / 2, e.Y-eraserSize / 2, new Rectangle(e.X - leftBorder * gridWidth - eraserSize / 2, e.Y - bottomBorder * gridHeight - eraserSize / 2, eraserSize, eraserSize ), GraphicsUnit.Pixel);
               g1.ResetClip();
               panel1.Invalidate(new Rectangle(e.X - eraserSize / 2, e.Y - eraserSize / 2, eraserSize, eraserSize ));
                
            



            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
            isErasing = false;
        }

        private void ClearAllNotes_Click(object sender, EventArgs e)
        {
            Draw_grid(grafx.Graphics);
            //panel1.Invalidate();
        }

        private void уменшитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight -= 5;
            gridWidth -= 5;
            Draw_grid(grafx.Graphics);
            //panel1.Invalidate();
        }

        private void увеличитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight += 5;
            gridWidth += 5;
            Draw_grid(grafx.Graphics);
            //panel1.Invalidate();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (((isNewRow == true)&&(newRowPos==e.RowIndex)) && (canDelete == true) && (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null))
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = 0;
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = 0;
                isNewRow = false;
            }
            //else return;

            if (e.RowIndex < grid_values.Count)
           
                if (e.ColumnIndex == 0)
                {
                    grid_values.Remove(OldValue);
                    grid_values.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value), new ValueProps(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value)));
                    if (table_values.Contains(OldValue))
                        for (int i = 0; i < table_values.Count; i++) 
                            if (table_values[i]==OldValue) table_values[i]=Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                }
            
            else
                grid_values[Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value)] = new ValueProps(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
            else
                grid_values[Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value)] = new ValueProps(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));

            addToAutoComlete();
            Draw_grid(grafx.Graphics);

            
         
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
            addToAutoComlete();
            DataGridFill();
        }

        private void addToGrid_Click(object sender, EventArgs e)
        {

            if (grid_values.ContainsKey(Convert.ToInt32(addKey.Text)))
                MessageBox.Show("Таблица данных не может содержать два одинаковых значения!", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                grid_values.Add(Convert.ToInt32(addKey.Text), new ValueProps(Convert.ToInt32(addProp1.Text), Convert.ToInt32(addProp2.Text)));
                addToAutoComlete();
                dataGridView1.Rows.Add(Convert.ToInt32(addKey.Text), grid_values[Convert.ToInt32(addKey.Text)].prop1, grid_values[Convert.ToInt32(addKey.Text)].prop2);
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (canDelete)
            {
                grid_values.Remove(grid_values.ElementAt(e.RowIndex).Key);
                
                Draw_grid(grafx.Graphics);
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
            if ((e.X / gridWidth != 0)&&(e.Y/gridHeight==0))
            {
                panel1.Controls.Remove(dinamicTextBox);
                dinamicTextBox = new TextBox();
                dinamicTextBox.Height = gridHeight;
                dinamicTextBox.Width = gridWidth;
                dinamicTextBox.Parent = panel1;
                dinamicTextBox.Location = new Point((e.X / gridWidth) * gridWidth, (e.Y / gridHeight) * gridHeight);
                
                dinamicTextBox.Leave += new EventHandler(deleteTextBox);
                dinamicTextBox.KeyPress += new KeyPressEventHandler(keyPressTextBox);
                dinamicTextBox.Focus();


                dinamicTextBox.Show();
            }
        }

        private void keyPressTextBox(object sender, KeyPressEventArgs e)
        {
            

            
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (dinamicTextBox.Text == "") return;
                
                int newVal = Convert.ToInt32(dinamicTextBox.Text);
                int col = dinamicTextBox.Location.X / gridWidth;
                int row = dinamicTextBox.Location.Y / gridHeight;
                if (grid_values.ContainsKey(newVal))
                    if (col > table_values.Count)
                        table_values.Add(newVal);
                    else
                        table_values[col - 1] = newVal;

                dinamicTextBox.Leave -= new EventHandler(deleteTextBox);
                splitContainer2.Panel2.Focus();
                panel1.Controls.Remove(dinamicTextBox);
                
                
                dinamicTextBox.Dispose();
                Draw_grid(grafx.Graphics);
                
            }
            else
                if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == Convert.ToChar(Keys.Back))) return;
                else
                    e.Handled = true;
                
                
        }

        private void deleteTextBox(object sender, EventArgs e)
        {
            if (dinamicTextBox.Text == "") return;
            int newVal = Convert.ToInt32(dinamicTextBox.Text);
            int col = dinamicTextBox.Location.X/gridWidth;
            int row = dinamicTextBox.Location.Y/gridHeight;
            if (grid_values.ContainsKey(newVal))
                if (col > table_values.Count)
                    table_values.Add(newVal);
                else
                    table_values[col - 1] = newVal;

            
           /* if (grid_values.Count > col - 1)
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
            }*/
            splitContainer2.Panel2.Focus();
            panel1.Controls.Remove(dinamicTextBox);
            dinamicTextBox.Dispose();
            Draw_grid(grafx.Graphics);
            //panel1.Invalidate();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canDelete = false;
            table_values.Clear();
          
            Draw_grid(grafx.Graphics);

            canDelete = true;
        }

 


       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Focus();
           if (panel1.Controls.Contains(dinamicTextBox))
               panel1.Controls.Remove(dinamicTextBox);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString() == "") return;
            if ((dataGridView1[e.ColumnIndex, e.RowIndex].Value == null) || (e.FormattedValue.ToString() != dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
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
                for (int i=0; i<table_values.Count; i++)
                {

                    ObjExcel.Cells[1, i + 2] = table_values[i];
                    ObjExcel.Cells[2, i + 2] = grid_values[table_values[i]].prop1;
                    for (int j=1; j<=gridRows; j++)

                        ObjExcel.Cells[j + 2, i + 2] = grid_values[table_values[i]].prop2;
                        
                }
                ObjWorkBook.SaveAs(saveFileDialog.FileName);
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

        private void pencil_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow == true)
            {
                isNewRow = true;
                newRowPos = e.RowIndex;
            }
        
        }


        private void количествоСтролбцовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox f = new InputBox("Количество столбцов", "Введите количество столбцов", gridCols.ToString());
            f.ShowDialog(this);
            if (f.DialogResult == DialogResult.OK)
            {
                gridCols = f.value;
                Properties.Settings.Default.gridCols = gridCols;
                reSizeDrawBuffer();
                Draw_grid(grafx.Graphics);
            }
        }

        private void количествоСтрокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox f = new InputBox("Количество строк", "Введите количество строк", gridRows.ToString());
            f.ShowDialog(this);
            if (f.DialogResult == DialogResult.OK)
            {
                gridRows = f.value;
                Properties.Settings.Default.gridRows = gridRows;
                reSizeDrawBuffer();
                Draw_grid(grafx.Graphics);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!dataGridView1.Rows[e.RowIndex].IsNewRow)
            OldValue = Convert.ToInt32(dataGridView1[e.ColumnIndex,e.RowIndex].Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grid_values.Clear();
            canDelete = false;
            int count = dataGridView1.Rows.Count;
            for (int i = 0; i < count - 1; i++)
                dataGridView1.Rows.RemoveAt(0);
            canDelete = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar))||(e.KeyChar==Convert.ToChar(Keys.Back))) return;
            else
                e.Handled=true;
        }

        private void addToAutoComlete()
        {
            AutoCompleteStringCollection autocomp = new AutoCompleteStringCollection();
            autocomp.AddRange(grid_values.Select(x => x.Key.ToString()).ToArray());
            textBox1.AutoCompleteCustomSource = autocomp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            if (grid_values.ContainsKey(Convert.ToInt32(textBox1.Text)))
            {
                if (table_values.Count == gridCols)
                {
                    MessageBox.Show("Вся таблица уже заполнена значениями!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                table_values.Add(Convert.ToInt32(textBox1.Text));
                textBox1.Clear();
                Draw_grid(grafx.Graphics);
            }
            else
                toolTip1.Show("Вводимое число должно быть с таблицы значений", textBox1, textBox1.Location.X-35 , textBox1.Location.Y-65, 3000);
        }
    }
}
