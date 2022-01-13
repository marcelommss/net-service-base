using System;
using System.Globalization;
using System.Threading;
using RobotFTP.Log;
using Syngenta.Services.Managers;
using Topshelf;

namespace WindowsServiceBase
{
    class Program
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static DateTime? LastLogProcess = null;

        static void Main()
        {
            try
            {
                Configuration();

                HostFactory.Run(x =>
                {
                    string appName = "Your Service Name";

                    x.SetDisplayName(appName);
                    x.SetServiceName("My.Service.Name");

                    x.SetDescription("This service is responsible for doing some awesome backgroud work.");

                    //x.RunAsLocalSystem();

                    x.RunAsNetworkService();
                    x.UseLog4Net();

                    x.Service<ServiceManager>(s =>
                    {
                        s.ConstructUsing(name => new ServiceManager(appName));
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });

                });

            }
            catch (Exception)
            {
                if (Environment.UserInteractive)
                    Console.ReadKey();
            }
        }

        private static void Configuration()
        {
            try
            {
                SetCulture();
                ConfigureLog();
            }
            catch (Exception)
            {
            }
        }

        private static void ConfigureLog()
        {
            var logManager = new LogManagerConfiguration();
            logManager.Configure();
        }

        public static void TryClearConsole()
        {
            try
            {
                if (!Environment.UserInteractive)
                    return;

#if DEBUG
                Console.Clear();
#endif
            }
            catch (Exception)
            {
            }
        }

        private static void SetCulture()
        {
            try
            {
                var culture = new CultureInfo("pt-BR");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                CultureInfo.CurrentCulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception)
            {
            }
        }

    }
}
