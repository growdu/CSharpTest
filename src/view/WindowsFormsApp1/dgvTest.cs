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
            AddCheckBox();
        }

        private void InitData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("number");
            dt.Columns.Add("name");
            dt.Columns.Add("class");
            DataRow dr = dt.NewRow();
            dr["number"] = "1";
            dr["name"] = "jack";
            dr["class"] = "class 1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["number"] = "2";
            dr["name"] = "tom";
            dr["class"] = "class 1";
            dt.Rows.Add(dr);
            dgvData.DataSource = dt;
            dgvData.Refresh();
        }

        private void AddCheckBox()
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                column.HeaderText = "learning";
                column.Name = "status";
                column.FalseValue = false;
                column.TrueValue = true;
                //column.AutoSizeMode =
                //    DataGridViewAutoSizeColumnMode.DisplayedCells;
                //column.FlatStyle = FlatStyle.Standard;
                //column.ThreeState = true;
                //column.CellTemplate = new DataGridViewCheckBoxCell();
                //column.CellTemplate.Style.BackColor = Color.Blue;
            }
            dgvData.Columns.Insert(0, column);
        }

        private void SetCheckBox()
        {
            dgvData.Rows[0].Cells[0].Value =true ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetCheckBox();
        }
    }
}
