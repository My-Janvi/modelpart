using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace B205614
{
    public partial class Form1 : Form
    {
        public Form1()
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataTable dt1 = FillMasterTable();
            DataTable dt2 = FillDetailTable();
            dataSet.Tables.Add(dt1);
            dataSet.Tables.Add(dt2);

            DataColumn keyColumn = dataSet.Tables["MasterTable"].Columns["Vendor"];
            DataColumn foreignKeyColumn = dataSet.Tables["DetailTable"].Columns["Vendor"];
            dataSet.Relations.Add("Models", keyColumn, foreignKeyColumn);

            gridControl1.DataSource = dataSet.Tables["MasterTable"];
        }

        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl gridControl = sender as GridControl;
            GridView focusedView = gridControl.FocusedView as GridView;

            if (e.KeyCode == Keys.Tab && focusedView.Name == gridView2.Name)
            {
                if (IsLastFocusableColumn(focusedView, focusedView.FocusedColumn)
                    && focusedView.GetNextVisibleRow(focusedView.GetVisibleIndex(focusedView.FocusedRowHandle)) == GridControl.InvalidRowHandle)
                {
                    focusedView.CloseEditor();
                    focusedView.UpdateCurrentRow();
                }
            }
        }

        bool IsLastFocusableColumn(GridView view, GridColumn column)
        {
            foreach (GridColumn gc in view.Columns)
                if (gc.VisibleIndex > column.VisibleIndex && gc.OptionsColumn.AllowFocus == true)
                    return false;
            return true;
        }

        public static DataTable FillMasterTable()
        {
            DataTable _masterTable = new DataTable();
            _masterTable.TableName = "MasterTable";
            _masterTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
            _masterTable.Columns.Add(new DataColumn("Availability", typeof(bool)));
            _masterTable.Rows.Add(new object[] { "HP", true });
            _masterTable.Rows.Add(new object[] { "Dell", true });
            return _masterTable;
        }

        public static DataTable FillDetailTable()
        {
            DataTable _detailTable = new DataTable();
            _detailTable.TableName = "DetailTable";
            _detailTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
            _detailTable.Columns.Add(new DataColumn("Model", typeof(string)));
            _detailTable.Columns.Add(new DataColumn("Availability", typeof(bool)));
            _detailTable.Rows.Add(new object[] { "HP", "Pavillion", true });
            _detailTable.Rows.Add(new object[] { "HP", "Mini", true });
            _detailTable.Rows.Add(new object[] { "Dell", "Inspiron", true });
            _detailTable.Rows.Add(new object[] { "Dell", "Latitude", true });
            return _detailTable;
        }
    }
}
