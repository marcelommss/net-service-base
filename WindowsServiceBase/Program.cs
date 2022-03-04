using System;
using System.Globalization;
using System.Threading;
using Log;
using Services.Managers;
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

                Codility();

                Console.ReadKey();


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

        private static void Codility()
        {
            Console.WriteLine(solution(new int[] {4, -6000, 2, 200, 6, 6, 4 }).ToString());
        }
        public static int solution(int[] A)
        {
            int maxValue = 0;
            int result = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] < maxValue)
                    continue;
                result = i;
                maxValue = A[i];
            }
            return result;
        }
        //public static int solution(int[] A)
        //{
        //    int N = A.Length;
        //    int result = -1000000000;
        //    for (int i = 0; i < N; i++)
        //        result = ((A[i] - 1 < result)) ? result : (A[i] - 1);
        //    return result;
        //}
        //public static String solution(String s)
        //{
        //    char c = s[0];
        //    if ___ {  // please fix condition
        //        return "upper";
        //    }
        //    else if ___ {  // please fix condition
        //        return "lower";
        //    }
        //    else if ___ {  // please fix condition
        //        return "digit";
        //    }
        //    else
        //    {
        //        return "other";
        //    }
        //}

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
