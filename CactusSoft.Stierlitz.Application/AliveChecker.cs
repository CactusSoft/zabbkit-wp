using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CactusSoft.Stierlitz.Application
{
    public static class AliveChecker
    {
        public struct WeakRefWithType
        {
            public WeakRefWithType(object reference)
            {
                Reference = new WeakReference(reference);
                Type = reference.GetType();
            }

            public WeakReference Reference;
            public Type Type;
        }

        private static readonly List<WeakRefWithType> _references = new List<WeakRefWithType>();
        private static Timer _timer;

        static AliveChecker()
        {
            _timer = new Timer(OnTimer, null, 1000,1000);
        }

        static void OnTimer(object state)
        {
            GC.Collect();            

            lock (_references)
            {
                var removed = _references.Where(r => !r.Reference.IsAlive).ToList();

                foreach (var reference in removed)
                {
                    Log("DIED " + reference.Type.Name);
                    _references.Remove(reference);
                }
            }
        }

        public static void Monitor(object instance)
        {
            lock(_references)
            {                
                _references.Add(new WeakRefWithType(instance));
                
                Log("ADDED " + instance.GetType().Name);
            }
        }

        static void Log(string s)
        {
            Debug.WriteLine("~~~ AC ~~~ [{0}] - {1}", DateTime.Now.ToLongTimeString(), s);
        }
    }
}
