using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;

namespace Action.Engine
{
    public interface IGameModule
    {
        int Index { get; }
        void RaiseLoad(GameWorld world);
        void RaiseUnload(GameWorld world);
        void RaisePerFiveMinutes(GameWorld world);
        void RaisePerHalfHour(GameWorld world);
        void RaiseCreateRole(GamePlayer player);
        void RaiseEnterGame(GamePlayer player);
        void RaiseLeaveGame(GamePlayer player);
        void RaiseLevelUp(GamePlayer player, int oldLevel);
    }

    public class GameModule : IGameModule
    {
        public virtual int Index
        {
            get { return 100; }
        }

        public void RaiseLoad(GameWorld world)
        {
            try
            {
                Load(world);
            }
            catch (Exception ex)
            {
                world.AppServer.Logger.LogError(ex);
            }
        }

        public void RaiseUnload(GameWorld world)
        {
            try
            {
                Unload(world);
            }
            catch (Exception ex)
            {
                world.AppServer.Logger.LogError(ex);
            }
        }

        public void RaisePerFiveMinutes(GameWorld world)
        {
            world.AppServer.MainQueue.Add(() =>
                {
                    try
                    {
                        PerFiveMinutes(world);
                    }
                    catch (Exception ex)
                    {
                        world.AppServer.Logger.LogError(ex);
                    }
                });
        }

        public void RaisePerHalfHour(GameWorld world)
        {
            world.AppServer.MainQueue.Add(() =>
                {
                    try
                    {
                        PerHalfHour(world);
                    }
                    catch (Exception ex)
                    {
                        world.AppServer.Logger.LogError(ex);
                    }
                });
        }

        public void RaiseCreateRole(GamePlayer player)
        {
            var server = player.Session.Server;
            server.MainQueue.Add(() =>
                {
                    try
                    {
                        CreateRole(player);
                    }
                    catch (Exception ex)
                    {
                        server.Logger.LogError(ex);
                    }
                });
        }

        public void RaiseEnterGame(GamePlayer player)
        {
            var server = player.Session.Server;
            server.MainQueue.Add(() =>
            {
                try
                {
                    EnterGame(player);
                }
                catch (Exception ex)
                {
                    server.Logger.LogError(ex);
                }
            });
        }

        public void RaiseLeaveGame(GamePlayer player)
        {
            var server = player.Session.Server;
            server.MainQueue.Add(() =>
            {
                try
                {
                    LeaveGame(player);
                }
                catch (Exception ex)
                {
                    server.Logger.LogError(ex);
                }
            });
        }

        public void RaiseLevelUp(GamePlayer player, int oldLevel)
        {
            var server = player.Session.Server;
            server.MainQueue.Add(() =>
            {
                try
                {
                    LevelUp(player, oldLevel);
                }
                catch (Exception ex)
                {
                    server.Logger.LogError(ex);
                }
            });
        }

        public virtual void Load(GameWorld world)
        {            
        }

        public virtual void Unload(GameWorld world)
        {
        }

        public virtual void PerSecond(GameWorld world)
        {
        }

        public virtual void PerFiveMinutes(GameWorld world)
        {
        }

        public virtual void PerHalfHour(GameWorld world)
        {
        }

        public virtual void CreateRole(GamePlayer player)
        {
        }

        public virtual void EnterGame(GamePlayer player)
        {
        }

        public virtual void LeaveGame(GamePlayer player)
        {
        }

        public virtual void LevelUp(GamePlayer player, int oldLevel)
        {
        }
    }

    public class GameModuleFactory
    {
        [ImportMany]
        private IEnumerable<IGameModule> _modules = null;
        public IEnumerable<IGameModule> Modules
        {
            get { return _modules; }
        }

        private Dictionary<Type, IGameModule> _typeHash = new Dictionary<Type, IGameModule>();

        public GameModuleFactory()
        {
            Composition.ComposeParts(this);
            _modules = _modules.OrderBy(m => m.Index);
            foreach (var module in _modules)
            {
                var interfaceType = GetInterfaceType(module.GetType());
                if (interfaceType != null)
                    _typeHash.Add(interfaceType, module);
            }
        }

        private Type GetInterfaceType(Type type)
        {
            return type.GetInterfaces().SingleOrDefault(t => t.Name.EndsWith(type.Name));
        }

        public IGameModule Module(Type type)
        {
            IGameModule module = null;
            if (_typeHash.TryGetValue(type, out module))
                return module;
            return null;
        }

        public T Module<T>() where T : IGameModule
        {
            return (T)Module(typeof(T));
        }
    }
}
