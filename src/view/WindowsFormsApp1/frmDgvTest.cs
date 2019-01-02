using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class frmDgvTest : Form
    {
        public frmDgvTest()
        {
            InitializeComponent();
            InitData();
            //AddCheckBox();
            SetCheckBox();
        }

        private void InitData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("learning");
            dt.Columns.Add("number");
            dt.Columns.Add("name");
            dt.Columns.Add("class");
            DataRow dr = dt.NewRow();
            dr["learning"] = "1";
            dr["number"] = "1";
            dr["name"] = "jack";
            dr["class"] = "class 1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["learning"] = "1";
            dr["number"] = "2";
            dr["name"] = "tom";
            dr["class"] = "class 1";
            dt.Rows.Add(dr);
            AddCheckBox();
            dgvData.DataSource = dt;
            dgvData.Refresh();
        }

        private void AddCheckBox()
        {
            // DataGridViewCheckBoxColumn无法初始化是否选中，采用多种方法均没用效果，
            //但先建一个button控件可对其进行赋值，后改由DataBindingComplete可实现初始化
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                column.HeaderText = "learning";
                column.Name = "status";
                column.DataPropertyName = "status";
                column.FalseValue = false;
                column.TrueValue = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                column.ReadOnly = false;
            }
            dgvData.Columns.Insert(0, column);
            dgvData.Refresh();
        }

        private void SetCheckBox()
        {
            if (true)
            {
                ((DataGridViewCheckBoxCell)dgvData.Rows[0].Cells[0]).Value = true;
            }
            dgvData.EndEdit();
            dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dgvData.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetCheckBox();
        }

        
        private void dgvData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {//用于实时更新cell值，修改后会马上提交缓冲区的数据，进而激活
            dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {//用于实时更新cell值

        }

        private void dgvData_Layout(object sender, LayoutEventArgs e)
        {//没用
            ((DataGridViewCheckBoxCell)dgvData.Rows[0].Cells[0]).Value = true;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {//没用
            ((DataGridViewCheckBoxCell)dgvData.Rows[0].Cells[0]).Value = true;
        }

        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {//有用
            ((DataGridViewCheckBoxCell)dgvData.Rows[0].Cells[0]).Value = true;
        }

        private void dgvData_DataSourceChanged(object sender, EventArgs e)
        {//没用
            ((DataGridViewCheckBoxCell)dgvData.Rows[0].Cells[0]).Value = true;
        }
    }
}
