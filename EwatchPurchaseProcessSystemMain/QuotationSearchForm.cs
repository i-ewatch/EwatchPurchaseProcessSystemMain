using DevExpress.XtraEditors;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel;
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
    public partial class QuotationSearchForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// 專案查詢內容
        /// </summary>
        private List<DispatchDataTable> searchdispatchdatatable;
        public QuotationSearchForm()
        {
            InitializeComponent();
            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
        }

        private void comboBoxEdit1_Properties_MouseEnter(object sender, EventArgs e)
        {
            comboBoxEdit1.Properties.Items.Clear();
            searchdispatchdatatable = SQLMethod.Search_dispatchdatatable();
            for (int i = 0; i < searchdispatchdatatable.Count; i++)
            {
                comboBoxEdit1.Properties.Items.Add(searchdispatchdatatable[i].projectno);
            }
        }

        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource != null)
            {
                gridView1.Columns.Clear();
            }
            string grammar = $"USE [PurchaseProcessSystemDB] Select ProjectItem,ProjectName,ProjectUnit,ProjectAmount,Price,Money,Remark,ProjectCode FROM Costofferform Where ProjectNO = '{comboBoxEdit1.Text}'";
            DataTable dataTable = SQLMethod.OutPutTable(grammar);
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[0].Caption = "項次";
            gridView1.Columns[1].BestFit();
            gridView1.Columns[1].Caption = "名稱";
            gridView1.Columns[2].BestFit();
            gridView1.Columns[2].Caption = "單位";
            gridView1.Columns[3].BestFit();
            gridView1.Columns[3].Caption = "數量";
            gridView1.Columns[4].BestFit();
            gridView1.Columns[4].Caption = "單價";
            gridView1.Columns[5].BestFit();
            gridView1.Columns[5].Caption = "金額";
            gridView1.Columns[6].BestFit();
            gridView1.Columns[6].Caption = "備註";
            gridView1.Columns[7].BestFit();
            gridView1.Columns[7].Caption = "請購編碼";
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text != null)
            {
                SearchsimpleButton.Enabled = true;
            }
            else
            {
                SearchsimpleButton.Enabled = false;
            }
        }
    }
}