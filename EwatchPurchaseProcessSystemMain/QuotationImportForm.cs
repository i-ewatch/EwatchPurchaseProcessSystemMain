using DevExpress.XtraEditors;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel;
using EwatchPurchaseProcessSystemMain.Method;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchPurchaseProcessSystemMain
{
    public partial class QuotationImportForm : DevExpress.XtraEditors.XtraForm
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
        /// <summary>
        /// 檔案瀏覽
        /// </summary>
        public OpenFileDialog Openfile;
        /// <summary>
        /// 報表資料夾路徑
        /// </summary>
        public string ReportPath { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Ecexl檔案
        /// </summary>
        public XSSFWorkbook xworkbook { get; set; }//xlsx
        public List<ICell> cell1 = new List<ICell>();
        public List<ICell> cell2 = new List<ICell>();
        public List<ICell> cell3 = new List<ICell>();
        public List<ICell> cell4 = new List<ICell>();
        public List<ICell> cell5 = new List<ICell>();
        public List<ICell> cell6 = new List<ICell>();
        public List<ICell> cell7 = new List<ICell>();
        public List<ICell> cell8 = new List<ICell>();
        /// <summary>
        /// Costofferform資料List
        /// </summary>
        private List<Costofferform> costofferforms = new List<Costofferform>();
        /// <summary>
        /// Group Costofferform資料List
        /// </summary>
        private List<Costofferform> groupcostofferform = new List<Costofferform>();
        /// <summary>
        /// Purchaseplans資料List
        /// </summary>
        private List<PurchasePlan> purchaseplans = new List<PurchasePlan>();
        /// <summary>
        /// Purchaseplans Table表pk值
        /// </summary>
        int pk_number { get; set; }
        /// <summary>
        /// Purchaseplans Table表ProjectItem值
        /// </summary>
        int ProjectItem_number = 1;
        public QuotationImportForm()
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
            #region excel資料匯入
            Openfile = new OpenFileDialog() { Filter = "*.Xlsx| *.xlsx" };
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ReportPath = Openfile.FileName;
                    if (ReportPath != null)
                    {
                        using (FileStream file = new FileStream($"{ReportPath}", FileMode.Open, FileAccess.Read))
                        {
                            xworkbook = new XSSFWorkbook(file);//Ecexl檔案載入
                            int sheet = xworkbook.NumberOfSheets;//取得分頁數量
                            for (int Sheetnum = 1; Sheetnum < 2; Sheetnum++)
                            {
                                var data = xworkbook.GetSheetAt(Sheetnum);//載入分頁資訊
                                for (int Rownum = 9; Rownum < data.LastRowNum; Rownum++)//每一行資料
                                {
                                    IRow row = data.GetRow(Rownum);
                                    #region 資料抓取
                                    if (row.GetCell(0).ToString() == "" & row.GetCell(1).ToString() == "" & row.GetCell(2).ToString() == "" & row.GetCell(3).ToString() == "" & row.GetCell(4).ToString() == "" & row.GetCell(5).ToString() == "" & row.GetCell(6).ToString() == "" & row.GetCell(7).ToString() == "")
                                    {
                                    }
                                    else
                                    {
                                        if (row.GetCell(7).ToString() == "以下空白")
                                        {
                                            cell1.Add(row.GetCell(0));
                                            cell2.Add(row.GetCell(1));
                                            cell3.Add(row.GetCell(2));
                                            cell4.Add(row.GetCell(3));
                                            if (row.GetCell(4).CellType == CellType.Formula)
                                            {
                                                row.GetCell(4).SetCellType(CellType.String);
                                                cell5.Add(row.GetCell(4));
                                            }
                                            else
                                            {
                                                cell5.Add(row.GetCell(4));
                                            }
                                            if (row.GetCell(5).CellType == CellType.Formula)
                                            {
                                                row.GetCell(5).SetCellType(CellType.String);
                                                cell6.Add(row.GetCell(5));
                                            }
                                            else
                                            {
                                                cell6.Add(row.GetCell(5));
                                            }
                                            cell7.Add(row.GetCell(6));
                                            cell8.Add(row.GetCell(7));
                                            break;
                                        }
                                        else
                                        {
                                            cell1.Add(row.GetCell(0));
                                            cell2.Add(row.GetCell(1));
                                            cell3.Add(row.GetCell(2));
                                            cell4.Add(row.GetCell(3));
                                            if (row.GetCell(4).CellType == CellType.Formula)
                                            {
                                                row.GetCell(4).SetCellType(CellType.String);
                                                cell5.Add(row.GetCell(4));
                                            }
                                            else
                                            {
                                                cell5.Add(row.GetCell(4));
                                            }
                                            if (row.GetCell(5).CellType == CellType.Formula)
                                            {
                                                row.GetCell(5).SetCellType(CellType.String);
                                                cell6.Add(row.GetCell(5));
                                            }
                                            else
                                            {
                                                cell6.Add(row.GetCell(5));
                                            }
                                            cell7.Add(row.GetCell(6));
                                            cell8.Add(row.GetCell(7));
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { Log.Error(ex, $"資料匯入失敗  檔案名稱{FieldName}"); }
            }
            #endregion
            #region GridView顯示匯入excel資料
            FiletextEdit.Text = ReportPath;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(Convert.ToString(cell1[0]));
            dataTable.Columns.Add(Convert.ToString(cell2[0]));
            dataTable.Columns.Add(Convert.ToString(cell3[0]));
            dataTable.Columns.Add(Convert.ToString(cell4[0]));
            dataTable.Columns.Add(Convert.ToString(cell5[0]));
            dataTable.Columns.Add(Convert.ToString(cell6[0]));
            dataTable.Columns.Add(Convert.ToString(cell7[0]));
            dataTable.Columns.Add(Convert.ToString(cell8[0]));
            for (int i = 1; i < cell1.Count; i++)
            {
                dataTable.Rows.Add(cell1[i], cell2[i], cell3[i], cell4[i], cell5[i], cell6[i], cell7[i], cell8[i]);
            }
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[1].BestFit();
            gridView1.Columns[2].BestFit();
            gridView1.Columns[3].BestFit();
            gridView1.Columns[4].BestFit();
            gridView1.Columns[5].BestFit();
            gridView1.Columns[6].BestFit();
            gridView1.Columns[7].BestFit();
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            #endregion
            if (gridControl1.DataSource != null)
            {
                ImportsimpleButton.Enabled = true;
            }
        }

        private void ImportsimpleButton_Click(object sender, EventArgs e)
        {
            #region 匯入報價單至資料庫
            try
            {
                if (SQLMethod.Search_Costofferform(comboBoxEdit1.Text).Count() == 0)
                {
                    int j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count();
                    if (j == 0)
                    {
                        j = 1;
                        string NO = comboBoxEdit1.Text;
                        for (int i = 1; i < cell1.Count; i++)
                        {
                            string content = $"{j},'{NO}','{cell1[i].ToString()}','{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                            SQLMethod.Insert_costofferforms(content);
                            j += 1;
                        }
                    }
                    else
                    {
                        j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count() + 1;
                        string NO = comboBoxEdit1.Text;
                        for (int i = 1; i < cell1.Count; i++)
                        {
                            string content = $"{j},'{NO}','{cell1[i].ToString()}','{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                            SQLMethod.Insert_costofferforms(content);
                            j += 1;
                        }
                    }
                    MessageBox.Show("報價單匯入成功!!");
                    #region 將資料從請購單匯入至請購計畫資料庫
                    costofferforms = SQLMethod.Count_Costofferform();
                    purchaseplans = SQLMethod.Count_purchaseplan();
                    groupcostofferform = SQLMethod.Group_costofferform();
                    pk_number = purchaseplans.Select(g => g.pk).Count();
                    if (pk_number != 0)
                    {
                        pk_number = purchaseplans.Select(g => g.pk).Count() + 1;
                    }
                    else
                    {
                        pk_number = 1;
                    }
                    for (int i = 0; i < groupcostofferform.Count; i++)
                    {
                        for (int k = 0; k < costofferforms.Count - 1; k++)
                        {
                            var first = costofferforms[k].ProjectCode;
                            if (first == groupcostofferform[i].ProjectCode)
                            {
                                if (first.Length == 5)
                                {
                                    string content = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(0, 1)}',{ProjectItem_number},'{first}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                    SQLMethod.Insert_purchaseplan(content);
                                    pk_number += 1;
                                    ProjectItem_number += 1;
                                    break;
                                }
                                else
                                {
                                    string content = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(0, 1)}',{ProjectItem_number},'{first.Substring(0, 5)}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                    SQLMethod.Insert_purchaseplan(content);
                                    pk_number += 1;
                                    ProjectItem_number += 1;
                                    string content1 = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(6, 1)}',{ProjectItem_number},'{first.Substring(6, 5)}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                    SQLMethod.Insert_purchaseplan(content1);
                                    pk_number += 1;
                                    ProjectItem_number += 1;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    DialogResult Result = MessageBox.Show("此專案已匯入過報價單，是否覆蓋原本的報價單??", "表單訊息", MessageBoxButtons.YesNo);
                    if (Result == System.Windows.Forms.DialogResult.Yes)
                    {
                        SQLMethod.Delete_Costofferform(comboBoxEdit1.Text);
                        SQLMethod.Delete_PurchasePlan(comboBoxEdit1.Text);
                        int j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count();
                        if (j == 0)
                        {
                            j = 1;
                            string NO = comboBoxEdit1.Text;
                            for (int i = 1; i < cell1.Count; i++)
                            {
                                string content = $"{j},'{NO}','{cell1[i].ToString()}','{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                                SQLMethod.Insert_costofferforms(content);
                                j += 1;
                            }
                        }
                        else
                        {
                            j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count() + 1;
                            string NO = comboBoxEdit1.Text;
                            for (int i = 1; i < cell1.Count; i++)
                            {
                                string content = $"{j},'{NO}','{cell1[i].ToString()}','{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                                SQLMethod.Insert_costofferforms(content);
                                j += 1;
                            }
                        }
                        MessageBox.Show("報價單重新匯入成功!!");
                        #region 將資料從請購單匯入至請購計畫資料庫
                        costofferforms = SQLMethod.Count_Costofferform();
                        purchaseplans = SQLMethod.Count_purchaseplan();
                        groupcostofferform = SQLMethod.Group_costofferform();
                        pk_number = purchaseplans.Select(g => g.pk).Count();
                        if (pk_number != 0)
                        {
                            pk_number = purchaseplans.Select(g => g.pk).Count() + 1;
                        }
                        else
                        {
                            pk_number = 1;
                        }
                        for (int i = 0; i < groupcostofferform.Count; i++)
                        {
                            for (int k = 0; k < costofferforms.Count - 1; k++)
                            {
                                var first = costofferforms[k].ProjectCode;
                                if (first == groupcostofferform[i].ProjectCode)
                                {
                                    if (first.Length == 5)
                                    {
                                        string content = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(0, 1)}',{ProjectItem_number},'{first}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                        SQLMethod.Insert_purchaseplan(content);
                                        pk_number += 1;
                                        ProjectItem_number += 1;
                                        break;
                                    }
                                    else
                                    {
                                        string content = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(0, 1)}',{ProjectItem_number},'{first.Substring(0, 5)}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                        SQLMethod.Insert_purchaseplan(content);
                                        pk_number += 1;
                                        ProjectItem_number += 1;
                                        string content1 = $"{pk_number},'{comboBoxEdit1.Text}','{first.Substring(6, 1)}',{ProjectItem_number},'{first.Substring(6, 5)}','{null}','{costofferforms[k].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','{null}', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                                        SQLMethod.Insert_purchaseplan(content1);
                                        pk_number += 1;
                                        ProjectItem_number += 1;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "報價單匯入失敗!!");
                MessageBox.Show("報價單匯入失敗!!");
            }
            #endregion
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text != null)
            {
                BrowsingsimpleButton.Enabled = true;
            }
        }
    }
}