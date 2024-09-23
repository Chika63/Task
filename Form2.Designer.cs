namespace Task
{
    partial class Form2
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
            inputNumtxt = new TextBox();
            Titletxt = new TextBox();
            label7 = new Label();
            label1 = new Label();
            dgv = new DataGridView();
            savebtn = new Button();
            deletebtn = new Button();
            label2 = new Label();
            ticketcmb = new ComboBox();
            label3 = new Label();
            Contenttxt = new TextBox();
            panel1 = new Panel();
            label4 = new Label();
            EXCCHK = new DataGridViewCheckBoxColumn();
            INPUTNUM = new DataGridViewTextBoxColumn();
            PROGRESS = new DataGridViewProgressBarColumn();
            COMPDAY = new DataGridViewTextBoxColumn();
            DESCRIPTION = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // inputNumtxt
            // 
            inputNumtxt.BackColor = SystemColors.ControlLight;
            inputNumtxt.Location = new Point(81, 12);
            inputNumtxt.MaxLength = 3;
            inputNumtxt.Name = "inputNumtxt";
            inputNumtxt.ReadOnly = true;
            inputNumtxt.Size = new Size(57, 23);
            inputNumtxt.TabIndex = 8;
            inputNumtxt.TabStop = false;
            // 
            // Titletxt
            // 
            Titletxt.BackColor = SystemColors.ControlLight;
            Titletxt.Location = new Point(186, 12);
            Titletxt.Name = "Titletxt";
            Titletxt.ReadOnly = true;
            Titletxt.Size = new Size(588, 23);
            Titletxt.TabIndex = 9;
            Titletxt.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(23, 17);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 17;
            label7.Text = "チケットNo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(144, 17);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 18;
            label1.Text = "タイトル";
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { EXCCHK, INPUTNUM, PROGRESS, COMPDAY, DESCRIPTION });
            dgv.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv.Location = new Point(23, 86);
            dgv.Name = "dgv";
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 25;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(751, 325);
            dgv.TabIndex = 19;
            dgv.CellEndEdit += dgv_CellEndEdit;
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;
            // 
            // savebtn
            // 
            savebtn.Location = new Point(673, 4);
            savebtn.Name = "savebtn";
            savebtn.Size = new Size(75, 23);
            savebtn.TabIndex = 20;
            savebtn.Text = "閉じる";
            savebtn.UseVisualStyleBackColor = true;
            savebtn.Click += savebtn_Click;
            // 
            // deletebtn
            // 
            deletebtn.Location = new Point(592, 4);
            deletebtn.Name = "deletebtn";
            deletebtn.Size = new Size(75, 23);
            deletebtn.TabIndex = 21;
            deletebtn.Text = "削除";
            deletebtn.UseVisualStyleBackColor = true;
            deletebtn.Click += deletebtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 8);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 22;
            label2.Text = "関連チケット";
            // 
            // ticketcmb
            // 
            ticketcmb.FormattingEnabled = true;
            ticketcmb.Location = new Point(70, 3);
            ticketcmb.Name = "ticketcmb";
            ticketcmb.Size = new Size(72, 23);
            ticketcmb.TabIndex = 23;
            ticketcmb.SelectedIndexChanged += ticketcmb_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 41);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 24;
            label3.Text = "詳細内容";
            // 
            // Contenttxt
            // 
            Contenttxt.BackColor = SystemColors.Window;
            Contenttxt.Location = new Point(81, 38);
            Contenttxt.Multiline = true;
            Contenttxt.Name = "Contenttxt";
            Contenttxt.Size = new Size(693, 42);
            Contenttxt.TabIndex = 25;
            Contenttxt.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(ticketcmb);
            panel1.Controls.Add(savebtn);
            panel1.Controls.Add(deletebtn);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 417);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 33);
            panel1.TabIndex = 26;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(163, 6);
            label4.Name = "label4";
            label4.Size = new Size(129, 15);
            label4.TabIndex = 24;
            label4.Text = "※ALLは追加、変更不可";
            // 
            // EXCCHK
            // 
            EXCCHK.DataPropertyName = "EXCCHK";
            EXCCHK.FalseValue = "0";
            EXCCHK.FillWeight = 30F;
            EXCCHK.HeaderText = "仕";
            EXCCHK.Name = "EXCCHK";
            EXCCHK.TrueValue = "1";
            EXCCHK.Width = 30;
            // 
            // INPUTNUM
            // 
            INPUTNUM.DataPropertyName = "INPUTNUM";
            INPUTNUM.FillWeight = 35F;
            INPUTNUM.HeaderText = "NO";
            INPUTNUM.MaxInputLength = 3;
            INPUTNUM.Name = "INPUTNUM";
            INPUTNUM.SortMode = DataGridViewColumnSortMode.NotSortable;
            INPUTNUM.Width = 35;
            // 
            // PROGRESS
            // 
            PROGRESS.DataPropertyName = "PROGRESS";
            PROGRESS.FillWeight = 50F;
            PROGRESS.HeaderText = "進捗率";
            PROGRESS.Name = "PROGRESS";
            PROGRESS.Width = 50;
            // 
            // COMPDAY
            // 
            COMPDAY.DataPropertyName = "COMPDAY";
            COMPDAY.FillWeight = 70F;
            COMPDAY.HeaderText = "完了日";
            COMPDAY.Name = "COMPDAY";
            COMPDAY.SortMode = DataGridViewColumnSortMode.NotSortable;
            COMPDAY.Width = 70;
            // 
            // DESCRIPTION
            // 
            DESCRIPTION.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DESCRIPTION.DataPropertyName = "DESCRIPTION";
            DESCRIPTION.HeaderText = "対応内容";
            DESCRIPTION.Name = "DESCRIPTION";
            DESCRIPTION.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(Contenttxt);
            Controls.Add(label3);
            Controls.Add(dgv);
            Controls.Add(label1);
            Controls.Add(label7);
            Controls.Add(inputNumtxt);
            Controls.Add(Titletxt);
            Name = "Form2";
            Text = "タスク詳細";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox inputNumtxt;
        private TextBox Titletxt;
        private Label label7;
        private Label label1;
        private DataGridView dgv;
        private Button savebtn;
        private Button deletebtn;
        private Label label2;
        private ComboBox ticketcmb;
        private Label label3;
        private TextBox Contenttxt;
        private Panel panel1;
        private Label label4;
        private DataGridViewCheckBoxColumn EXCCHK;
        private DataGridViewTextBoxColumn INPUTNUM;
        private DataGridViewProgressBarColumn PROGRESS;
        private DataGridViewTextBoxColumn COMPDAY;
        private DataGridViewTextBoxColumn DESCRIPTION;
    }
}