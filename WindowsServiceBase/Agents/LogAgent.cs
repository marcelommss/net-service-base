using Keyrus.Services.Agents;
using log4net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Syngenta.Services.Agents
{
    public class LogAgent : ConfigurationAgentBase
    {
        public ILog Logger { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public override async Task<bool> Process()
        { 
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return true;
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
