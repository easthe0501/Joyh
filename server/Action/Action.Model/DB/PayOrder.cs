using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Action.Core;

namespace Action.Model
{
    public class PayOrder
    {
        public string BillNo { get; set; }
        public string OpenId { get; set; }
        public string AppId { get; set; }
        public int TS { get; set; }
        public PayItem PayItem { get; set; }
        public string Token { get; set; }
        public string Version { get; set; }
        public int ZoneId { get; set; }
        public int ProvideType { get; set; }
        public int Amt { get; set; }
        public int Payamt_coins { get; set; }
        public int Pubacct_payamt_coins { get; set; }
        public bool Finished { get; set; }

        private DateTime _createTime = Global.DefaultDateTime;
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value.ToLocalTime(); }
        }

        public PayOrder(HttpListenerRequest request)
        {
            BillNo = request.QueryString["billno"];
            OpenId = request.QueryString["openid"];
            AppId = request.QueryString["appid"];
            TS = MyConvert.ToInt32(request.QueryString["ts"]);
            PayItem = new PayItem(request.QueryString["payitem"]);
            Token = request.QueryString["token"];
            Version = request.QueryString["version"];
            ZoneId = MyConvert.ToInt32(request.QueryString["zoneid"]);
            ProvideType = MyConvert.ToInt32(request.QueryString["providetype"]);
            Amt = MyConvert.ToInt32(request.QueryString["amt"]);
            Payamt_coins = MyConvert.ToInt32(request.QueryString["payamt_coins"]);
            Pubacct_payamt_coins = MyConvert.ToInt32(request.QueryString["pubacct_payamt_coins"]);
            Finished = false;
            CreateTime = DateTime.Now;
        }
    }

    public class PayItem
    {
        public int ItemId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public PayItem(string text)
        {
            var array = text.Split('*');
            if (array.Length > 0)
                ItemId = MyConvert.ToInt32(array[0]);
            if (array.Length > 1)
                Price = MyConvert.ToInt32(array[1]);
            if (array.Length > 2)
                Count = MyConvert.ToInt32(array[2]);
        }

        public override string ToString()
        {
            return string.Format("{0}*{1}*{2}", ItemId, Price, Count);
        }
    }
}
