using System;
using System.Threading;
using System.Threading.Tasks;

namespace Keyrus.Services.Agents
{
    public abstract class AgentBase
    {

        #region Constructors

        public AgentBase()
        {
        }

        #endregion

        #region Properties

        public int Delay { get; set; } = 5;
        public bool KeepRunning { get; set; } = false;

        #endregion

        #region Methods

        public void Main()
        {
            try
            {
                Initialize();
                StartProcess();
                Run();
            }
            catch (Exception)
            {
            }
        }

        public virtual void Initialize() { }

        public virtual void StartProcess() { }

        public abstract Task<bool> Process();

        public async void Run()
        {
            while (KeepRunning)
            {
                //if (EstabilishConnection())
                await Process();
                Thread.Sleep(Delay * 1000);
            }
        }

        public void Continue()
        {
            KeepRunning = true;
        }

        public void Pause()
        {
            KeepRunning = false;
        }

        public void Stop()
        {
            KeepRunning = false;
        }

        #endregion

    }
}