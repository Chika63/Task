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
        #region �t�B�[���h

        //���͏��i�[�e�[�u��
        private DataTable _dtcurrent = new DataTable("List");

        //�t�@�C���p�X
        private string xmlPath = Properties.Settings.Default.TASKFILE;

        //�c�[���`�b�v�\���p
        private ToolTip Tool = ToolTipMessageBase.ToolTipMessage();

        //�G���[�J���[
        private Color ErrColor = Color.LightCoral;

        //�J�ڗp�t�H���_�p�X
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
            ������,
            ����,
            �S��
        }

        #endregion

        #region �R���X�g���N�^
        public Form1()
        {
            InitializeComponent();

            // �ۑ��t�@�C���������Load�������܂�
            if (File.Exists(xmlPath))
            {
                // Load����
                LoadXMLToDataTable(this._dataset, xmlPath);
            }

            //�e�[�u���쐬
            SetDtTable(this._dataset.Tables["LIST"]);

        }

        #endregion


        #region �C�x���g


        //Load�����C�x���g
        private void Form1_Load(object sender, EventArgs e)
        {

            //���e�L�X�g�{�b�N�X�������t�H�[�J�X�ɂ���
            ActiveControl = inputNumtxt;

            //�S�I����Ԃ���������    
            this.inputNumtxt.SelectionStart = 0;

            //�L�����b�g�ʒu�i�����̓��͈ʒu�j�𖖔��ɐݒ肷��
            this.inputNumtxt.Select(this.inputNumtxt.Text.Length, 0);

            //�L�[�C�x���g���t�H�[���Ŏ󂯎��
            this.KeyPreview = true;

            //KeyDown�C�x���g�n���h����ǉ�
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            cmbComp.Items.Add(DataCmb.������);
            cmbComp.Items.Add(DataCmb.����);
            cmbComp.Items.Add(DataCmb.�S��);
            cmbComp.SelectedIndex = 0;

            // �ꗗ�f�[�^���Q���ȏ゠��ꍇ�͊�����
            bool oyaCheck = this._dtcurrent.Rows.Count > 1 ? true : false;
            this.Oyakobtn.Enabled = oyaCheck;
        }

        //�ǉ��{�^���N���b�N�C�x���g
        private void addbtn_Click(object sender, EventArgs e)
        {
            //�V�K�쐬
            if (this.inputNumtxt.BackColor == ErrColor)
            {
                // �`�P�b�gNo���d�����Ă���ꍇ�̓t�H�[�J�X
                this.inputNumtxt.Focus();
            }
            else if (this.inputNumtxt.Text.Length == 3 && !string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                //�`�P�b�gNo�̐ݒ�l���R���ł���B���@���e��񂪐ݒ肳��Ă���Έȉ�����
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

                // ���͏��i6�[�e�[�u���Ƀf�[�^�ǉ�
                this._dtcurrent.Rows.Add(row.ItemArray);
                dgv.DataSource = this._dtcurrent;

                ClearForm();
            }
            else
            {
                //���̓t�H�[���ɐݒ�l���Ȃ��ꍇ�͐ԕ\��
                if (string.IsNullOrEmpty(descriptionTextBox.Text))
                {
                    this.descriptionTextBox.BackColor = ErrColor;
                    Tool.SetToolTip(this.descriptionTextBox, "���͂��Ă�������");
                    this.descriptionTextBox.Focus();
                }
                else
                {
                    Tool.SetToolTip(this.inputNumtxt, "");
                    this.descriptionTextBox.BackColor = SystemColors.Window;
                }

                if (string.IsNullOrEmpty(inputNumtxt.Text) || this.inputNumtxt.Text.Length < 3)
                {
                    // �`�P�b�g�m������3���ɖ����Ȃ��ꍇ
                    this.inputNumtxt.BackColor = ErrColor;
                    Tool.SetToolTip(this.inputNumtxt, "3�����͂��Ă�������");
                    this.inputNumtxt.Focus();
                }
            }

            // �ꗗ�f�[�^���Q���ȏ゠��ꍇ�͊�����
            bool oyaCheck = this._dtcurrent.Rows.Count > 1 ? true : false;
            this.Oyakobtn.Enabled = oyaCheck;
        }



        //�ۑ��{�^���N���b�N�C�x���g
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists(xmlPath))
            {
                //�w���ɐV�K��xml�t�@�C�����쐬���܂��B

                //SaveFileDialog�N���X�̃C���X�^���X���쐬
                SaveFileDialog sfd = new SaveFileDialog();

                //�͂��߂̃t�@�C�������w�肷��
                //�͂��߂Ɂu�t�@�C�����v�ŕ\������镶������w�肷��
                sfd.FileName = "tasks.xml";

                //�͂��߂ɕ\�������t�H���_���w�肷��
                sfd.InitialDirectory = (string)Properties.Settings.Default["TASKFILE"];

                //[�t�@�C���̎��]�ɕ\�������I�������w�肷��
                //�w�肵�Ȃ��i��̕�����j�̎��́A���݂̃f�B���N�g�����\�������
                sfd.Filter = "HTML�t�@�C��(*.html;*.htm)|*.html;*.htm|���ׂẴt�@�C��(*.*)|*.*";

                //[�t�@�C���̎��]�ł͂��߂ɑI���������̂��w�肷��
                //2�Ԗڂ́u���ׂẴt�@�C���v���I������Ă���悤�ɂ���
                sfd.FilterIndex = 2;

                //�^�C�g����ݒ肷��
                sfd.Title = "�t�@�C���̕ۑ����I�����Ă�������";

                //�_�C�A���O�{�b�N�X�����O�Ɍ��݂̃f�B���N�g���𕜌�����悤�ɂ���
                sfd.RestoreDirectory = true;

                //���ɑ��݂���t�@�C�������w�肵���Ƃ��x������
                //�f�t�H���g��True�Ȃ̂Ŏw�肷��K�v�͂Ȃ�
                sfd.OverwritePrompt = true;

                //���݂��Ȃ��p�X���w�肳�ꂽ�Ƃ��x����\������
                //�f�t�H���g��True�Ȃ̂Ŏw�肷��K�v�͂Ȃ�
                sfd.CheckPathExists = true;


                /* �����t�H���_ */
                //sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                //�_�C�A���O��\������
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OK�{�^�����N���b�N���ꂽ�Ƃ��A
                    //�I�����ꂽ���O�ŐV�����t�@�C�����쐬���A
                    //�ǂݏ����A�N�Z�X���ł��̃t�@�C�����J���B
                    //�����̃t�@�C�����I�����ꂽ�Ƃ��̓f�[�^�������鋰�ꂠ��B
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

        // �ҏW�{�^���N���b�N�C�x���g
        private void editbtn_Click(object sender, EventArgs e)
        {
            // �O���b�h�Ƀf�[�^��1���ȏ㑶�݂���ꍇ
            if (dgv.Rows.Count != 0)
            {
                // dgv�I���s�̍s�C���f�b�N�X���擾 
                int iRow = dgv.CurrentCell.RowIndex;
                // dgv�̓��e����I�𒆂̃^�X�N���ɕύX
                dgv.Rows[iRow].Cells[2].Value = taskContentTextBox.Text;
                // dgv�̊������������ύX�̐ݒ�l�ɕύX
                dgv.Rows[iRow].Cells[4].Value = dueDatetxt.Text;
            }
        }

        //�폜�{�^���N���b�N�C�x���g
        private void deletebtn_Click(object sender, EventArgs e)
        {
            //�O���b�h�f�[�^�����݂���ꍇ
            if (dgv.Rows.Count != 0)
            {
                //�I���s���擾
                int iRow = dgv.CurrentCell.RowIndex;
                DataRow[] rows = this._dtcurrent.Select("", "INPUTNUM DESC");
                DataRow dr = rows[iRow];
                this._dtcurrent.Rows.Remove(dr);
                dgv.DataSource = this._dtcurrent;


            }
        }

        //�N���A�N���b�N�C�x���g
        private void clearbtn_Click(object sender, EventArgs e)
        {
            this._dtcurrent.RejectChanges();
            //�O���b�h�Ƀf�[�^�\��
            dgv.DataSource = this._dtcurrent;
        }

        //�`�P�b�gNo���͌�C�x���g
        private void inputNumtxt_Validated(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            // �O���b�h�ɓ���No������΃G���[
            var emptyRows = dt.AsEnumerable()
               .Where(row =>
                    row.Field<string>("INPUTNUM") == this.inputNumtxt.Text.ToString());

            if (emptyRows.Any())
            {
                Tool.SetToolTip(this.inputNumtxt, string.Format("����{0}�͑��݂��Ă��܂��B", this.inputNumtxt.Text));
                this.inputNumtxt.BackColor = ErrColor;
            }
            else if (!string.IsNullOrEmpty(this.inputNumtxt.Text) && this.inputNumtxt.Text.Length < 3)
            {
                //�`�P�b�gNo��3���ɖ����Ȃ����̓c�[���`�b�v�\���B
                Tool.SetToolTip(this.inputNumtxt, "3�����͂��Ă�������");
                this.inputNumtxt.BackColor = ErrColor;
            }
            else
            {
                Tool.SetToolTip(this.inputNumtxt, "");
                this.inputNumtxt.BackColor = SystemColors.Window;
            }


        }

        //���̓t�H�[�����͌�C�x���g
        private void descriptionTextBox_Validated(object sender, EventArgs e)
        {
            //�`�P�b�gNo��3���ɖ����Ȃ����̓c�[���`�b�v�\���B
            if (string.IsNullOrEmpty(this.descriptionTextBox.Text) && this.descriptionTextBox.Text.Length > 0)
            {
                Tool.SetToolTip(this.inputNumtxt, "3�����͂��Ă�������");
                this.descriptionTextBox.BackColor = ErrColor;
            }
            else
            {
                Tool.SetToolTip(this.descriptionTextBox, "");
                this.descriptionTextBox.BackColor = SystemColors.Window;
            }
        }

        //�f�[�^�O���b�h�@�I���s�؂�ւ��C�x���g
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.Rows.Count != 0)
            {
                //���ݍs���擾���܂��B
                int iRow = dgv.CurrentCell.RowIndex;

                //�����ύX�ɑI���������t��ݒ肵�܂��B
                dueDatetxt.Text = Convert.ToDateTime(dgv.Rows[iRow].Cells["DUEDATE"].Value).ToString("yyyy/MM/dd");

                //�d�l���A�e�X�g���ʂ̓��e���擾���܂��B
                string Spec = dgv.Rows[iRow].Cells["SPEC"].Value.ToString();
                string Results = dgv.Rows[iRow].Cells["RESULTS"].Value.ToString();
                string Url = dgv.Rows[iRow].Cells["URL"].Value.ToString();
                this._filePass.Clear();
                this._filePass.Add(Spec);
                this._filePass.Add(Results);
                this._filePass.Add(Url);


                //�I�𒆂̃^�X�N�ɓ��e����ݒ肵�܂��B
                this.taskContentTextBox.Text = dgv.Rows[iRow].Cells["DESCRIPTION"].Value.ToString();


                // No��PNo���قȂ�ꍇ�͐e�q�ݒ�̃{�^���s��
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
            // DisplayMember��ValueMember�ɂ̓v���p�e�B�Ŏw�肷��d�g��
            public String ItemDisp { get; set; }
            public int ItemValue { get; set; }

            // �v���p�e�B���R���X�g���N�^�ŃZ�b�g
            public ItemSet(int v, String s)
            {
                ItemDisp = s;
                ItemValue = v;
            }
        }


        // �L�[�_�E������
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter�L�[��������Ă��邩�m�F
            //Alt��Ctrl�L�[��������Ă��鎞�͖�������
            if ((e.KeyCode == Keys.Enter)
                && !e.Alt && !e.Control)
            {
                //��������Tab�L�[�������ꂽ���̂悤�ɂ���
                //Shift��������Ă��鎞�͑O�̃R���g���[���̃t�H�[�J�X���ړ�
                this.ProcessTabKey(!e.Shift);

                e.Handled = true;
                //.NET Framework 2.0�ȍ~
                e.SuppressKeyPress = true;
            }
        }

        private void inputNumtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0�`9�ƁA�o�b�N�X�y�[�X�ȊO�̎��́A�C�x���g���L�����Z������
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        // �f�[�^�O���b�h�i�Z���̒l�ύX�C�x���g�j
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.DataSource != null && dgv.Rows.Count > 0)
            {

                string filwhere = "";

                if (cmbComp.SelectedIndex == ((int)DataCmb.������))
                {
                    //�^�X�N�ꗗ�̑I�����������̏ꍇ�@
                    filwhere = "CHECOMP = 0";
                }
                else if (cmbComp.SelectedIndex == ((int)DataCmb.����))
                {
                    //�^�X�N�ꗗ�̑I���������ς̏ꍇ
                    filwhere = "CHECOMP = 1";
                }
                else
                {
                    filwhere = "";
                }
                this._dtcurrent = (DataTable)dgv.DataSource;
                this._dtcurrent.DefaultView.RowFilter = filwhere;

                // �O���b�h�ɃZ�b�g���܂��B
                dgv.DataSource = this._dtcurrent;

            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentCellAddress.X == 0 &&
                dgv.IsCurrentCellDirty)
            {
                //�R�~�b�g����
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            }
        }


        #endregion

        #region�@���\�b�h

        //Load����
        private void LoadXMLToDataTable(DataSet dataSet, string xmlPath)
        {
            //xml�t�@�C����ǂݍ��݂܂�
            dataSet.ReadXml(xmlPath);
        }

        private void SetDtTable(DataTable dtList)
        {
            // List�i���C���j�e�[�u���ɏ�񂪂Ȃ��ꍇ�͐V�K�쐬���܂�
            if (dtList == null)
            {
                // ����
                this._dtcurrent.Columns.Add("CHECOMP", typeof(int));
                // No
                this._dtcurrent.Columns.Add("INPUTNUM", typeof(string));
                // ���e
                this._dtcurrent.Columns.Add("DESCRIPTION", typeof(string));
                // URL
                this._dtcurrent.Columns.Add("URL", typeof(string));
                // ����
                this._dtcurrent.Columns.Add("DUEDATE", typeof(DateTime));
                // �d�l��
                this._dtcurrent.Columns.Add("SPEC", typeof(string));
                // �e�X�g����
                this._dtcurrent.Columns.Add("RESULTS", typeof(string));
                // �o�^��
                this._dtcurrent.Columns.Add("REGDATE", typeof(DateTime));
                // �e
                this._dtcurrent.Columns.Add("OYA", typeof(string));
                // �ڍ�
                this._dtcurrent.Columns.Add("DETAIL", typeof(string));

                dtList = this._dtcurrent;


            }

            // List�̃f�[�^�����O���b�h�ɃZ�b�g���܂�
            dtList.DefaultView.Sort = "INPUTNUM DESC";
            dgv.DataSource = dtList;
            this._dtcurrent = dtList;
            //this._dtcurrent = dtList.Clone();
            //foreach (DataRowView drv in dtList.DefaultView)
            //{
            //    this._dtcurrent.ImportRow(drv.Row);
            //}

            // ���e�ڍחp�̃e�[�u����p�ӂ��܂��B
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
            MessageBox.Show("�ۑ��������܂���",
                "����",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ClearForm()
        {
            dateTimePicker.Value = DateTime.Now;
            descriptionTextBox.Text = string.Empty;
            // ���e�L�X�g���N���A���܂��B
            inputNumtxt.Clear();
        }

        #endregion


        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Return�L�[��������Ă��邩���ׂ�
            //Alt��Ctrl�L�[��������Ă��鎞�́A�{���̓����������

            if (((keyData & Keys.KeyCode) == Keys.Return) &&
                ((keyData & (Keys.Alt | Keys.Control)) == Keys.None))
            {
                //Tab�L�[�����������Ɠ��������������
                //Shift�L�[��������Ă��鎞�́A�t���ɂ���
                this.ProcessTabKey((keyData & Keys.Shift) != Keys.Shift);
                //�{���̏����͂����Ȃ�
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

            // �O���b�h�̓��e���_�u���N���b�N�ŕʉ�ʑ@��
            // ���e��I��
            if (colName.IndexOf(DataColumns.DESCRIPTION.ToString()) == 0)
            {
                // �I���s�̏����擾����
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
            // URL�E�d�l���E�e�X�g���ʂ��N���b�N�ňȉ��������s
            else if (colName.IndexOf(DataColumns.URL.ToString()) == 0 ||
                colName.IndexOf(DataColumns.SPEC.ToString()) == 0 ||
                colName.IndexOf(DataColumns.RESULTS.ToString()) == 0)
            {

                // �t�@�C���w���ɑJ�ڂ���
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
            //"URL"��Ȃ�΁A�{�^�����N���b�N���ꂽ
            if (dgv.Columns[e.ColumnIndex].Name == "URL")
            {

                DataGridViewLinkCell cell =
                    (DataGridViewLinkCell)dgv[e.ColumnIndex, e.RowIndex];
                //�K��ς݂ɂ���
                cell.LinkVisited = true;

                ProcessStartInfo pi = new ProcessStartInfo()
                {
                    FileName = cell.Value.ToString(),
                    UseShellExecute = true,
                };

                Process.Start(pi);


            }
        }

        //�R���{�{�b�N�X�l�ύX�C�x���g
        private void cmbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // �f�[�^�O���b�h�ɏ�񂪂���ꍇ
            if ((DataTable)dgv.DataSource != null)
            {

                string filwhere = "";

                if (cmbComp.SelectedIndex == ((decimal)DataCmb.������))
                {
                    //�^�X�N�ꗗ�̑I�����������̏ꍇ�@
                    filwhere = "CHECOMP = 0";
                }
                else if (cmbComp.SelectedIndex == ((decimal)DataCmb.����))
                {
                    //�^�X�N�ꗗ�̑I���������ς̏ꍇ
                    filwhere = "CHECOMP = 1";
                }
                else
                {
                    filwhere = "";
                }

                // �t�B���^�[�������܂�
                this._dtcurrent = (DataTable)dgv.DataSource;
                this._dtcurrent.DefaultView.RowFilter = filwhere;

                // �O���b�h�ɃZ�b�g���܂��B
                dgv.DataSource = this._dtcurrent;
            }

        }

        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // �N���b�N���ꂽ��̖��O���擾
            string columnName = dgv.Columns[e.ColumnIndex].Name;

            // �\�[�g��������
            string sortOrder = "ASC";
            if (dgv.Tag?.ToString() == columnName + " ASC")
            {
                sortOrder = "DESC";
            }

            // DataTable��DefaultView�Ń\�[�g��K�p
            _dtcurrent.DefaultView.Sort = columnName + " " + sortOrder;

            // �\�[�g����ۑ�
            dgv.Tag = columnName + " " + sortOrder;
        }

        private void TaskReportbtn_Click(object sender, EventArgs e)
        {
            // �t�@�C���w���ɑJ�ڂ���
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
            // �ꗗ�f�[�^���擾
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

            // Form5����߂��Ă����Ƃ��̏���
            if (refForm.DialogResult == DialogResult.OK)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["OYA"].ToString() == cRow.Cells["INPUTNUM"].Value.ToString())
                    {
                        row["OYA"] = DBNull.Value; // NULL�ɂ���
                    }
                }

                string receivedValue = refForm.ticktNo;
                if (!string.IsNullOrEmpty(receivedValue))
                {
                    string[] inputNum = receivedValue.Split(',');
                    // �󂯎�����l�𗘗p���鏈��
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

            // �I���s��PNo���ݒ肳��Ă��Ȃ��ꍇ�͑I���Ȃ��̃f�[�^�𒊏o
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
                    MessageBox.Show("�q�ɐݒ�\��No������܂���B");
                    return null;
                }
                else
                {
                    emptyRows = result.ToArray().CopyToDataTable();
                }
            }
            else
            {
                // �I���s��PNo���ݒ�ς݂�No��PNo���قȂ��Ă��郌�R�[�h
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