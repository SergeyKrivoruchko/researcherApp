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


namespace researcherApp
{
    public partial class Main : Form
    {
        public struct ValueProps
        {
            //public ValueProps() { }

            public ValueProps(int p1, int p2) 
            { 
                prop1=p1;
                prop2=p2;
            }

            public int prop1;
            public int prop2;
        }

        Dictionary<int, ValueProps> grid_values = new Dictionary<int, ValueProps>();

        public string FileName = "";
        TextBox dinamicTextBox;
        bool isPainting = false, isPencilChecked=false, canDelete=true, isErasing=false, isEraseCheked=false;
        Bitmap forPainting;
        Point PaintFrom = new Point(0, 0);
        String[] baseColors = {"Red", "Green", "Blue", "Yellow"};
        int gridHeight = 20, gridWidth = 70, gridRows = 30, gridCols = 300;
        public Main()
        {
            InitializeComponent();
            ReadTables();
            pictureBox1.Image = Drow_grid();
            DataGridFill();
            GetColors();
           initializeBitMap(gridWidth * (gridCols+1), gridHeight * (gridRows+2));
            
           
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

        private Bitmap Drow_grid()
        {
            Graphics g;
            Bitmap b;



             gridCols = grid_values.Count;

            b = new Bitmap(gridWidth*(gridCols+1), gridHeight*(gridRows+2));

            g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < pictureBox1.Height; i++)
                g.DrawLine(Pens.LightGray, new Point(0, i * gridHeight), new Point(b.Width, i * gridHeight));

            for (int i = 0; i < pictureBox1.Width; i++)
                g.DrawLine(Pens.LightGray, new Point(i * gridWidth, 0), new Point(i * gridWidth, b.Height));

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
            g.Dispose();
           // b.Dispose();
            GC.Collect();
            Find_Similar(b);
            return b;
        }

        private void Find_Similar(Bitmap bmp)
        {
            Graphics g;
            
          

            g = Graphics.FromImage(bmp);
            Font f = new Font("Arial", gridHeight - 6, GraphicsUnit.Pixel);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
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
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top+5);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
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
                g = Graphics.FromImage(pictureBox1.Image);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen p = new Pen(Color.FromName(colorComboBox.Text), Convert.ToInt32(pencilSize.Value));
                g.DrawLine(p, PaintFrom, new Point(e.X, e.Y));
                PaintFrom.X = e.X;
                PaintFrom.Y = e.Y;

               // g.Dispose();
               // g = Graphics.FromImage(pictureBox1.Image);

                //g.DrawImage(forPainting, 0, 0);
                pictureBox1.Invalidate();
                
                //g.Dispose();
            }

            if (isErasing)
            {
               /* Bitmap b = new Bitmap(Drow_grid());
                Graphics g = Graphics.FromImage(b);
                
                GraphicsPath clipPath = new GraphicsPath();
               // Bitmap b1 = new Bitmap(10,10);
               // Graphics g2 = Graphics.FromImage(b1);
               // g2.DrawImage(b, 0, 0, new Rectangle(e.X - 5, e.Y - 5, 10, 10), GraphicsUnit.Pixel);
                clipPath.AddEllipse(e.X-5, e.Y-5, 10, 10);
               // g.SetClip(clipPath);
                
                //g.DrawImage(pictureBox1.Image, e.X - 3, e.Y - 3);
                //g.Dispose();
               Graphics g1 = Graphics.FromImage(pictureBox1.Image);
               g1.SetClip(clipPath);

               g1.DrawImage(b, e.X - 5, e.Y - 5, new Rectangle(e.X - 5, e.Y - 5, 10, 10), GraphicsUnit.Pixel);
               pictureBox1.Invalidate();*/

                Bitmap b = new Bitmap(gridWidth * 2, gridHeight * 2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(b);
                //b.MakeTransparent(Color.White);
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
                        if ((leftBorder + i) - (bottomBorder + j) >= 0)
                            if (grid_values.ElementAt(leftBorder + i - 1).Value.prop2 == grid_values.ElementAt((leftBorder + i - 1) - (bottomBorder - j - 1)).Value.prop1)
                                g.FillRectangle(Brushes.Yellow, new Rectangle(gridWidth * i + 1, gridHeight * j + 1, gridWidth - 1, gridHeight - 1));
                        if (bottomBorder==0)
                            g.DrawString(grid_values.ElementAt(leftBorder-1).Key.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder-1).Key.ToString(), f, i, 0));
                        else
                        if (bottomBorder == 1)
                            g.DrawString(grid_values.ElementAt(leftBorder - 1).Value.prop1.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder - 1).Value.prop1.ToString(), f, i, 0));
                        else
                        
                                g.DrawString(grid_values.ElementAt(leftBorder - 1).Value.prop2.ToString(), f, Brushes.Black, Cell_Position(grid_values.ElementAt(leftBorder - 1).Value.prop2.ToString(), f, i, 0));
                    }
                    
                
                
                GraphicsPath clipPath = new GraphicsPath();
                
                
                
                clipPath.AddEllipse(e.X-5, e.Y-5, 10, 10);
                g.SetClip(clipPath);
                
                
               Graphics g1 = Graphics.FromImage(pictureBox1.Image);
               g1.SetClip(clipPath);

               g1.DrawImage(b, e.X - 5, e.Y - 5, new Rectangle(e.X-leftBorder*gridWidth - 5, e.Y -bottomBorder*gridHeight- 5, 10, 10), GraphicsUnit.Pixel);
               pictureBox1.Invalidate();
                
            



            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
            isErasing = false;
        }

        private void ClearAllNotes_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Drow_grid();
        }

        private void уменшитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight -= 5;
            gridWidth -= 5;
            pictureBox1.Image = Drow_grid();
        }

        private void увеличитьРазмерЯчеекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHeight += 5;
            gridWidth += 5;
            pictureBox1.Image = Drow_grid();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           

            //grid_values[e.RowIndex][e.ColumnIndex] = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            grid_values.Remove(grid_values.ElementAt(e.RowIndex).Key);
            grid_values.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), new ValueProps(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+2].Value)));
            pictureBox1.Image = Drow_grid();
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
                /*string[] key = line.Split(' ');
                line = reader.ReadLine();
                string[] prop1 = line.Split(' ');
                line = reader.ReadLine();
                string[] prop2 = line.Split(' ');
                for (int i = 0; i < key.Length; i++)
                {
                    int[] prop = new int[3];
                    prop[0] = Convert.ToInt32(key[i]);
                    prop[1] = Convert.ToInt32(prop1[i]);
                    prop[2] = Convert.ToInt32(prop2[i]);

                    grid_values.Add(new List<int>(prop));

                }*/
            }
            pictureBox1.Image = Drow_grid();
            DataGridFill();
        }

        private void addToGrid_Click(object sender, EventArgs e)
        {

           // grid_values.Add(new List<int>(new int[] {Convert.ToInt32(addKey.Text), Convert.ToInt32(addProp1.Text),Convert.ToInt32(addProp2.Text)}));
            grid_values.Add(Convert.ToInt32(addKey.Text), new ValueProps(Convert.ToInt32(addProp1.Text),Convert.ToInt32(addProp2.Text)));

            dataGridView1.Rows.Add(Convert.ToInt32(addKey.Text), grid_values[Convert.ToInt32(addKey.Text)].prop1, grid_values[Convert.ToInt32(addKey.Text)].prop2);
            pictureBox1.Image = Drow_grid();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (canDelete)
            {
                grid_values.Remove(grid_values.ElementAt(e.RowIndex).Key);
                //grid_values.RemoveAt(e.RowIndex);
                pictureBox1.Image = Drow_grid();
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
            pictureBox1.Controls.Remove(dinamicTextBox);
            dinamicTextBox = new TextBox();
            dinamicTextBox.Height = gridHeight;
            dinamicTextBox.Width = gridWidth;
            dinamicTextBox.Parent = pictureBox1;
            dinamicTextBox.Location = new Point((e.X/gridWidth)*gridWidth, (e.Y/gridHeight)*gridHeight);
            dinamicTextBox.Focus();
            dinamicTextBox.Leave += new EventHandler(deleteTextBox);
            //dinamicTextBox.Text = grid_values[e.X / gridWidth-1][e.Y / gridHeight].ToString();
            dinamicTextBox.Show();
        }



        private void deleteTextBox(object sender, EventArgs e)
        {
            pictureBox1.Controls.Remove(dinamicTextBox);
            dinamicTextBox.Dispose();
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
            pictureBox1.Image = Drow_grid();
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

    }
}
