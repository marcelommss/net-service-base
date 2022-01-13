using Keyrus.Services.Interfaces;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Keyrus.Services.Tasks
{
    public abstract class ServiceBaseTask : IProcessTask, IDisposable
    {
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }

        public ServiceBaseTask()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }

        public virtual async Task<bool> Process()
        {
            StartedOn = DateTime.Now;
            if (OnError)
                return false;
            return true;
        }

        public virtual string Process(object value)
        {
            StartedOn = DateTime.Now;
            if (OnError)
                return "Falha na criação da task";
            return string.Empty;
        }

        public string Name { get; set; }

        public bool IsTest { get; set; } = false;

        public bool KeepRunning { get; set; } = true;

        public bool OnError { get; set; } = false;

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }


    }
}
