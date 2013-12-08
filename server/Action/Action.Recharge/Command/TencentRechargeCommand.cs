using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using Tencent.Open;
using Action.Engine;
using Action.Model;
using Action.Utility;
using Action.Core;

namespace Action.Recharge.Command
{
    [GameCommand((int)CommandEnum.TencentRecharge)]
    public class TencentRechargeCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var setting = APF.Settings.TencentApi;
            var player = session.Player.Data.AsDbPlayer();
            var tencentArgs = session.Context.TencentParams.ToArgs();

            var privateParams = string.Format("pfkey={0}&ts={1}&payitem={2}&goodsmeta={3}&goodsurl={4}&zoneid={5}",
                tencentArgs.PfKey, DateTime.Now.ToUnixTicks(), setting.Pay_buy_goods.PayItem,
                setting.Pay_buy_goods.GoodsMeta, setting.Pay_buy_goods.GoodsUrl, Global.Config.ZoneId);

            var url = new OpenApiBuilder().SetPublicParams(setting.Pay_buy_goods.Api, tencentArgs.OpenId, tencentArgs.OpenKey,
                setting.AppId, setting.AppKey, tencentArgs.Pf).SetPrivateParams(privateParams).ToRequestUrl();

            var service = new OpenApiService(setting.IP, true);
            service.ResponseCompleted += (sender, result) =>
            {
                Queue.Add(() =>
                    {
                        try
                        {
                            session.Logger.LogDebug(session, "[v3/pay/buy_goods]获得平台响应");
                            var res = JsonHelper.FromJson<TencentRechargeArgs>(result);
                            if (res.ret == 0)
                                session.SendResponse(ID, res);
                            else
                                session.Logger.LogError(result);
                        }
                        catch (Exception ex)
                        {
                            session.Logger.LogError(session, string.Format("{0}/{1}", session.Player, ToString()), ex);
                        }
                    });
            };
            service.ResponseFailed += (sender, ex) =>
            {
                session.Logger.LogError(ex);
            };
            service.CallAsync(url);
            session.Logger.LogDebug(session, "[v3/pay/buy_goods]请求平台数据\r\n" + url);
        }
    }
}
