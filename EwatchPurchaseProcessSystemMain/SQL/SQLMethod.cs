using Dapper;
using Serilog;
using EwatchPurchaseProcessSystemMain.Configuration;
using EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace EwatchPurchaseProcessSystemMain.Method
{
    public class SQLMethod
    {
        /// <summary>
        /// 資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder scsb;
        /// <summary>
        /// 資料庫JSON
        /// </summary>
        public SQLSetting setting { get; set; }
        private SqlCommand sqlCommand = null;
        #region 資料庫連結
        /// <summary>
        /// EF資料庫連結
        /// </summary>
        /// <param name="DataBaseType">資料庫類型</param>
        public void SQLConnect()
        {
            scsb = new SqlConnectionStringBuilder()
            {
                DataSource = setting.DataSource,
                InitialCatalog = setting.InitialCatalog,
                UserID = setting.UserID,
                Password = setting.Password
            };
        }
        #endregion

        #region DispatchDataTable資料表
        #region 建立派工資料表
        public List<DispatchDataTable> Insert_dispatchdatatable(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string sql = $"USE [PurchaseProcessSystemDB] INSERT INTO [DispatchDataTable] (projectdatettime, projectno, ownername, owneraddress, ownerno, workstartdate, workenddate, contact, contactphone, projectleader, " +
                        $"projectworkleader, bargainperson, bargainyesornot, bargainrenson, bargaindate, bargainlocal, bargainprice, bargainmoney, quotationcode, quotationprice, " +
                        $"insuranceyesornot, insurancestartdate, insuranceenddate, contractmoney, contractmoneyyesornot, plancost, contractcost, plangoal, ponumber)VALUES({content})";
                    var values = conn.Query<DispatchDataTable>(sql).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "建立派工資料表失敗!");
                MessageBox.Show("建立派工資料表失敗!");
                return null;
            }
        }
        #endregion

        #region 查詢專案編號
        public List<DispatchDataTable> Search_dispatchdatatable()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT projectno FROM [PurchaseProcessSystemDB].[dbo].[DispatchDataTable]";
                    var values = conn.Query<DispatchDataTable>(sql).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢專案編號失敗!");
                return null;
            }
        }
        #endregion

        #region 抓取DispatchDataTable所需內容
        public List<DispatchDataTable> Count_dispatchdatatable()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[DispatchDataTable]";
                    var values = conn.Query<DispatchDataTable>(sql).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取DispatchDataTable所需內容失敗!");
                return null;
            }
        }
        #endregion
        #endregion

        #region Costofferform資料表
        #region 成本報價單資料抓取
        public List<Costofferform> Count_Costofferform()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectCode != ''";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "成本報價單資料抓取失敗");
                return null;
            }
        }
        #endregion

        #region 報價單匯入資料庫
        public List<Costofferform> Insert_costofferforms(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] INSERT INTO [Costofferform] (pk, ProjectNO, ProjectItem, ProjectName, ProjectUnit, ProjectAmount, Price, Money, Remark, ProjectCode";
                    grammar += $" ) VALUES ({content})";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "報價單匯入資料庫失敗");
                return null;
            }
        }
        #endregion

        #region 整理請購編號
        public List<Costofferform> Group_costofferform()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT ProjectCode,SUM(Money) as Money FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectCode != '' and ProjectCode != '以下空白' Group By ProjectCode";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "整理請購編號失敗");
                return null;
            }
        }
        #endregion

        #region 檢查是否匯入過報價單
        public List<Costofferform> Search_Costofferform(string prono)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"SELECT ProjectNO FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectNO = '{prono}'";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "檢查是否匯入過報價單失敗");
                return null;
            }
        }
        #endregion

        #region 刪除匯入資料表
        public List<Costofferform> Delete_Costofferform(string prono)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"Delete FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectNO = '{prono}'";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "刪除匯入資料表失敗");
                return null;
            }
        }
        #endregion
        #endregion

        #region PurchasePlan資料表
        #region excel資料匯入資料庫
        public List<PurchasePlan> Insert_purchaseplan(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] INSERT INTO [PurchasePlan] (pk, ProjectNO, Code, ProjectItem, ProjectCode, ProjectPurchaser, ProjectContent, EstimatedMailDate, RealMailDate, BuyMan, EstimatedDecisionDate, RaelDecisionDate, LongTimeDevice, ContactCode, ErrorCode, Vendor, OrderCode, ContactPerson, ContactPersonPhone, EstimatedCost, ExecutionGoal, PackageCoin, NuPackageCoin, Subtotal, DiffCoin, AccCoin, Remark";
                    grammar += $" ) VALUES ({content})";
                    var values = conn.Query<PurchasePlan>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "資料匯入PurchasePlan資料表失敗");
                MessageBox.Show("報價單匯入失敗!!");
                return null;
            }
        }
        #endregion

        #region 請購計畫表資料抓取
        public List<PurchasePlan> Count_purchaseplan()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[PurchasePlan]";
                    var values = conn.Query<PurchasePlan>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取請購計畫資料失敗");
                return null;
            }
        }
        #endregion

        #region 刪除請購計畫資料表
        public List<PurchasePlan> Delete_PurchasePlan(string prono)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"Delete FROM [PurchaseProcessSystemDB].[dbo].[PurchasePlan] Where ProjectNO = '{prono}'";
                    var values = conn.Query<PurchasePlan>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "刪除請購計畫資料表失敗");
                return null;
            }
        }
        #endregion
        #endregion

        #region SignOffList資料表
        #region 簽核單資料匯入
        public List<SignOffList> Insert_signofflist(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] INSERT INTO [SignOffList] (pk, ProjectNO, Code, ProjectCode, PurchaseNumber, ApplicationSector, ApplicationDate, PurchaseDate, NeedDate, Receiver, TradingLocation, Content, Brand, [Check]";
                    grammar += $" ) VALUES ({content})";
                    var values = conn.Query<SignOffList>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "資料匯入SignOffList資料表失敗");
                return null;
            }
        }
        #endregion

        #region 簽核單pk值抓取
        public List<SignOffList> Catch_signofflist()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT pk FROM [PurchaseProcessSystemDB].[dbo].[SignOffList]";
                    var values = conn.Query<SignOffList>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取SignOffList資料表pk值失敗");
                return null;
            }
        }
        #endregion

        #region 抓簽核單內容
        public List<SignOffList> Count_signofflist(string procode,string prono)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"SELECT * FROM [PurchaseProcessSystemDB].[dbo].[SignOffList] Where ProjectCode = '{procode}' and ProjectNO = '{prono}'";
                    var values = conn.Query<SignOffList>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取簽核單內容失敗");
                return null;
            }
        }
        #endregion
        #endregion

        #region 輸出報表
        public DataTable OutPutTable(string grammar)
        {
            var logconsole = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    DataTable dataTable = new DataTable();
                    DataSet dataSet = new DataSet();
                    sqlCommand = new SqlCommand(grammar, conn);
                    SqlDataAdapter sqlData = new SqlDataAdapter(sqlCommand);
                    dataSet.Clear();
                    sqlData.Fill(dataSet);
                    dataTable = dataSet.Tables[0];
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                logconsole.Information(ex.ToString());
                Log.Logger.Error(ex, "資料匯入Table錯誤");
                return null;
            }
        }
        #endregion
    }
}
