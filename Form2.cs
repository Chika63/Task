using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task
{

    public partial class Form2 : Form
    {
        private DataGridView dataGridView;

        //データセット
        private DataTable _dttask = new DataTable("TASK");

        private DataRow _selectRow = null;

        private DataTable _oyaRows = null;

        private DataSet _dtset = null;

        private DataTable _dataTable;
        enum SELCNG
        {
            未対応,
            対応中,
            対応済
        }


        public DataTable datatask
        {
            set { this._dttask = value; }
        }

        public DataRow selectRow
        {
            set { this._selectRow = value; }
        }
        public DataSet dataSet
        {
            set { this._dtset = value; }
        }
        public DataTable oyaRows
        {
            set { this._oyaRows = value; }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.inputNumtxt.Text = this._selectRow["INPUTNUM"].ToString();
            this.Titletxt.Text = this._selectRow["DESCRIPTION"].ToString();
            this.Contenttxt.Text = this._selectRow["DETAIL"].ToString();
            if (this._dttask.Rows.Count == 0)
            {
                DataRow newRow = this._dttask.NewRow();
                newRow["INPUTNUM"] = this._selectRow["INPUTNUM"].ToString();
                this._dttask.Rows.Add(newRow);
            }

            this._dataTable = this._dttask;

            //DataGridViewComboBoxColumnを作成
            ComboSel();
            dgv.DataSource = this._dataTable;

            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



        }

        private void savebtn_Click(object sender, EventArgs e)
        {

            this._dttask = (DataTable)dgv.DataSource;
            this._selectRow["DETAIL"] = this.Contenttxt.Text;

            this.Close();
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 完了がチェックされたときに動作
            DataGridViewRow dataGridViewRow = dgv.CurrentRow;

            DataGridView dataGridView = sender as DataGridView;

            if (dataGridViewRow != null)
            {
                var dgvCell = dgv.CurrentCell;

                if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("COMPDAY") == 0)
                {
                    // 完了日
                    return;
                }
                else if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("DESCRIPTION") == 0)
                {
                    // 対応内容
                    return;
                }
                else if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("EXCCHK") == 0)
                {
                    // 仕チェック
                    return;
                }
                else if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("INPUTNUM") == 0)
                {
                    // NO
                    return;
                }
                else if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("PROGRESS") == 0)
                {
                    // 進捗率
                    return;
                }


                // 選択
                this.dgv.CellValueChanged -= dgv_CellValueChanged;

                if (dataGridViewRow.Cells[dgvCell.ColumnIndex].OwningColumn.DataPropertyName.IndexOf("SELCMB") == 0)
                {
                    // 現在日時を取得
                    DateTime datetime = DateTime.Now;

                    // 選択が変更された場合の処理
                    string isChecked = dataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();

                    // 選択が変更された場合
                    if (isChecked.IndexOf(SELCNG.対応済.ToString()) == 0)
                    {
                        // 対応済の時は完了日に現在日を設定する。
                        dataGridViewRow.Cells["COMPDAY"].Value = datetime.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        // 上記以外は空にする。
                        dataGridViewRow.Cells["COMPDAY"].Value = string.Empty;
                    }
                }


                this.dgv.CellValueChanged += dgv_CellValueChanged;

            }
        }

        private void ComboSel()
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            //ComboBoxのリストに表示する項目を設定する
            column.Items.Add(SELCNG.未対応.ToString());
            column.Items.Add(SELCNG.対応中.ToString());
            column.Items.Add(SELCNG.対応済.ToString());

            //表示する列の名前を設定する
            column.DataPropertyName = dgv.Columns["SELCMB"].DataPropertyName;
            //以下のようにしても同じ
            //現在Week列が存在している位置に挿入する
            dgv.Columns.Insert(dgv.Columns["SELCMB"].Index, column);
            dgv.Columns.Remove("SELCMB");


            //挿入した列の名前を「選択」とする
            column.Name = "選択";


            if (this._oyaRows.Rows.Count > 0)
            {
                string[] columnsToExtract = { "INPUTNUM" };
                DataView dv = new DataView(this._oyaRows);
                DataTable dtNew = dv.ToTable(false, columnsToExtract);
                DataRow newRow = dtNew.NewRow();
                newRow["INPUTNUM"] = "ALL";
                dtNew.Rows.Add(newRow);

                ticketcmb.DataSource = dtNew;
                ticketcmb.DisplayMember = "INPUTNUM";
                ticketcmb.ValueMember = "INPUTNUM";

                ticketcmb.SelectedValue = this._selectRow["INPUTNUM"].ToString();

            }


        }


        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            if (dgv.CurrentCell is DataGridViewCheckBoxCell && dgv.IsCurrentCellDirty)
            {
                // 編集が終了したことを通知して、CellValueChangedイベントを発生させる
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            else if (dgv.CurrentCellAddress.X == 1 && dgv.IsCurrentCellDirty)
            {
                //コミットする
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            //グリッドデータが存在する場合
            if (dgv.Rows.Count > 1)
            {

                //選択行を取得
                int iRow = dgv.CurrentCell.RowIndex;

                dgv.Rows.RemoveAt(iRow);

            }
        }

        private void ticketcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択された項目をstring型で取得
            string selectedValue = string.Empty;
            DataRowView drv = ticketcmb.SelectedValue as DataRowView;
            if (drv == null)
            {
                selectedValue = ticketcmb.SelectedValue.ToString();

                // グリッドの内容のみ変更する。
                DataTable allTable = this._selectRow.Table;

                DataTable dtc = this._dttask.Clone();
                if (!selectedValue.Contains("ALL"))
                {
                    this._dttask = this._dtset.Tables[selectedValue];
                    if (this._dttask != null)
                    {
                        dgv.DataSource = this._dttask;

                    }

                }
                else
                {

                    var oyasel = allTable.AsEnumerable()
                        .Where(row => row.Field<string>("INPUTNUM") == this._selectRow["OYA"].ToString()).FirstOrDefault();

                    string oyaNum = oyasel.IsNull("INPUTNUM") ? this._selectRow["INPUTNUM"].ToString() : oyasel["OYA"].ToString();


                    var selData = allTable.AsEnumerable()
                       .Where(row => row.Field<string>("OYA") == oyaNum)
                       .Select(row => row.Field<string>("INPUTNUM"));


                    foreach (var result in selData)
                    {
                        DataTable dt = this._dtset.Tables[result];

                        // 選択列の値がnullか空文字列の場合
                        var filteredRows = dt.AsEnumerable()
                            .Where(row => !row.IsNull("SELCMB") && !string.IsNullOrEmpty(row.Field<string>("SELCMB")));

                        DataTable dataTable = new DataTable();
                        if (filteredRows.Any())
                        {
                            dataTable = filteredRows.CopyToDataTable();
                        }

                        dtc.Merge(dataTable);
                    }
                    dgv.DataSource = dtc;
                }
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].
                    CellType == typeof(DataGridViewComboBoxCell))
            {
                // グリッドの最終行を取得
                DataGridViewRow lastRow = dgv.Rows[dgv.RowCount - 1];

                // 選択が空じゃなければ新規行追加
                if (!string.IsNullOrEmpty(lastRow.Cells["選択"].Value.ToString()))
                {
                    // 関連チケットが設定されていれば値を取得
                    int ticketparse;
                    string str = ticketcmb.SelectedValue == null ? string.Empty : ticketcmb.SelectedValue.ToString();

                    if (str != "ALL")
                    {
                        if (int.TryParse(str, out ticketparse))
                        {
                            str = ticketparse.ToString();
                        }

                        // 新しい行を追加
                        DataRow newRow = this._dttask.NewRow();
                        newRow["INPUTNUM"] = string.IsNullOrEmpty(str) ? this._selectRow["INPUTNUM"].ToString() : str;
                        this._dttask.Rows.Add(newRow);
                    }
                }
            }
        }
    }
}
