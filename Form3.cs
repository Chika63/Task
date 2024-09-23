using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Task
{
    public partial class Form3 : Form
    {

        private Color ErrColor = Color.LightCoral;

        private ToolTip Tool = ToolTipMessageBase.ToolTipMessage();

        //遷移用フォルダパス
        List<string> _filePass = new List<string>();



        public List<string> filePass
        {
            set { this._filePass = value; }
        }


        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (_filePass.Count > 0)
            {
                this.Spectxt.Text = _filePass[0];
                this.Resultstxt.Text = _filePass[1];
                this.Urltxt.Text = _filePass[2];
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            bool[] bools = new bool[3];

            if (Directory.Exists(Spectxt.Text))
            {
                //_filePass.Add(Spectxt.Text);
                _filePass[0] = Spectxt.Text;
            }
            else
            {
                if (Spectxt.Text.Length > 0)
                {
                    this.Spectxt.BackColor = ErrColor;
                    Tool.SetToolTip(this.Spectxt, "指定先が不正です");
                    bools[0] = true;
                }
                else
                {
                    //_filePass.Add(string.Empty);
                    _filePass[0] = string.Empty;
                }
            }
            if (Directory.Exists(Resultstxt.Text))
            {
                //_filePass.Add(Resultstxt.Text);
                _filePass[1] = Resultstxt.Text;
            }
            else
            {
                if (Resultstxt.Text.Length > 0)
                {
                    this.Resultstxt.BackColor = ErrColor;
                    Tool.SetToolTip(this.Resultstxt, "指定先が不正です");
                    bools[1] = true;
                }
                else
                {
                    //_filePass.Add(string.Empty);
                    _filePass[1] = string.Empty;
                }
            }

            //URLに間違いがある場合はエラー表示
            string url = Urltxt.Text;
            if (!IsValidUrl(url))
            {
                this.Urltxt.BackColor = ErrColor;
                Tool.SetToolTip(this.Urltxt, "URLが正しくありません");
                bools[2] = true;
                return;
            }

            if (Array.IndexOf(bools, true) == 1)
            {
                this.Resultstxt.Focus();
                return;
            }
            else if (Array.IndexOf(bools, true) == 0)
            {
                this.Spectxt.Focus();
                return;
            }
            else if (Array.IndexOf(bools, true) == 2)
            {
                this.Urltxt.Focus();
                return;
            }
            _filePass[2] = Urltxt.Text;

            this.Close();
        }

        public static bool IsValidUrl(string url)
        {
            bool bools = true;
            if (url != string.Empty)
            {
                bools = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            }
            return bools;
        }

        private void btnSpecSet_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofDialog = new OpenFileDialog();

            //仕様書が未設定の場合は、デフォルトでデスクトップ設定
            if (string.IsNullOrEmpty(Spectxt.Text))
            {
                ofDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else
            {
                // デフォルトのフォルダに値を設定
                ofDialog.InitialDirectory = Spectxt.Text;
            }

            //ダイアログのタイトルを指定する
            ofDialog.Title = "仕様書選択ダイアログ";

            //ダイアログを表示する
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                Spectxt.Text = System.IO.Path.GetDirectoryName(ofDialog.FileName);
            }

            // オブジェクトを破棄する
            ofDialog.Dispose();


        }

        private void btnResultsSet_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofDialog = new OpenFileDialog();

            //仕様書が未設定の場合は、デフォルトでデスクトップ設定
            if (string.IsNullOrEmpty(Resultstxt.Text))
            {
                ofDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else
            {
                // デフォルトのフォルダに値を設定
                ofDialog.InitialDirectory = Resultstxt.Text;
            }

            //ダイアログのタイトルを指定する
            ofDialog.Title = "テスト結果選択ダイアログ";

            //ダイアログを表示する
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                Resultstxt.Text = System.IO.Path.GetDirectoryName(ofDialog.FileName);
            }
            else
            {
                Console.WriteLine("キャンセルされました");
            }

            // オブジェクトを破棄する
            ofDialog.Dispose();
        }

        private void btnSpecOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = Spectxt.Text;
            System.Diagnostics.Process.Start("EXPLORER.EXE", ofDialog.InitialDirectory);
        }

        private void btnResultsOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = Resultstxt.Text;
            System.Diagnostics.Process.Start("EXPLORER.EXE", ofDialog.InitialDirectory);
        }

        private void Spectxt_TextChanged(object sender, EventArgs e)
        {
            this.Spectxt.BackColor = SystemColors.Window;
        }

        private void Resultstxt_TextChanged(object sender, EventArgs e)
        {
            this.Resultstxt.BackColor = SystemColors.Window;
        }

        private void btnUrlOpen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Urltxt.Text.ToString()))
            {
                ProcessStartInfo pi = new ProcessStartInfo()
                {
                    FileName = this.Urltxt.Text,
                    UseShellExecute = true,
                };

                Process.Start(pi);
            }
        }
    }
}
