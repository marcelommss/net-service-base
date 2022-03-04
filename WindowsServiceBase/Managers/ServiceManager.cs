using Services.Managers;
using Services.Workers;
using Services.Agents;
using System;
using System.Threading.Tasks;

namespace Services.Managers
{

    public class ServiceManager : ServiceManagerBase
    {
        private int _instances = 1;
        private int _delay = 60;

        public ServiceManager(string serviceName, int delay = 10, int instances = 1) : base(serviceName)
        {
            try
            {
                _instances = instances;
                _delay = delay;
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// Create service workers
        /// </summary>
        public override async Task<bool> CreateWorkers()
        {
            try
            {

#if DEBUG
                if (Environment.UserInteractive)
                    Console.WriteLine($"Creating service workers...");
#endif

                for (int i = 0; i < _instances; i++)
                {
                    var serviceAgent = new ServiceAgent(_delay);
                    CreateServices(ref serviceAgent);
                    _workerThreads.Add(new WorkerThread()
                    { Name = $"ServiceAgent {i}", Agent = serviceAgent });
                }

                return true;

            }
            catch (Exception)
            {
                //Log.Error($"ServiceManager - Details:{ex?.Message}");
                return false;
            }
        }

        private void CreateServices(ref ServiceAgent serviceAgent)
        {
            //serviceAgent.AddTasks(task);
        }
    }
}
