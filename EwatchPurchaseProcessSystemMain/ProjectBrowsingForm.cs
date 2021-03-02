using DevExpress.XtraBars;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.Method;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchPurchaseProcessSystemMain
{
    public partial class ProjectBrowsingForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        public ProjectBrowsingForm()
        {
            InitializeComponent();
        }

        private void ProjectBrowsingForm_Load(object sender, EventArgs e)
        {
            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
            string grammar = "USE [PurchaseProcessSystemDB] Select projectdatettime,projectno,ownername,projectleader,projectworkleader,ponumber FROM DispatchDataTable Order By projectdatettime";
            DataTable dataTable = SQLMethod.OutPutTable(grammar);
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[0].Caption = "日期";
            gridView1.Columns[1].BestFit();
            gridView1.Columns[1].Caption = "專案編號";
            gridView1.Columns[2].BestFit();
            gridView1.Columns[2].Caption = "業主名稱";
            gridView1.Columns[3].BestFit();
            gridView1.Columns[3].Caption = "專案負責人";
            gridView1.Columns[4].BestFit();
            gridView1.Columns[4].Caption = "專案工地負責人";
            gridView1.Columns[5].BestFit();
            gridView1.Columns[5].Caption = "PO單號碼";
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
        }
    }
}