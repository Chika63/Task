namespace Task
{
    partial class Form5
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
            ticketlbl = new Label();
            ticketNotxt = new TextBox();
            label1 = new Label();
            checkedListBox1 = new CheckedListBox();
            Savebtn = new Button();
            SuspendLayout();
            // 
            // ticketlbl
            // 
            ticketlbl.AutoSize = true;
            ticketlbl.Location = new Point(23, 10);
            ticketlbl.Name = "ticketlbl";
            ticketlbl.Size = new Size(59, 15);
            ticketlbl.TabIndex = 0;
            ticketlbl.Text = "チケットNO";
            // 
            // ticketNotxt
            // 
            ticketNotxt.Location = new Point(88, 7);
            ticketNotxt.Name = "ticketNotxt";
            ticketNotxt.ReadOnly = true;
            ticketNotxt.Size = new Size(52, 23);
            ticketNotxt.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 38);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 2;
            label1.Text = "子を選択してください";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(23, 56);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(456, 184);
            checkedListBox1.TabIndex = 3;
            // 
            // Savebtn
            // 
            Savebtn.Location = new Point(404, 246);
            Savebtn.Name = "Savebtn";
            Savebtn.Size = new Size(75, 23);
            Savebtn.TabIndex = 7;
            Savebtn.Text = "保存";
            Savebtn.UseVisualStyleBackColor = true;
            Savebtn.Click += Savebtn_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 281);
            Controls.Add(Savebtn);
            Controls.Add(checkedListBox1);
            Controls.Add(label1);
            Controls.Add(ticketNotxt);
            Controls.Add(ticketlbl);
            Name = "Form5";
            Text = "親子情報";
            Load += Form5_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ticketlbl;
        private TextBox ticketNotxt;
        private Label label1;
        private CheckedListBox checkedListBox1;
        private Button Savebtn;
    }
}