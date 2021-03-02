using DevExpress.XtraReports.UI;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.Method;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace EwatchPurchaseProcessSystemMain.ReportView
{
    public partial class ReportFQ : DevExpress.XtraReports.UI.XtraReport
    {
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        public ReportFQ()
        {
            InitializeComponent();
            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
        }
        public void Textchange(string projectno, string projectcode, string buyno, string projectpurchaseer, string branch, string project, string appdate, string buylimitdate, string needdate, string pickup, string deliery, bool devicecheck, bool materialcheck, bool constructioncheck, bool hangcheck, bool elsecheck, bool inagreement, bool noagreement, bool just, bool suggest, bool remark)
        {
            ProjectNOxrLabel.Text = projectno +"-"+ projectcode;
            BuyNOxrLabel.Text = buyno;
            ProjectPurchaserxrTableCell.Text = projectpurchaseer;
            BranchxrTableCell.Text = branch;
            ProjectxrTableCell.Text = project;
            ApplicationDatexrTableCell.Text = appdate;
            BuyLimitDatexrTableCell.Text = buylimitdate;
            NeedDatexrTableCell.Text = needdate;
            PickupxrTableCell.Text = pickup;
            DeliveryxrTableCell.Text = deliery;
            DevicexrCheckBox.Checked = devicecheck;
            MaterialxrCheckBox.Checked = materialcheck;
            ConstructionxrCheckBox.Checked = constructioncheck;
            HangxrCheckBox.Checked = hangcheck;
            ElsexrCheckBox.Checked = elsecheck;
            InAgreementxrCheckBox.Checked = inagreement;
            NoAgreementxrCheckBox8.Checked = noagreement;
            JustItxrCheckBox9.Checked = just;
            SuggestxrCheckBox.Checked = suggest;
            RemarkxrCheckBox.Checked = remark;
        }
        public void DataxrCrossTabChange()
        {
            string projectnostring = ProjectNOxrLabel.Text.Split('-')[1] + '-' + ProjectNOxrLabel.Text.Split('-')[2];
            var grammar = $"USE [PurchaseProcessSystemDB] Select ProjectName as '名稱',ProjectUnit as '單位',ProjectAmount as '數量',Remark as '備註' FROM Costofferform Where ProjectCode = '{projectnostring}'";
            DataTable dataTable = SQLMethod.OutPutTable(grammar);

            // Create a table and add it to the detail band.
            XRTable xRTable = new XRTable();
            Detail.Controls.Add(xRTable);
            int numRows = dataTable.Rows.Count;
            int numCols = dataTable.Columns.Count;
            xRTable.BeginInit();
            for (int i = 0; i < numRows; i++)
            {
                XRTableRow xRTableRow = new XRTableRow();
                xRTable.Rows.Add(xRTableRow);
                for (int j = 0; j < numCols; j++)
                {
                    XRTableCell xRTableCell = new XRTableCell();
                    xRTable.Rows[i].Cells.Add(xRTableCell);
                    xRTableCell.Text = dataTable.Rows[i][j].ToString();
                }
            }
            xRTable.HeightF = 38 * numRows;
            xRTable.WidthF = 777;
            xRTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xRTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xRTable.EndInit();
        }
    }
}
