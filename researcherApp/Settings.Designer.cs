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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colCount = new System.Windows.Forms.TextBox();
            this.rowCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pencilSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addSequence = new System.Windows.Forms.Button();
            this.currentSequence = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteSequence = new System.Windows.Forms.Button();
            this.sequencesList = new System.Windows.Forms.CheckedListBox();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colCount);
            this.groupBox1.Controls.Add(this.rowCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Размеры таблицы";
            // 
            // colCount
            // 
            this.colCount.Location = new System.Drawing.Point(175, 57);
            this.colCount.Name = "colCount";
            this.colCount.Size = new System.Drawing.Size(79, 20);
            this.colCount.TabIndex = 3;
            // 
            // rowCount
            // 
            this.rowCount.Location = new System.Drawing.Point(175, 22);
            this.rowCount.Name = "rowCount";
            this.rowCount.Size = new System.Drawing.Size(79, 20);
            this.rowCount.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество ячеек";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество строк";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pencilSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(278, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки заметок";
            // 
            // pencilSize
            // 
            this.pencilSize.Location = new System.Drawing.Point(188, 22);
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
            this.pencilSize.Size = new System.Drawing.Size(56, 20);
            this.pencilSize.TabIndex = 1;
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
            this.label3.Location = new System.Drawing.Point(25, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Размер кисти";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.addSequence);
            this.groupBox3.Controls.Add(this.currentSequence);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.deleteSequence);
            this.groupBox3.Controls.Add(this.sequencesList);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox3.Location = new System.Drawing.Point(12, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(526, 160);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выделять последовальности";
            // 
            // addSequence
            // 
            this.addSequence.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addSequence.Location = new System.Drawing.Point(63, 101);
            this.addSequence.Name = "addSequence";
            this.addSequence.Size = new System.Drawing.Size(75, 23);
            this.addSequence.TabIndex = 4;
            this.addSequence.Text = "Добавить";
            this.addSequence.UseVisualStyleBackColor = true;
            this.addSequence.Click += new System.EventHandler(this.addSequence_Click);
            // 
            // currentSequence
            // 
            this.currentSequence.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.currentSequence.Location = new System.Drawing.Point(13, 71);
            this.currentSequence.Name = "currentSequence";
            this.currentSequence.Size = new System.Drawing.Size(183, 20);
            this.currentSequence.TabIndex = 3;
            this.currentSequence.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.currentSequence_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(25, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Введите последовательность:";
            // 
            // deleteSequence
            // 
            this.deleteSequence.AutoSize = true;
            this.deleteSequence.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteSequence.Location = new System.Drawing.Point(308, 130);
            this.deleteSequence.Name = "deleteSequence";
            this.deleteSequence.Size = new System.Drawing.Size(158, 23);
            this.deleteSequence.TabIndex = 1;
            this.deleteSequence.Text = "Удалить последовальность";
            this.deleteSequence.UseVisualStyleBackColor = true;
            this.deleteSequence.Click += new System.EventHandler(this.deleteSequence_Click);
            // 
            // sequencesList
            // 
            this.sequencesList.BackColor = System.Drawing.SystemColors.Control;
            this.sequencesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sequencesList.CheckOnClick = true;
            this.sequencesList.FormattingEnabled = true;
            this.sequencesList.Location = new System.Drawing.Point(249, 19);
            this.sequencesList.Name = "sequencesList";
            this.sequencesList.Size = new System.Drawing.Size(271, 105);
            this.sequencesList.TabIndex = 0;
            this.sequencesList.ThreeDCheckBoxes = true;
            this.sequencesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.sequencesList_ItemCheck);
            this.sequencesList.SelectedValueChanged += new System.EventHandler(this.sequencesList_SelectedValueChanged);
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(175, 284);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 35);
            this.accept.TabIndex = 3;
            this.accept.Text = "Сохранить";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(298, 284);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 35);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.button4_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 327);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Settings";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox colCount;
        private System.Windows.Forms.TextBox rowCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button addSequence;
        private System.Windows.Forms.TextBox currentSequence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteSequence;
        private System.Windows.Forms.CheckedListBox sequencesList;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.NumericUpDown pencilSize;
    }
}