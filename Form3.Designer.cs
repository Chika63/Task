namespace Task
{
    partial class Form3
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
            label1 = new Label();
            Resultstxt = new TextBox();
            Spectxt = new TextBox();
            label7 = new Label();
            addbtn = new Button();
            btnSpecOpen = new Button();
            btnResultsOpen = new Button();
            btnResultsSet = new Button();
            btnSpecSet = new Button();
            Urltxt = new TextBox();
            label2 = new Label();
            btnUrlOpen = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 47);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 22;
            label1.Text = "テスト結果";
            // 
            // Resultstxt
            // 
            Resultstxt.Location = new Point(74, 44);
            Resultstxt.MaxLength = 100;
            Resultstxt.Name = "Resultstxt";
            Resultstxt.Size = new Size(292, 23);
            Resultstxt.TabIndex = 20;
            Resultstxt.TextChanged += Resultstxt_TextChanged;
            // 
            // Spectxt
            // 
            Spectxt.Location = new Point(74, 12);
            Spectxt.MaxLength = 100;
            Spectxt.Name = "Spectxt";
            Spectxt.Size = new Size(292, 23);
            Spectxt.TabIndex = 10;
            Spectxt.TextChanged += Spectxt_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 15);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 21;
            label7.Text = "仕様書";
            // 
            // addbtn
            // 
            addbtn.BackColor = SystemColors.ActiveCaption;
            addbtn.Location = new Point(438, 74);
            addbtn.Name = "addbtn";
            addbtn.Size = new Size(57, 23);
            addbtn.TabIndex = 99;
            addbtn.Text = "ADD";
            addbtn.UseVisualStyleBackColor = false;
            addbtn.Click += addbtn_Click;
            // 
            // btnSpecOpen
            // 
            btnSpecOpen.Location = new Point(438, 12);
            btnSpecOpen.Name = "btnSpecOpen";
            btnSpecOpen.Size = new Size(57, 23);
            btnSpecOpen.TabIndex = 12;
            btnSpecOpen.Text = "OPEN";
            btnSpecOpen.UseVisualStyleBackColor = true;
            btnSpecOpen.Click += btnSpecOpen_Click;
            // 
            // btnResultsOpen
            // 
            btnResultsOpen.Location = new Point(438, 44);
            btnResultsOpen.Name = "btnResultsOpen";
            btnResultsOpen.Size = new Size(57, 23);
            btnResultsOpen.TabIndex = 22;
            btnResultsOpen.Text = "OPEN";
            btnResultsOpen.UseVisualStyleBackColor = true;
            btnResultsOpen.Click += btnResultsOpen_Click;
            // 
            // btnResultsSet
            // 
            btnResultsSet.Location = new Point(372, 44);
            btnResultsSet.Name = "btnResultsSet";
            btnResultsSet.Size = new Size(57, 23);
            btnResultsSet.TabIndex = 21;
            btnResultsSet.Text = "SET";
            btnResultsSet.UseVisualStyleBackColor = true;
            btnResultsSet.Click += btnResultsSet_Click;
            // 
            // btnSpecSet
            // 
            btnSpecSet.Location = new Point(372, 12);
            btnSpecSet.Name = "btnSpecSet";
            btnSpecSet.Size = new Size(57, 23);
            btnSpecSet.TabIndex = 11;
            btnSpecSet.Text = "SET";
            btnSpecSet.UseVisualStyleBackColor = true;
            btnSpecSet.Click += btnSpecSet_Click;
            // 
            // Urltxt
            // 
            Urltxt.Location = new Point(74, 74);
            Urltxt.MaxLength = 100;
            Urltxt.Name = "Urltxt";
            Urltxt.Size = new Size(292, 23);
            Urltxt.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 78);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 29;
            label2.Text = "URL";
            // 
            // btnUrlOpen
            // 
            btnUrlOpen.Location = new Point(372, 74);
            btnUrlOpen.Name = "btnUrlOpen";
            btnUrlOpen.Size = new Size(57, 23);
            btnUrlOpen.TabIndex = 100;
            btnUrlOpen.Text = "OPEN";
            btnUrlOpen.UseVisualStyleBackColor = true;
            btnUrlOpen.Click += btnUrlOpen_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 114);
            Controls.Add(btnUrlOpen);
            Controls.Add(Urltxt);
            Controls.Add(label2);
            Controls.Add(btnResultsSet);
            Controls.Add(btnSpecSet);
            Controls.Add(btnResultsOpen);
            Controls.Add(btnSpecOpen);
            Controls.Add(addbtn);
            Controls.Add(label1);
            Controls.Add(Resultstxt);
            Controls.Add(Spectxt);
            Controls.Add(label7);
            Name = "Form3";
            Text = "ファイル指定先";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox Resultstxt;
        private TextBox Spectxt;
        private Label label7;
        private Button addbtn;
        private Button btnSpecOpen;
        private Button btnResultsOpen;
        private Button btnResultsSet;
        private Button btnSpecSet;
        private TextBox Urltxt;
        private Label label2;
        private Button btnUrlOpen;
    }
}