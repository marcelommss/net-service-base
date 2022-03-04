using log4net;
using System;
using System.Threading.Tasks;

namespace Services.Tasks
{
    public abstract class LogTask : ServiceBaseTask
    {
        public ILog Logger { get; set; }

        public override async Task<bool> Process()
        {
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return await base.Process();
        }

        public override string Process(object value)
        {
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return base.Process(value);
        }

        public void LogInformation(string message)
        {
            try
            {
#if DEBUG
                if (Environment.UserInteractive)
                    Console.WriteLine($"{message}");
                else
                    Logger.Info(message);
#else
                Logger.Info(message);
#endif
            }
            catch (Exception)
            {
            }
        }

        public void LogWarning(string message)
        {
            try
            {
                if (Environment.UserInteractive)
                    Console.WriteLine($"{message}");
                else
                    Logger.Warn(message);
            }
            catch (Exception)
            {
            }
        }

        public void LogError(Exception ex, string message = "")
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine($"{message}./r/t{ex.Message}");
                }
                else
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        Logger.Error(message, ex);
                    }
                    else
                    {
                        Logger.Error(ex);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
