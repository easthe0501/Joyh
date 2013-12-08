using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class TencentApiSetting
    {
        public TencentApiSetting()
        {
            User_get_info = new user_get_info();
            Pay_buy_goods = new pay_buy_goods();
        }

        public string IP { get; set; }
        public string Encoder { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }

        public class user_get_info
        {
            public string Api { get; set; }
        }
        public user_get_info User_get_info { get; set; }

        public class pay_buy_goods
        {
            public string Api { get; set; }
            public string PayItem { get; set; }
            public string GoodsMeta { get; set; }
            public string GoodsUrl { get; set; }
        }
        public pay_buy_goods Pay_buy_goods { get; set; }

        public class pay_confirm_delivery
        {
            public string Api { get; set; }
        }
        public pay_confirm_delivery Pay_confirm_delivery { get; set; }
    }
}
