using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form5 : Form
    {
        private DataTable _setDt = null;

        private string _ticktNo = string.Empty;

        public DataTable setDt
        {
            set { this._setDt = value; }
        }
        public string ticktNo
        {
            get { return this._ticktNo; }
            set { this._ticktNo = value; }
        }


        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.ticketNotxt.Text = this._ticktNo;
            int index = 0;
            foreach (DataRow row in this._setDt.Rows)
            {
                this.checkedListBox1.Items.Add(row["INPUTNUM"].ToString().PadRight(5) + row["DESCRIPTION"].ToString());

                // 
                bool isChecked = row["OYA"].ToString() == this._ticktNo.ToString() ? true : false;
                this.checkedListBox1.SetItemChecked(index, isChecked);
                index++;
            }

            this.checkedListBox1.SelectedIndex = 0;
            this.checkedListBox1.Select();
            this.checkedListBox1.Focus();

        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                sb.Append(item.ToString().Substring(0, 3));
                sb.Append(",");
            }

            // 末尾の余計なカンマを削除
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            ticktNo = sb.ToString();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
