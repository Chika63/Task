using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using ServiceStack;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;
using System.Web;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Microsoft.VisualBasic;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace Task
{
    public partial class Form1 : Form
    {
        #region フィールド

        //入力情報格納テーブル
        private DataTable _dtcurrent = new DataTable("List");

        //ファイルパス
        private string xmlPath = Properties.Settings.Default.TASKFILE;

        //ツールチップ表示用
        private ToolTip Tool = ToolTipMessageBase.ToolTipMessage();

        //エラーカラー
        private Color ErrColor = Color.LightCoral;

        //遷移用フォルダパス
        List<string> _filePass = new List<string>();

        private DataTable _dtresult = new DataTable("LIST2");

        private DataSet _dataset = new DataSet();

        enum DataColumns
        {
            CHECOMP,
            SELCMB,
            INPUTNUM,
            DESCRIPTION,
            DUEDATE,
            URL,
            SPEC,
            RESULTS,
            REGDATE,
            OYA,
            DETAIL
        }
        enum DataCmb
        {
            未完了,
            完了,
            全て
        }

        #endregion

        #region コンストラクタ
        public Form1()
        {
            InitializeComponent();

            // 保存ファイルがあればLoad処理します
            if (File.Exists(xmlPath))
            {
                // Load処理
                LoadXMLToDataTable(this._dataset, xmlPath);
            }

            //テーブル作成
            SetDtTable(this._dataset.Tables["LIST"]);

        }

        #endregion


        #region イベント


        //Load処理イベント
        private void Form1_Load(object sender, EventArgs e)
        {

            //＃テキストボックスを初期フォーカスにする
            ActiveControl = inputNumtxt;

            //全選択状態を解除する    
            this.inputNumtxt.SelectionStart = 0;

            //キャレット位置（文字の入力位置）を末尾に設定する
            this.inputNumtxt.Select(this.inputNumtxt.Text.Length, 0);

            //キーイベントをフォームで受け取る
            this.KeyPreview = true;

            //KeyDownイベントハンドラを追加
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            cmbComp.Items.Add(DataCmb.未完了);
            cmbComp.Items.Add(DataCmb.完了);
            cmbComp.Items.Add(DataCmb.全て);
            cmbComp.SelectedIndex = 0;

            // 一覧データが２件以上ある場合は活性化
            bool oyaCheck = this._dtcurrent.Rows.Count > 1 ? true : false;
            this.Oyakobtn.Enabled = oyaCheck;
        }

        //追加ボタンクリックイベント
        private void addbtn_Click(object sender, EventArgs e)
        {
            //新規作成
            if (this.inputNumtxt.BackColor == ErrColor)
            {
                // チケットNoが重複している場合はフォーカス
                this.inputNumtxt.Focus();
            }
            else if (this.inputNumtxt.Text.Length == 3 && !string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                //チケットNoの設定値が３桁である。かつ　内容情報が設定されていれば以下処理
                DataRow row = _dtcurrent.NewRow();
                row["CHECOMP"] = "0";
                row["INPUTNUM"] = inputNumtxt.Text;
                row["DESCRIPTION"] = descriptionTextBox.Text;
                row["URL"] = DBNull.Value;
                row["DUEDATE"] = dateTimePicker.Value.ToString("yyyy/MM/dd");
                row["SPEC"] = DBNull.Value;
                row["RESULTS"] = DBNull.Value;
                row["REGDATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                row["OYA"] = DBNull.Value;
                row["DETAIL"] = DBNull.Value;

                // 入力情報格6納テーブルにデータ追加
                this._dtcurrent.Rows.Add(row.ItemArray);
                dgv.DataSource = this._dtcurrent;

                ClearForm();
            }
            else
            {
                //入力フォームに設定値がない場合は赤表示
                if (string.IsNullOrEmpty(descriptionTextBox.Text))
                {
                    this.descriptionTextBox.BackColor = ErrColor;
                    Tool.SetToolTip(this.descriptionTextBox, "入力してください");
                    this.descriptionTextBox.Focus();
                }
                else
                {
                    Tool.SetToolTip(this.inputNumtxt, "");
                    this.descriptionTextBox.BackColor = SystemColors.Window;
                }

                if (string.IsNullOrEmpty(inputNumtxt.Text) || this.inputNumtxt.Text.Length < 3)
                {
                    // チケットＮｏが空か3桁に満たない場合
                    this.inputNumtxt.BackColor = ErrColor;
                    Tool.SetToolTip(this.inputNumtxt, "3桁入力してください");
                    this.inputNumtxt.Focus();
                }
            }

            // 一覧データが２件以上ある場合は活性化
            bool oyaCheck = this._dtcurrent.Rows.Count > 1 ? true : false;
            this.Oyakobtn.Enabled = oyaCheck;
        }



        //保存ボタンクリックイベント
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(xmlPath))
            {
                //指定先に新規でxmlファイルを作成します。

                //SaveFileDialogクラスのインスタンスを作成
                SaveFileDialog sfd = new SaveFileDialog();

                //はじめのファイル名を指定する
                //はじめに「ファイル名」で表示される文字列を指定する
                sfd.FileName = "tasks.xml";

                //はじめに表示されるフォルダを指定する
                sfd.InitialDirectory = (string)Properties.Settings.Default["TASKFILE"];

                //[ファイルの種類]に表示される選択肢を指定する
                //指定しない（空の文字列）の時は、現在のディレクトリが表示される
                sfd.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";

                //[ファイルの種類]ではじめに選択されるものを指定する
                //2番目の「すべてのファイル」が選択されているようにする
                sfd.FilterIndex = 2;

                //タイトルを設定する
                sfd.Title = "ファイルの保存先を選択してください";

                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;

                //既に存在するファイル名を指定したとき警告する
                //デフォルトでTrueなので指定する必要はない
                sfd.OverwritePrompt = true;

                //存在しないパスが指定されたとき警告を表示する
                //デフォルトでTrueなので指定する必要はない
                sfd.CheckPathExists = true;


                /* 初期フォルダ */
                //sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                //ダイアログを表示する
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき、
                    //選択された名前で新しいファイルを作成し、
                    //読み書きアクセス許可でそのファイルを開く。
                    //既存のファイルが選択されたときはデータが消える恐れあり。
                    System.IO.Stream stream;
                    stream = sfd.OpenFile();
                    if (stream != null)
                    {
                        xmlPath = ((System.IO.FileStream)stream).Name;
                        Properties.Settings.Default["TASKFILE"] = xmlPath;
                        Properties.Settings.Default.Save();
                        stream.Close();
                    }
                }
            }
            else
            {
                if (this._dataset.Tables.IndexOf(this._dtcurrent) < 0)
                {
                    this._dataset.Tables.Add(this._dtcurrent);

                }

                SaveDataTableToXML(this._dataset, xmlPath);
                dgv.DataSource = this._dtcurrent;
                this._dtcurrent.AcceptChanges();
            }
        }

        // 編集ボタンクリックイベント
        private void editbtn_Click(object sender, EventArgs e)
        {
            // グリッドにデータが1件以上存在する場合
            if (dgv.Rows.Count != 0)
            {
                // dgv選択行の行インデックスを取得 
                int iRow = dgv.CurrentCell.RowIndex;
                // dgvの内容欄を選択中のタスク情報に変更
                dgv.Rows[iRow].Cells[2].Value = taskContentTextBox.Text;
                // dgvの期限欄を期日変更の設定値に変更
                dgv.Rows[iRow].Cells[4].Value = dueDatetxt.Text;
            }
        }

        //削除ボタンクリックイベント
        private void deletebtn_Click(object sender, EventArgs e)
        {
            //グリッドデータが存在する場合
            if (dgv.Rows.Count != 0)
            {
                //選択行を取得
                int iRow = dgv.CurrentCell.RowIndex;
                DataRow[] rows = this._dtcurrent.Select("", "INPUTNUM DESC");
                DataRow dr = rows[iRow];
                this._dtcurrent.Rows.Remove(dr);
                dgv.DataSource = this._dtcurrent;


            }
        }

        //クリアクリックイベント
        private void clearbtn_Click(object sender, EventArgs e)
        {
            this._dtcurrent.RejectChanges();
            //グリッドにデータ表示
            dgv.DataSource = this._dtcurrent;
        }

        //チケットNo入力後イベント
        private void inputNumtxt_Validated(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            // グリッドに同じNoがあればエラー
            var emptyRows = dt.AsEnumerable()
               .Where(row =>
                    row.Field<string>("INPUTNUM") == this.inputNumtxt.Text.ToString());

            if (emptyRows.Any())
            {
                Tool.SetToolTip(this.inputNumtxt, string.Format("既に{0}は存在しています。", this.inputNumtxt.Text));
                this.inputNumtxt.BackColor = ErrColor;
            }
            else if (!string.IsNullOrEmpty(this.inputNumtxt.Text) && this.inputNumtxt.Text.Length < 3)
            {
                //チケットNoが3桁に満たない時はツールチップ表示。
                Tool.SetToolTip(this.inputNumtxt, "3桁入力してください");
                this.inputNumtxt.BackColor = ErrColor;
            }
            else
            {
                Tool.SetToolTip(this.inputNumtxt, "");
                this.inputNumtxt.BackColor = SystemColors.Window;
            }


        }

        //入力フォーム入力後イベント
        private void descriptionTextBox_Validated(object sender, EventArgs e)
        {
            //チケットNoが3桁に満たない時はツールチップ表示。
            if (string.IsNullOrEmpty(this.descriptionTextBox.Text) && this.descriptionTextBox.Text.Length > 0)
            {
                Tool.SetToolTip(this.inputNumtxt, "3桁入力してください");
                this.descriptionTextBox.BackColor = ErrColor;
            }
            else
            {
                Tool.SetToolTip(this.descriptionTextBox, "");
                this.descriptionTextBox.BackColor = SystemColors.Window;
            }
        }

        //データグリッド　選択行切り替えイベント
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.Rows.Count != 0)
            {
                //現在行を取得します。
                int iRow = dgv.CurrentCell.RowIndex;

                //期日変更に選択した日付を設定します。
                dueDatetxt.Text = Convert.ToDateTime(dgv.Rows[iRow].Cells["DUEDATE"].Value).ToString("yyyy/MM/dd");

                //仕様書、テスト結果の内容を取得します。
                string Spec = dgv.Rows[iRow].Cells["SPEC"].Value.ToString();
                string Results = dgv.Rows[iRow].Cells["RESULTS"].Value.ToString();
                string Url = dgv.Rows[iRow].Cells["URL"].Value.ToString();
                this._filePass.Clear();
                this._filePass.Add(Spec);
                this._filePass.Add(Results);
                this._filePass.Add(Url);


                //選択中のタスクに内容情報を設定します。
                this.taskContentTextBox.Text = dgv.Rows[iRow].Cells["DESCRIPTION"].Value.ToString();


                // NoとPNoが異なる場合は親子設定のボタン不可
                if (!string.IsNullOrEmpty(dgv.Rows[iRow].Cells["OYA"].Value.ToString()) &&
                    dgv.Rows[iRow].Cells["INPUTNUM"].Value.ToString() != dgv.Rows[iRow].Cells["OYA"].Value.ToString())
                {
                    this.Oyakobtn.Enabled = false;
                }
                else
                {
                    this.Oyakobtn.Enabled = true;

                }
            }
        }


        public class ItemSet
        {
            // DisplayMemberとValueMemberにはプロパティで指定する仕組み
            public String ItemDisp { get; set; }
            public int ItemValue { get; set; }

            // プロパティをコンストラクタでセット
            public ItemSet(int v, String s)
            {
                ItemDisp = s;
                ItemValue = v;
            }
        }


        // キーダウン処理
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Enterキーが押されているか確認
            //AltかCtrlキーが押されている時は無視する
            if ((e.KeyCode == Keys.Enter)
                && !e.Alt && !e.Control)
            {
                //あたかもTabキーが押されたかのようにする
                //Shiftが押されている時は前のコントロールのフォーカスを移動
                this.ProcessTabKey(!e.Shift);

                e.Handled = true;
                //.NET Framework 2.0以降
                e.SuppressKeyPress = true;
            }
        }

        private void inputNumtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        // データグリッド（セルの値変更イベント）
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.DataSource != null && dgv.Rows.Count > 0)
            {

                string filwhere = "";

                if (cmbComp.SelectedIndex == ((int)DataCmb.未完了))
                {
                    //タスク一覧の選択が未完了の場合　
                    filwhere = "CHECOMP = 0";
                }
                else if (cmbComp.SelectedIndex == ((int)DataCmb.完了))
                {
                    //タスク一覧の選択が完了済の場合
                    filwhere = "CHECOMP = 1";
                }
                else
                {
                    filwhere = "";
                }
                this._dtcurrent = (DataTable)dgv.DataSource;
                this._dtcurrent.DefaultView.RowFilter = filwhere;

                // グリッドにセットします。
                dgv.DataSource = this._dtcurrent;

            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentCellAddress.X == 0 &&
                dgv.IsCurrentCellDirty)
            {
                //コミットする
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            }
        }


        #endregion

        #region　メソッド

        //Load処理
        private void LoadXMLToDataTable(DataSet dataSet, string xmlPath)
        {
            //xmlファイルを読み込みます
            dataSet.ReadXml(xmlPath);
        }

        private void SetDtTable(DataTable dtList)
        {
            // List（メイン）テーブルに情報がない場合は新規作成します
            if (dtList == null)
            {
                // 完了
                this._dtcurrent.Columns.Add("CHECOMP", typeof(int));
                // No
                this._dtcurrent.Columns.Add("INPUTNUM", typeof(string));
                // 内容
                this._dtcurrent.Columns.Add("DESCRIPTION", typeof(string));
                // URL
                this._dtcurrent.Columns.Add("URL", typeof(string));
                // 期限
                this._dtcurrent.Columns.Add("DUEDATE", typeof(DateTime));
                // 仕様書
                this._dtcurrent.Columns.Add("SPEC", typeof(string));
                // テスト結果
                this._dtcurrent.Columns.Add("RESULTS", typeof(string));
                // 登録日
                this._dtcurrent.Columns.Add("REGDATE", typeof(DateTime));
                // 親
                this._dtcurrent.Columns.Add("OYA", typeof(string));
                // 詳細
                this._dtcurrent.Columns.Add("DETAIL", typeof(string));

                dtList = this._dtcurrent;


            }

            // Listのデータ情報をグリッドにセットします
            dtList.DefaultView.Sort = "INPUTNUM DESC";
            dgv.DataSource = dtList;
            this._dtcurrent = dtList;
            //this._dtcurrent = dtList.Clone();
            //foreach (DataRowView drv in dtList.DefaultView)
            //{
            //    this._dtcurrent.ImportRow(drv.Row);
            //}

            // 内容詳細用のテーブルを用意します。
            this._dtresult.Columns.Add("CHECOMP", typeof(int));
            this._dtresult.Columns.Add("COMPDAY", typeof(DateTime));
            this._dtresult.Columns.Add("DESCRIPTION", typeof(string));
        }

        private void SaveDataTableToXML(DataSet dtcurrent, string xmlPath)
        {
            using (FileStream fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                //XmlSerializer serializer = new XmlSerializer(dtcurrent.GetType());
                //serializer.Serialize(fs, dtcurrent);
                dtcurrent.WriteXml(fs, XmlWriteMode.WriteSchema);
            }
            MessageBox.Show("保存完了しました",
                "完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ClearForm()
        {
            dateTimePicker.Value = DateTime.Now;
            descriptionTextBox.Text = string.Empty;
            // ＃テキストをクリアします。
            inputNumtxt.Clear();
        }

        #endregion


        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Returnキーが押されているか調べる
            //AltかCtrlキーが押されている時は、本来の動作をさせる

            if (((keyData & Keys.KeyCode) == Keys.Return) &&
                ((keyData & (Keys.Alt | Keys.Control)) == Keys.None))
            {
                //Tabキーを押した時と同じ動作をさせる
                //Shiftキーが押されている時は、逆順にする
                this.ProcessTabKey((keyData & Keys.Shift) != Keys.Shift);
                //本来の処理はさせない
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            DataTable dt = (DataTable)dgv.DataSource;

            DataGridViewRow cRow = dgv.CurrentRow;
            string colName = cRow.Cells[e.ColumnIndex].OwningColumn.Name;

            DataTable emptyRows = new DataTable();
            if (!string.IsNullOrEmpty(cRow.Cells["OYA"].Value.ToString()))
            {
                StringBuilder sb = new StringBuilder();

                emptyRows = dt.AsEnumerable()
                       .Where(row =>
                            row.Field<string>("OYA") == cRow.Cells["OYA"].Value.ToString()
                            )
                       .ToList().CopyToDataTable();
            }

            // グリッドの内容をダブルクリックで別画面繊維
            // 内容を選択
            if (colName.IndexOf(DataColumns.DESCRIPTION.ToString()) == 0)
            {
                // 選択行の情報を取得する
                DataGridViewRow selectedRow = dgv.SelectedRows[0];
                DataRowView drv = (DataRowView)selectedRow.DataBoundItem;
                DataRow drow = (DataRow)drv.Row;
                Form2 refForm = new Form2();
                refForm.selectRow = drow;
                refForm.oyaRows = emptyRows;

                if (this._dataset.Tables[drow["INPUTNUM"].ToString()] != null)
                {
                    refForm.datatask = this._dataset.Tables[drow["INPUTNUM"].ToString()];
                }
                else
                {
                    DataTable dataTable = new DataTable(drow["INPUTNUM"].ToString());
                    dataTable.Columns.Add("EXCCHK", typeof(int));
                    dataTable.Columns.Add("INPUTNUM", typeof(string));
                    dataTable.Columns.Add("PROGRESS", typeof(int));
                    dataTable.Columns.Add("SELCMB", typeof(string));
                    dataTable.Columns.Add("COMPDAY", typeof(DateTime));
                    dataTable.Columns.Add("DESCRIPTION", typeof(string));
                    refForm.datatask = dataTable;
                    this._dataset.Tables.Add(dataTable);
                }

                refForm.dataSet = this._dataset;
                refForm.Owner = this;
                refForm.ShowDialog();

            }
            // URL・仕様書・テスト結果をクリックで以下処理実行
            else if (colName.IndexOf(DataColumns.URL.ToString()) == 0 ||
                colName.IndexOf(DataColumns.SPEC.ToString()) == 0 ||
                colName.IndexOf(DataColumns.RESULTS.ToString()) == 0)
            {

                // ファイル指定先に遷移する
                Form3 refForm = new Form3();
                refForm.filePass = _filePass;
                refForm.Owner = this;
                if (refForm.ShowDialog() == DialogResult.OK)
                {
                    return;
                }

                dgv.SelectionChanged -= dgv_SelectionChanged;
                dgv[(int)DataColumns.SPEC - 1, e.RowIndex].Value = string.IsNullOrEmpty(_filePass[0]) ? (object)string.Empty : _filePass[0];
                dgv[(int)DataColumns.RESULTS - 1, e.RowIndex].Value = string.IsNullOrEmpty(_filePass[1]) ? (object)string.Empty : _filePass[1];
                dgv[(int)DataColumns.URL - 2, e.RowIndex].Value = string.IsNullOrEmpty(_filePass[2]) ? (object)string.Empty : _filePass[2];
                dgv.SelectionChanged += dgv_SelectionChanged;
                //SaveDataTableToXML(this._dtcurrent, xmlPath);

                this._dtcurrent = (DataTable)dgv.DataSource;

            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            //"URL"列ならば、ボタンがクリックされた
            if (dgv.Columns[e.ColumnIndex].Name == "URL")
            {

                DataGridViewLinkCell cell =
                    (DataGridViewLinkCell)dgv[e.ColumnIndex, e.RowIndex];
                //訪問済みにする
                cell.LinkVisited = true;

                ProcessStartInfo pi = new ProcessStartInfo()
                {
                    FileName = cell.Value.ToString(),
                    UseShellExecute = true,
                };

                Process.Start(pi);


            }
        }

        //コンボボックス値変更イベント
        private void cmbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // データグリッドに情報がある場合
            if ((DataTable)dgv.DataSource != null)
            {

                string filwhere = "";

                if (cmbComp.SelectedIndex == ((decimal)DataCmb.未完了))
                {
                    //タスク一覧の選択が未完了の場合　
                    filwhere = "CHECOMP = 0";
                }
                else if (cmbComp.SelectedIndex == ((decimal)DataCmb.完了))
                {
                    //タスク一覧の選択が完了済の場合
                    filwhere = "CHECOMP = 1";
                }
                else
                {
                    filwhere = "";
                }

                // フィルター処理します
                this._dtcurrent = (DataTable)dgv.DataSource;
                this._dtcurrent.DefaultView.RowFilter = filwhere;

                // グリッドにセットします。
                dgv.DataSource = this._dtcurrent;
            }

        }

        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // クリックされた列の名前を取得
            string columnName = dgv.Columns[e.ColumnIndex].Name;

            // ソート順を決定
            string sortOrder = "ASC";
            if (dgv.Tag?.ToString() == columnName + " ASC")
            {
                sortOrder = "DESC";
            }

            // DataTableのDefaultViewでソートを適用
            _dtcurrent.DefaultView.Sort = columnName + " " + sortOrder;

            // ソート順を保存
            dgv.Tag = columnName + " " + sortOrder;
        }

        private void TaskReportbtn_Click(object sender, EventArgs e)
        {
            // ファイル指定先に遷移する
            Form4 refForm = new Form4();
            refForm.setDt = this._dataset;
            refForm.Owner = this;
            if (refForm.ShowDialog() == DialogResult.OK)
            {
                return;
            }
        }

        private void dgv_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = "Option1";
        }

        private void Oyakobtn_Click(object sender, EventArgs e)
        {
            // 一覧データを取得
            DataTable dt = (DataTable)dgv.DataSource;

            DataGridViewRow cRow = dgv.CurrentRow;

            DataTable emptyRows = oyakoData(dt, cRow);
            if (emptyRows == null)
            {
                return;
            }


            Form5 refForm = new Form5();
            refForm.setDt = emptyRows;
            refForm.ticktNo = cRow.Cells["INPUTNUM"].Value.ToString();
            refForm.Owner = this;
            refForm.ShowDialog();

            // Form5から戻ってきたときの処理
            if (refForm.DialogResult == DialogResult.OK)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["OYA"].ToString() == cRow.Cells["INPUTNUM"].Value.ToString())
                    {
                        row["OYA"] = DBNull.Value; // NULLにする
                    }
                }

                string receivedValue = refForm.ticktNo;
                if (!string.IsNullOrEmpty(receivedValue))
                {
                    string[] inputNum = receivedValue.Split(',');
                    // 受け取った値を利用する処理
                    foreach (string numNo in inputNum)
                    {
                        DataRow foundRow = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("INPUTNUM") == numNo);
                        foundRow["OYA"] = cRow.Cells["INPUTNUM"].Value.ToString();
                    }
                    DataRow selectRow = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("INPUTNUM") == cRow.Cells["INPUTNUM"].Value.ToString());
                    selectRow["OYA"] = cRow.Cells["INPUTNUM"].Value.ToString();
                }
            }
        }

        private DataTable oyakoData(DataTable dt, DataGridViewRow cRow)
        {

            DataTable emptyRows = null;

            // 選択行のPNoが設定されていない場合は選択なしのデータを抽出
            if (string.IsNullOrEmpty(cRow.Cells["OYA"].Value.ToString()))
            {
                var result = dt.AsEnumerable()
                    .Where(row =>
                        string.IsNullOrEmpty(row.Field<string>("OYA")) &&
                        row.Field<string>("INPUTNUM") != cRow.Cells["INPUTNUM"].Value.ToString()
                        )
                    .ToList();

                if (result.Count == 0)
                {
                    MessageBox.Show("子に設定可能なNoがありません。");
                    return null;
                }
                else
                {
                    emptyRows = result.ToArray().CopyToDataTable();
                }
            }
            else
            {
                // 選択行のPNoが設定済みでNoとPNoが異なっているレコード
                emptyRows = dt.AsEnumerable()
                   .Where(row =>
                        row.Field<string>("OYA") == cRow.Cells["INPUTNUM"].Value.ToString() &&
                        row.Field<string>("INPUTNUM") != cRow.Cells["INPUTNUM"].Value.ToString() ||
                        string.IsNullOrEmpty(row.Field<string>("OYA")))
                   .ToList().CopyToDataTable();

            }

            return emptyRows;
        }
    }
}