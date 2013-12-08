using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using SuperSocket.SocketBase;
using Tencent.Open;
using Action.Utility;

namespace Action.Login.Command
{
    [GameCommand((int)CommandEnum.TencentLogin)]
    public class TencentLoginCommand : GameCommand<TencentApiArgs>
    {
        class ResponseArgs
        {
            public int ret { get; set; }
            public int is_lost { get; set; }
            public string nickname { get; set; }
            public string gender { get; set; }
            public string country{ get; set; }
            public string province{ get; set; }
            public string city { get; set; }
            public string figureurl { get; set; }
            public int is_yellow_vip { get; set; }
            public int is_yellow_year_vip { get; set; }
            public int yellow_vip_level { get; set; }
            public int is_yellow_high_vip { get; set; }
        }

        protected override bool Ready(GameSession session, TencentApiArgs args)
        {
            return session.Player.Status == LoginStatus.CreateSocket;
        }

        protected override void Run(GameSession session, TencentApiArgs args)
        {
            var setting = APF.Settings.TencentApi;
            var url = new OpenApiBuilder().SetPublicParams(setting.User_get_info.Api, args.OpenId, args.OpenKey,
                setting.AppId, setting.AppKey, args.Pf).ToRequestUrl();

            var service = new OpenApiService(setting.IP);
            service.ResponseCompleted += (sender, result) =>
            {
                Queue.Add(() =>
                    {
                        try
                        {
                            session.Logger.LogDebug(session, "[v3/user/get_info]获得平台响应");
                            var res = JsonHelper.FromJson<ResponseArgs>(result);
                            if (res.ret == 0)
                            {
                                var acc = args.OpenId;
                                session.Context.TencentParams.FromArgs(args);
                                session.Login(acc);
                            }
                            else
                            {
                                session.SendError(ErrorCode.ErrorAccount);
                                session.Logger.LogError(result);
                            }
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
            session.Logger.LogDebug(session, "[v3/user/get_info]请求平台数据\r\n" + url);
        }
    }
}
