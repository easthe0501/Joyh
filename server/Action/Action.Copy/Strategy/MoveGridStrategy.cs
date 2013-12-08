//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Model;
//using Action.Engine;
//using Action.Core;

//namespace Action.Copy.Strategy
//{
//    public class MoveGridStrategy : ICopyGridStrategy
//    {
//        public IEnumerable<CopyEvent> Run(GameSession session, CopyMember member, object data)
//        {
//            //改变位置
//            var copySetting = member.Instance.Setting;
//            member.CurrentPos += MyConvert.ToInt32(data);
//            if (member.CurrentPos > copySetting.Grids.Length - 1)
//                member.CurrentPos = copySetting.Grids.Length - 1;
//            if (member.CurrentPos < 0)
//                member.CurrentPos = 0;
//            yield return new CopyEvent() { Type = CopyEventType.MoveTo, Data = member.CurrentPos };

//            //触发位置事件
//            var grid = copySetting.Grids[member.CurrentPos];
//            var strategy = Strategy.CopyGridStrategyFactory.GetStrategy(grid.Type);
//            if (strategy != null)
//            {
//                var events = strategy.Run(session, member, grid.Data);
//                foreach (var evt in events)
//                    yield return evt;
//            }
//        }
//    }
//}
