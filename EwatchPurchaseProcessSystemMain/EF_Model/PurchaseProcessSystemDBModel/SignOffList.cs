using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchaseProcessSystemMain.EF_Model.PurchaseProcessSystemDBModel
{
    public partial class SignOffList
    {
        public int pk { get; set; }
        public string ProjectNO { get; set; }
        public string Code { get; set; }
        public string ProjectCode { get; set; }
        public string PurchaseNumber { get; set; }
        public string ApplicationSector { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime NeedDate { get; set; }
        public string Receiver { get; set; }
        public string TradingLocation { get; set; }
        public string Content { get; set; }
        public string Brand { get; set; }
        public byte Check { get; set; }
    }
}
