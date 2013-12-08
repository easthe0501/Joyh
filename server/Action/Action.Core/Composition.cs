using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System;
using System.IO;
using System.Reflection;

namespace Action.Core
{
    public static class Composition
    {
        private static CompositionContainer _container;

        static Composition()
        {
            //Create the CompositionContainer with the parts in the catalog
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            var dirCatalog = new DirectoryCatalog(path, "Action.*.dll");
            _container = new CompositionContainer(dirCatalog);
            //if (AppDomain.CurrentDomain.IsFullyTrusted)
            //{
            //    var dirCatalog = new DirectoryCatalog(path, "Action.*.dll");
            //    _container = new CompositionContainer(dirCatalog);
            //}
            //else
            //{
            //    AggregateCatalog catalog = new AggregateCatalog();
            //    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            //    {
            //        if (assembly.FullName.Substring(0, 6) == "Action")
            //        {
            //            foreach (var t in assembly.GetExportedTypes())
            //            {
            //                catalog.Catalogs.Add(new TypeCatalog(t));
            //            }
            //            //catalog.Catalogs.Add(new AssemblyCatalog(assembly));

            //        }
            //    }
            //    _container = new CompositionContainer(catalog);
            //}
        }

        private static object _syncRoot = new object();

        public static bool ComposeParts(params object[] attributedParts)
        {
            lock (_syncRoot)
            {
                try
                {
                    _container.ComposeParts(attributedParts);
                    return true;
                }
                catch (CompositionException)
                {
                    throw;
                }
            }
        }
    }
}
