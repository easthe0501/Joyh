using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Net;
using System.IO;
using Action.Core;
using Action.Engine;
using Action.Model;
using Tencent.Open;
using Action.Utility;

namespace Action.Recharge
{
    [Export(typeof(IGameModule))]
    public class RechargeModule : GameModule
    {
        class ResponseArgs
        {
            public int ret { get; set; }
            public string msg { get; set; }
        }

        private HttpServer _rechargeServer;

        public override void Load(GameWorld world)        
        {
            var url = string.Format("http://{0}:9001/recharge/", Global.Config.ServerIp);
            _rechargeServer = new HttpServer("Recharge", url);
            _rechargeServer.DataReceived += (context) => OnRechargeCallback(world, context);
            _rechargeServer.Start();
            _rechargeServer.Logger.LogInfo("充值服务已启动，监听地址：" + url);
        }

        public override void Unload(GameWorld world)
        {
            _rechargeServer.Stop();
            _rechargeServer.Logger.LogInfo("充值服务已停止");
        }

        public override void EnterGame(GamePlayer player)
        {
            FinishPayOrders(player);
        }

        private void OnRechargeCallback(GameWorld world, HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var url = request.Url.PathAndQuery;
            _rechargeServer.Logger.LogDebug(url);

            var sp1 = OpenApiUtility.GenerateSigSource2(url);
            var sp2 = OpenApiUtility.GenerateSigKey(APF.Settings.TencentApi.AppKey);
            var sig1 = request.QueryString["sig"];
            var sig2 = OpenApiUtility.GenerateSigFinal(sp1, sp2);
            DebugSig(sp1, sp2, sig1, sig2);

            var args = new ResponseArgs();
            if (sig1 == sig2)
            {
                var appId1 = request.QueryString["appid"];
                var appId2 = APF.Settings.TencentApi.AppId;
                if (appId1 == appId2)
                {
                    var ts1 = MyConvert.ToInt32(request.QueryString["ts"]);
                    var ts2 = DateTime.Now.ToUnixTicks();
                    if (Math.Abs(ts2 - ts1) < 900)
                    {
                        var payOrder = new PayOrder(request);
                        var dbWorld = world.Data.AsDbWorld();
                        var summary = dbWorld.GetSummaryByAcc(payOrder.OpenId);
                        if (summary != null)
                        {
                            dbWorld.PayOrders[payOrder.BillNo] = payOrder;
                            summary.PayOrders.Add(payOrder.BillNo);
                            var player = world.GetPlayer(summary.Name);
                            if (player != null)
                                FinishPayOrders(player);
                            args.ret = 0;
                            args.msg = "支付成功";
                            TimerHelper.Delay(() => ConfirmDelivery(summary, payOrder), 10000);
                        }
                        else
                        {
                            args.ret = 4;
                            args.msg = "openid不存在";
                        }
                    }
                    else
                    {
                        args.ret = 3;
                        args.msg = "支付超时";
                    }
                }
                else
                {
                    args.ret = 2;
                    args.msg = "无效的appid";
                }
            }
            else
            {
                args.ret = 1;
                args.msg = "签名不正确";
            }
            response.Write(args.ToJson());
        }

        private void ConfirmDelivery(PlayerSummary summary, PayOrder payOrder)
        {
            var logger = ServerContext.GameServer.Logger;
            var setting = APF.Settings.TencentApi;
            var tencentArgs = summary.Context.TencentParams.ToArgs();

            var privateParams = string.Format("ts={0}&payitem={1}&token_id={2}&billno={3}&zoneid={4}&provide_errno={5}&amt={6}&payamt_coins={7}",
                DateTime.Now.ToUnixTicks(), payOrder.PayItem.ToString(), payOrder.Token, payOrder.BillNo, payOrder.ZoneId,
                0, payOrder.Amt, payOrder.Pubacct_payamt_coins);

            var url = new OpenApiBuilder().SetPublicParams(setting.Pay_confirm_delivery.Api, tencentArgs.OpenId, tencentArgs.OpenKey,
                setting.AppId, setting.AppKey, tencentArgs.Pf).SetPrivateParams(privateParams).ToRequestUrl();

            var service = new OpenApiService(setting.IP, true);
            service.ResponseCompleted += (sender, result) =>
            {
                try
                {
                    logger.LogDebug(string.Format("{0}({1})/{2}\r\n[v3/pay/confirm_delivery]获得平台响应\r\n{3}",
                        summary.Name, summary.Account, payOrder.BillNo, result));
                }
                catch (Exception ex)
                {
                    logger.LogError(string.Format("{0}({1})/{2}\r\n{3}",
                        summary.Name, summary.Account, payOrder.BillNo, ex));
                }
            };
            service.ResponseFailed += (sender, ex) =>
            {
                logger.LogError(ex);
            };
            service.CallAsync(url);
            logger.LogDebug(string.Format("{0}({1})/{2}\r\n[v3/pay/confirm_delivery]请求平台数据\r\n",
                        summary.Name, summary.Account, payOrder.BillNo));
        }

        private void DebugSig(string sp1, string sp2, string sig1, string sig2)
        {
            var sb = new StringBuilder()
                .AppendLine("sp1 = {0}", sp1)
                .AppendLine("sp2 = {0}", sp2)
                .AppendLine("sig1 = {0}", sig1)
                .AppendLine("sig2 = {0}", sig2);
            _rechargeServer.Logger.LogDebug(sb.ToString());
        }

        private void FinishPayOrders(GamePlayer player)
        {
            var dbWorld = player.World.Data.AsDbWorld();
            var dbPlayer = player.Data.AsDbPlayer();
            var tcPays = APF.Settings.TencentPays;
            var summary = player.GetSummary();
            foreach (var billId in summary.PayOrders)
            {
                var order = dbWorld.PayOrders.GetValue(billId);
                if (order != null)
                {
                    var gold = tcPays.Find(order.PayItem.ItemId).Gold * order.PayItem.Count;
                    dbPlayer.Recharge(player.Session, gold);
                    order.Finished = true;
                }
            }
            summary.PayOrders.Clear();
        }
    }
}
