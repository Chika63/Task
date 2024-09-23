using ServiceStack;
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
    public partial class Form4 : Form
    {
        private DataSet _setDt = null;

        private const int PaddingHeight = 5;

        public DataSet setDt
        {
            set { this._setDt = value; }
        }

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DateTime dttime = DateTime.Now;
            string ymdtime = dttime.ToString("yyyy/MM/dd");
            int noListcnt = 0;
            // 画面に表示
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("本日の作業報告になります。");
            sb.AppendLine("");
            // 完了項目が当日分を抽出します。
            DataTable compdt = new DataTable();

            if (this._setDt != null)
            {

                for (int i = 0; i < this._setDt.Tables.Count; i++)
                {
                    if (this._setDt.Tables[i].TableName != "List")
                    {
                        string tname = this._setDt.Tables[i].TableName;
                        DataTable srt = (DataTable)this._setDt.Tables[tname];
                        DataRow[] rows = srt.Select(string.Format("SELCMB = '{0}' AND COMPDAY = '{1}'", "対応済", ymdtime));
                        if (rows != null && rows.Length > 0)
                        {
                            compdt = rows.CopyToDataTable();

                        }
                        else
                        {
                            compdt.Clear();
                        }
                        if (compdt.Rows.Count > 0)
                        {
                            int index = 1;
                            sb.AppendLine(this._setDt.Tables["List"].Rows[noListcnt]["DESCRIPTION"].ToString());
                            sb.AppendLine(this._setDt.Tables["List"].Rows[noListcnt]["URL"].ToString());
                            foreach (DataRow row in compdt.Rows)
                            {

                                string numberedIndex = GetCircledNumber(index);
                                sb.Append(numberedIndex.Insert(0, "  "));
                                sb.Append(row["DESCRIPTION"].ToString());
                                sb.AppendLine("【完了】");
                                index++;
                            }
                        }
                        sb.AppendLine("");
                        noListcnt++;
                    }
                }

            }

            this.textBox1.Text = sb.ToString();
        }

        // インデックスを丸囲み数字に変換するメソッド
        static string GetCircledNumber(int number)
        {
            if (number < 1 || number > 20)
                throw new ArgumentOutOfRangeException("number", "Number must be between 1 and 20");

            char circledNumber = (char)(0x2460 + number - 1);
            return circledNumber.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            // テキストの行数を計算
            int lineCount = textBox1.GetLineFromCharIndex(textBox1.Text.Length) + 1;

            // テキストボックスのフォントの高さを取得
            int fontHeight = TextRenderer.MeasureText("A", textBox1.Font).Height;

            // 新しいテキストボックスの高さを計算
            int newTextBoxHeight = lineCount * fontHeight;

            // フォームの高さを調整
            this.Height = newTextBoxHeight + PaddingHeight;
        }
    }
}
