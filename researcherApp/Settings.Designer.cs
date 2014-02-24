namespace researcherApp
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.colCount = new System.Windows.Forms.TextBox();
            this.rowCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.addSequence = new System.Windows.Forms.Button();
            this.currentSequence = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteSequence = new System.Windows.Forms.Button();
            this.sequencesList = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.alertColor = new System.Windows.Forms.Button();
            this.sequenceColor = new System.Windows.Forms.Button();
            this.pencilColor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pencilSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilSize)).BeginInit();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(51, 280);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 35);
            this.accept.TabIndex = 3;
            this.accept.Text = "Сохранить";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(219, 280);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 35);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 274);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.colCount);
            this.tabPage1.Controls.Add(this.rowCount);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(336, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Таблица";
            // 
            // colCount
            // 
            this.colCount.Location = new System.Drawing.Point(263, 72);
            this.colCount.Name = "colCount";
            this.colCount.Size = new System.Drawing.Size(50, 20);
            this.colCount.TabIndex = 7;
            // 
            // rowCount
            // 
            this.rowCount.Location = new System.Drawing.Point(263, 27);
            this.rowCount.Name = "rowCount";
            this.rowCount.Size = new System.Drawing.Size(50, 20);
            this.rowCount.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество столбцов";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Количество строк";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.addSequence);
            this.tabPage2.Controls.Add(this.currentSequence);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.deleteSequence);
            this.tabPage2.Controls.Add(this.sequencesList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(336, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Последовательности";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // addSequence
            // 
            this.addSequence.Enabled = false;
            this.addSequence.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addSequence.Location = new System.Drawing.Point(253, 26);
            this.addSequence.Name = "addSequence";
            this.addSequence.Size = new System.Drawing.Size(75, 31);
            this.addSequence.TabIndex = 9;
            this.addSequence.Text = "Добавить";
            this.addSequence.UseVisualStyleBackColor = true;
            this.addSequence.Click += new System.EventHandler(this.addSequence_Click);
            // 
            // currentSequence
            // 
            this.currentSequence.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.currentSequence.Location = new System.Drawing.Point(8, 35);
            this.currentSequence.Name = "currentSequence";
            this.currentSequence.Size = new System.Drawing.Size(216, 20);
            this.currentSequence.TabIndex = 8;
            this.currentSequence.TextChanged += new System.EventHandler(this.currentSequence_TextChanged);
            this.currentSequence.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.currentSequence_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(36, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Введите последовательность:";
            // 
            // deleteSequence
            // 
            this.deleteSequence.AutoSize = true;
            this.deleteSequence.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteSequence.Location = new System.Drawing.Point(89, 211);
            this.deleteSequence.Name = "deleteSequence";
            this.deleteSequence.Size = new System.Drawing.Size(158, 31);
            this.deleteSequence.TabIndex = 6;
            this.deleteSequence.Text = "Удалить последовальность";
            this.deleteSequence.UseVisualStyleBackColor = true;
            this.deleteSequence.Click += new System.EventHandler(this.deleteSequence_Click);
            // 
            // sequencesList
            // 
            this.sequencesList.BackColor = System.Drawing.Color.White;
            this.sequencesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sequencesList.CheckOnClick = true;
            this.sequencesList.FormattingEnabled = true;
            this.sequencesList.Location = new System.Drawing.Point(8, 70);
            this.sequencesList.Margin = new System.Windows.Forms.Padding(0);
            this.sequencesList.Name = "sequencesList";
            this.sequencesList.Size = new System.Drawing.Size(320, 137);
            this.sequencesList.TabIndex = 5;
            this.sequencesList.ThreeDCheckBoxes = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.alertColor);
            this.tabPage3.Controls.Add(this.sequenceColor);
            this.tabPage3.Controls.Add(this.pencilColor);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.pencilSize);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(336, 248);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Заметки";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // alertColor
            // 
            this.alertColor.BackColor = System.Drawing.Color.Yellow;
            this.alertColor.Location = new System.Drawing.Point(260, 151);
            this.alertColor.Name = "alertColor";
            this.alertColor.Size = new System.Drawing.Size(41, 41);
            this.alertColor.TabIndex = 15;
            this.alertColor.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.alertColor.UseVisualStyleBackColor = false;
            this.alertColor.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // sequenceColor
            // 
            this.sequenceColor.BackColor = System.Drawing.Color.Red;
            this.sequenceColor.Location = new System.Drawing.Point(260, 104);
            this.sequenceColor.Name = "sequenceColor";
            this.sequenceColor.Size = new System.Drawing.Size(41, 41);
            this.sequenceColor.TabIndex = 14;
            this.sequenceColor.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.sequenceColor.UseVisualStyleBackColor = false;
            this.sequenceColor.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // pencilColor
            // 
            this.pencilColor.BackColor = System.Drawing.Color.Red;
            this.pencilColor.Location = new System.Drawing.Point(260, 57);
            this.pencilColor.Name = "pencilColor";
            this.pencilColor.Size = new System.Drawing.Size(41, 41);
            this.pencilColor.TabIndex = 13;
            this.pencilColor.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.pencilColor.UseVisualStyleBackColor = false;
            this.pencilColor.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Цвет последовательностей";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Цвет совпадений";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Цвет кисти";
            // 
            // pencilSize
            // 
            this.pencilSize.Location = new System.Drawing.Point(260, 28);
            this.pencilSize.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pencilSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pencilSize.Name = "pencilSize";
            this.pencilSize.Size = new System.Drawing.Size(41, 20);
            this.pencilSize.TabIndex = 4;
            this.pencilSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(8, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Размер кисти";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(344, 327);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox colCount;
        private System.Windows.Forms.TextBox rowCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addSequence;
        private System.Windows.Forms.TextBox currentSequence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteSequence;
        private System.Windows.Forms.CheckedListBox sequencesList;
        private System.Windows.Forms.NumericUpDown pencilSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button alertColor;
        public System.Windows.Forms.Button sequenceColor;
        public System.Windows.Forms.Button pencilColor;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}