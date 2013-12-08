using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Engine
{
    public class GameContext
    {
        private TencentParams _tencentParams = new TencentParams();
        public TencentParams TencentParams
        {
            get { return _tencentParams; }
        }
    }

    public class TencentParams
    {
        public string OpenId { get; set; }
        public string OpenKey { get; set; }
        public string Pf { get; set; }
        public string PfKey { get; set; }
    }
}
