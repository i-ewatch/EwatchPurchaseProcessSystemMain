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
    public partial class PurchasePlanBrowsingForm : DevExpress.XtraEditors.XtraForm
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
        public PurchasePlanBrowsingForm()
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

        private void BrowsingsimpleButton_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource != null)
            {
                gridView1.Columns.Clear();
            }
            #region 請購計畫顯示
            string grammar = $"USE [PurchaseProcessSystemDB] Select * FROM PurchasePlan Where ProjectNO = '{comboBoxEdit1.Text}' Order By ProjectCode";
            DataTable dataTable = SQLMethod.OutPutTable(grammar);
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].BestFit();
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].BestFit();
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[3].BestFit();
            gridView1.Columns[3].Caption = "項次";
            gridView1.Columns[4].BestFit();
            gridView1.Columns[4].Caption = "請購編號";
            gridView1.Columns[5].BestFit();
            gridView1.Columns[5].Caption = "請購人";
            gridView1.Columns[6].BestFit();
            gridView1.Columns[6].Caption = "請購內容";
            gridView1.Columns[7].BestFit();
            gridView1.Columns[7].Caption = "預計掛件日期";
            gridView1.Columns[8].BestFit();
            gridView1.Columns[8].Caption = "實際掛件日期";
            gridView1.Columns[9].BestFit();
            gridView1.Columns[9].Caption = "採購承辦";
            gridView1.Columns[10].BestFit();
            gridView1.Columns[10].Caption = "預定決商日";
            gridView1.Columns[11].BestFit();
            gridView1.Columns[11].Caption = "實際決商日";
            gridView1.Columns[12].BestFit();
            gridView1.Columns[12].Caption = "長交期設備";
            gridView1.Columns[13].BestFit();
            gridView1.Columns[13].Caption = "聯繫單編號";
            gridView1.Columns[14].BestFit();
            gridView1.Columns[14].Caption = "異常單編號";
            gridView1.Columns[15].BestFit();
            gridView1.Columns[15].Caption = "廠商";
            gridView1.Columns[16].BestFit();
            gridView1.Columns[16].Caption = "訂單編號";
            gridView1.Columns[17].BestFit();
            gridView1.Columns[17].Caption = "聯絡人";
            gridView1.Columns[18].BestFit();
            gridView1.Columns[18].Caption = "連絡電話";
            gridView1.Columns[19].BestFit();
            gridView1.Columns[19].Caption = "估算成本";
            gridView1.Columns[20].BestFit();
            gridView1.Columns[20].Caption = "執行目標(A)";
            gridView1.Columns[21].BestFit();
            gridView1.Columns[21].Caption = "已發包金額";
            gridView1.Columns[22].BestFit();
            gridView1.Columns[22].Caption = "未發包金額";
            gridView1.Columns[23].BestFit();
            gridView1.Columns[23].Caption = "小計(B)";
            gridView1.Columns[24].BestFit();
            gridView1.Columns[24].Caption = "差異金額(A)-(B)";
            gridView1.Columns[25].BestFit();
            gridView1.Columns[25].Caption = "累積已計價金額";
            gridView1.Columns[26].BestFit();
            gridView1.Columns[26].Caption = "備註";
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            #endregion
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text != null)
            {
                BrowsingsimpleButton.Enabled = true;
            }
            else
            {
                BrowsingsimpleButton.Enabled = false;
            }
        }
    }
}