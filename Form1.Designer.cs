namespace Task
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            addbtn = new Button();
            editbtn = new Button();
            deletebtn = new Button();
            descriptionTextBox = new TextBox();
            dateTimePicker = new DateTimePicker();
            label3 = new Label();
            taskContentTextBox = new TextBox();
            label5 = new Label();
            dueDatetxt = new TextBox();
            label6 = new Label();
            inputNumtxt = new TextBox();
            dgv = new DataGridView();
            CHECOMP = new DataGridViewCheckBoxColumn();
            INPUTNUM = new DataGridViewTextBoxColumn();
            OYA = new DataGridViewTextBoxColumn();
            DESCRIPTION = new DataGridViewTextBoxColumn();
            DUEDATE = new DataGridViewTextBoxColumn();
            URL = new DataGridViewLinkColumn();
            SPEC = new DataGridViewTextBoxColumn();
            RESULTS = new DataGridViewTextBoxColumn();
            REGDATE = new DataGridViewTextBoxColumn();
            Savebtn = new Button();
            label7 = new Label();
            cmbComp = new ComboBox();
            label1 = new Label();
            panel1 = new Panel();
            TaskReportbtn = new Button();
            Oyakobtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // addbtn
            // 
            addbtn.Location = new Point(206, 82);
            addbtn.Name = "addbtn";
            addbtn.Size = new Size(75, 23);
            addbtn.TabIndex = 4;
            addbtn.Text = "追加";
            addbtn.UseVisualStyleBackColor = true;
            addbtn.Click += addbtn_Click;
            // 
            // editbtn
            // 
            editbtn.Location = new Point(534, 84);
            editbtn.Name = "editbtn";
            editbtn.Size = new Size(75, 23);
            editbtn.TabIndex = 11;
            editbtn.Text = "編集";
            editbtn.UseVisualStyleBackColor = true;
            editbtn.Click += editbtn_Click;
            // 
            // deletebtn
            // 
            deletebtn.Location = new Point(557, 0);
            deletebtn.Name = "deletebtn";
            deletebtn.Size = new Size(75, 23);
            deletebtn.TabIndex = 8;
            deletebtn.Text = "削除";
            deletebtn.UseVisualStyleBackColor = true;
            deletebtn.Click += deletebtn_Click;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(61, 33);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = ScrollBars.Vertical;
            descriptionTextBox.Size = new Size(220, 45);
            descriptionTextBox.TabIndex = 3;
            descriptionTextBox.Validated += descriptionTextBox_Validated;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(124, 6);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(124, 23);
            dateTimePicker.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(297, 36);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 7;
            label3.Text = "選択中のタイトル";
            // 
            // taskContentTextBox
            // 
            taskContentTextBox.Location = new Point(389, 33);
            taskContentTextBox.Multiline = true;
            taskContentTextBox.Name = "taskContentTextBox";
            taskContentTextBox.ScrollBars = ScrollBars.Vertical;
            taskContentTextBox.Size = new Size(220, 45);
            taskContentTextBox.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(331, 10);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 10;
            label5.Text = "期日変更";
            // 
            // dueDatetxt
            // 
            dueDatetxt.Location = new Point(389, 6);
            dueDatetxt.Name = "dueDatetxt";
            dueDatetxt.Size = new Size(84, 23);
            dueDatetxt.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1, 5);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 13;
            label6.Text = "タスク一覧";
            // 
            // inputNumtxt
            // 
            inputNumtxt.Location = new Point(61, 6);
            inputNumtxt.MaxLength = 3;
            inputNumtxt.Name = "inputNumtxt";
            inputNumtxt.Size = new Size(48, 23);
            inputNumtxt.TabIndex = 1;
            inputNumtxt.KeyPress += inputNumtxt_KeyPress;
            inputNumtxt.Validated += inputNumtxt_Validated;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { CHECOMP, INPUTNUM, OYA, DESCRIPTION, DUEDATE, URL, SPEC, RESULTS, REGDATE });
            dgv.Location = new Point(1, 111);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 25;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(652, 233);
            dgv.TabIndex = 5;
            dgv.CellContentClick += dgv_CellContentClick;
            dgv.CellMouseDoubleClick += dgv_CellMouseDoubleClick;
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.ColumnHeaderMouseClick += dgv_ColumnHeaderMouseClick;
            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;
            dgv.SelectionChanged += dgv_SelectionChanged;
            // 
            // CHECOMP
            // 
            CHECOMP.DataPropertyName = "CHECOMP";
            CHECOMP.FalseValue = "0";
            CHECOMP.HeaderText = "済";
            CHECOMP.Name = "CHECOMP";
            CHECOMP.TrueValue = "1";
            CHECOMP.Width = 30;
            // 
            // INPUTNUM
            // 
            INPUTNUM.DataPropertyName = "INPUTNUM";
            INPUTNUM.HeaderText = "No";
            INPUTNUM.MaxInputLength = 3;
            INPUTNUM.Name = "INPUTNUM";
            INPUTNUM.ReadOnly = true;
            INPUTNUM.Resizable = DataGridViewTriState.True;
            INPUTNUM.SortMode = DataGridViewColumnSortMode.NotSortable;
            INPUTNUM.Width = 30;
            // 
            // OYA
            // 
            OYA.DataPropertyName = "OYA";
            OYA.HeaderText = "PNo";
            OYA.MaxInputLength = 3;
            OYA.Name = "OYA";
            OYA.ReadOnly = true;
            OYA.SortMode = DataGridViewColumnSortMode.NotSortable;
            OYA.Width = 35;
            // 
            // DESCRIPTION
            // 
            DESCRIPTION.DataPropertyName = "DESCRIPTION";
            DESCRIPTION.HeaderText = "タイトル";
            DESCRIPTION.Name = "DESCRIPTION";
            DESCRIPTION.ReadOnly = true;
            DESCRIPTION.SortMode = DataGridViewColumnSortMode.NotSortable;
            DESCRIPTION.Width = 250;
            // 
            // DUEDATE
            // 
            DUEDATE.DataPropertyName = "DUEDATE";
            DUEDATE.HeaderText = "期限";
            DUEDATE.Name = "DUEDATE";
            DUEDATE.ReadOnly = true;
            DUEDATE.SortMode = DataGridViewColumnSortMode.NotSortable;
            DUEDATE.Width = 70;
            // 
            // URL
            // 
            URL.DataPropertyName = "URL";
            URL.HeaderText = "URL";
            URL.Name = "URL";
            URL.ReadOnly = true;
            URL.Resizable = DataGridViewTriState.True;
            URL.Width = 50;
            // 
            // SPEC
            // 
            SPEC.DataPropertyName = "SPEC";
            SPEC.HeaderText = "仕様書";
            SPEC.Name = "SPEC";
            SPEC.ReadOnly = true;
            SPEC.Resizable = DataGridViewTriState.True;
            SPEC.SortMode = DataGridViewColumnSortMode.NotSortable;
            SPEC.Width = 50;
            // 
            // RESULTS
            // 
            RESULTS.DataPropertyName = "RESULTS";
            RESULTS.HeaderText = "テスト結果";
            RESULTS.Name = "RESULTS";
            RESULTS.ReadOnly = true;
            RESULTS.Resizable = DataGridViewTriState.True;
            RESULTS.SortMode = DataGridViewColumnSortMode.NotSortable;
            RESULTS.Width = 65;
            // 
            // REGDATE
            // 
            REGDATE.DataPropertyName = "REGDATE";
            REGDATE.HeaderText = "登録日";
            REGDATE.Name = "REGDATE";
            REGDATE.ReadOnly = true;
            REGDATE.SortMode = DataGridViewColumnSortMode.NotSortable;
            REGDATE.Width = 70;
            // 
            // Savebtn
            // 
            Savebtn.Location = new Point(476, 0);
            Savebtn.Name = "Savebtn";
            Savebtn.Size = new Size(75, 23);
            Savebtn.TabIndex = 6;
            Savebtn.Text = "保存";
            Savebtn.UseVisualStyleBackColor = true;
            Savebtn.Click += Savebtn_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(2, 10);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 16;
            label7.Text = "チケットNo";
            // 
            // cmbComp
            // 
            cmbComp.FormattingEnabled = true;
            cmbComp.Location = new Point(62, 1);
            cmbComp.Name = "cmbComp";
            cmbComp.Size = new Size(67, 23);
            cmbComp.TabIndex = 22;
            cmbComp.SelectedIndexChanged += cmbComp_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 37);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 23;
            label1.Text = "タイトル";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSize = true;
            panel1.Controls.Add(Savebtn);
            panel1.Controls.Add(TaskReportbtn);
            panel1.Controls.Add(deletebtn);
            panel1.Controls.Add(cmbComp);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(8, 349);
            panel1.Name = "panel1";
            panel1.Size = new Size(635, 27);
            panel1.TabIndex = 24;
            // 
            // TaskReportbtn
            // 
            TaskReportbtn.Location = new Point(346, 0);
            TaskReportbtn.Name = "TaskReportbtn";
            TaskReportbtn.Size = new Size(75, 23);
            TaskReportbtn.TabIndex = 25;
            TaskReportbtn.Text = "作業報告";
            TaskReportbtn.UseVisualStyleBackColor = true;
            TaskReportbtn.Click += TaskReportbtn_Click;
            // 
            // Oyakobtn
            // 
            Oyakobtn.Enabled = false;
            Oyakobtn.Location = new Point(484, 6);
            Oyakobtn.Name = "Oyakobtn";
            Oyakobtn.Size = new Size(75, 23);
            Oyakobtn.TabIndex = 29;
            Oyakobtn.Text = "親子設定";
            Oyakobtn.UseVisualStyleBackColor = true;
            Oyakobtn.Click += Oyakobtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 381);
            Controls.Add(Oyakobtn);
            Controls.Add(dgv);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(inputNumtxt);
            Controls.Add(label7);
            Controls.Add(dueDatetxt);
            Controls.Add(label5);
            Controls.Add(taskContentTextBox);
            Controls.Add(label3);
            Controls.Add(dateTimePicker);
            Controls.Add(descriptionTextBox);
            Controls.Add(editbtn);
            Controls.Add(addbtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MaximumSize = new Size(670, 441);
            MinimumSize = new Size(670, 400);
            Name = "Form1";
            Text = "ToDoApp";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addbtn;
        private Button editbtn;
        private Button deletebtn;
        private TextBox descriptionTextBox;
        private DateTimePicker dateTimePicker;
        private Label label3;
        private TextBox taskContentTextBox;
        private Label label5;
        private TextBox dueDatetxt;
        private Label label6;
        private Button openbtn;
        private TextBox inputNumtxt;
        private DataGridView dgv;
        private Button Savebtn;
        private Label label7;
        private Button incompbtn;
        private Button compbtn;
        private ComboBox cmbComp;
        private Label label1;
        private Panel panel1;
        private Button TaskReportbtn;
        private Button Oyakobtn;
        private DataGridViewCheckBoxColumn CHECOMP;
        private DataGridViewTextBoxColumn INPUTNUM;
        private DataGridViewTextBoxColumn OYA;
        private DataGridViewTextBoxColumn DESCRIPTION;
        private DataGridViewTextBoxColumn DUEDATE;
        private DataGridViewLinkColumn URL;
        private DataGridViewTextBoxColumn SPEC;
        private DataGridViewTextBoxColumn RESULTS;
        private DataGridViewTextBoxColumn REGDATE;
    }
}