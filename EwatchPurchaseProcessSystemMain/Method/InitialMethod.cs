using EwatchPurchaseProcessSystemMain.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchaseProcessSystemMain.Method
{
    public class InitialMethod
    {
        /// <summary>
        /// 工作路徑
        /// </summary>
        private static readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        #region SQL設定檔
        public static SQLSetting InitialSQLSetting()
        {
            if (!Directory.Exists($"{WorkPath}\\stf"))
                Directory.CreateDirectory($"{WorkPath}\\stf");
            string setFile = $"{WorkPath}\\stf\\SQLSeverSetting.json";
            SQLSetting settings;
            if (File.Exists(setFile))
            {
                string json = File.ReadAllText(setFile, Encoding.UTF8);
                settings = JsonConvert.DeserializeObject<SQLSetting>(json);
            }
            else
            {
                settings = new SQLSetting()
                {
                    DataSource = "127.0.0.1",
                    InitialCatalog = "PurchaseProcessSystemDB",
                    UserID = "sa",
                    Password = "1234"
                };
                string output = JsonConvert.SerializeObject(settings, Formatting.Indented, new JsonSerializerSettings());
                File.WriteAllText(setFile, output, Encoding.UTF8);
            }
            return settings;
        }
        #endregion
    }
}
