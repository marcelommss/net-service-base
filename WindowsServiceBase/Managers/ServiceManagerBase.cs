using Services.Workers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Managers
{
    public abstract class ServiceManagerBase
    {
        public string ServiceName { get; set; }

        public string ConnectionString { get; set; }

        public List<WorkerThread> _workerThreads;

        public ServiceManagerBase(string serviceName)
        {
            try
            {
                ServiceName = serviceName;
                //ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            catch (System.Exception)
            {
            }
        }

        public async void Start()
        {
            ReleaseWorkerThreads();

            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };

            _workerThreads = new List<WorkerThread>();

            await CreateWorkers();

            StartWorkerThreads();
        }

        public virtual async Task<bool> CreateWorkers() 
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            return true;
        }

        private void StartWorkerThreads()
        {
            foreach (var workerThread in _workerThreads)
            {
                Thread currentThread = new Thread(workerThread.Agent.Main)
                {
                    //IsBackground = true,
                    Name = workerThread.Name
                };

                workerThread.Agent.KeepRunning = true;
                workerThread.Thread = currentThread;

                workerThread.Thread.Start();
            }
        }

        public void Continue()
        {
            foreach (var workerThread in _workerThreads)
            {
                if (workerThread?.Thread == null)
                    continue;

                if (workerThread.Thread.Name == null)
                {
                    workerThread.Thread.Interrupt();
                    continue;
                }

                if (workerThread.Agent == null)
                {
                    workerThread.Thread.Interrupt();
                    continue;
                }

                workerThread.Agent.Continue();

            }
        }

        public void Pause()
        {
            foreach (var workerThread in _workerThreads)
            {
                if (workerThread?.Thread == null)
                    continue;

                if (workerThread.Thread.Name == null)
                {
                    workerThread.Thread.Interrupt();
                    continue;
                }

                if (workerThread.Agent == null)
                {
                    workerThread.Thread.Interrupt();
                    continue;
                }

                workerThread.Agent.Pause();
            }
        }

        public void Stop()
        {
            foreach (var workerThread in _workerThreads)
            {
                if (workerThread.Thread == null)
                    continue;

                if (workerThread.Thread.Name == null)
                {
                    workerThread.Thread.Interrupt();
                    continue;
                }

                ThreadState currentThreadState = workerThread.Thread.ThreadState;

                if (currentThreadState == ThreadState.Running ||
                    currentThreadState == ThreadState.Background ||
                    currentThreadState == ThreadState.WaitSleepJoin ||
                    currentThreadState == (ThreadState.Background | ThreadState.WaitSleepJoin))
                {
                    workerThread.Agent.Stop();

                    if (!workerThread.Thread.Join(5000))
                        workerThread.Thread.Interrupt();
                }
                else
                {
                    workerThread.Thread.Interrupt();
                }
            }
        }

        private void ReleaseWorkerThreads()
        {
            if (_workerThreads == null)
                return;

            foreach (var current in _workerThreads)
            {
                if (current.Thread == null)
                    continue;

                var currentThreadState = current.Thread.ThreadState;

                if (currentThreadState == ThreadState.Stopped ||
                    currentThreadState == ThreadState.Unstarted)
                    continue;

                current.Thread.Interrupt();
            }

            _workerThreads.Clear();
            _workerThreads = null;
        }
    }

}
