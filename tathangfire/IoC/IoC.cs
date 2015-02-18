using Castle.Windsor;
using System;

namespace tathangfire.IoC
{
    public sealed class IoC
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer _container;

        private static IoC _instance = new IoC();

        private IoC()
        {
            _container = new WindsorContainer();
        }

        public static IWindsorContainer Container
        {
            get { return _container; }

            set
            {
                lock (LockObj)
                {
                    _container = value;
                }
            }
        }


        public static IoC Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new IoC();
                        }
                    }
                }

                return _instance;
            }
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}