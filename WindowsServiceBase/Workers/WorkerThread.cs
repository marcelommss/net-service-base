using Services.Agents;
using System.Threading;

namespace Services.Workers
{
    public class WorkerThread
    {
        public string Name { get; set; }
        public AgentBase Agent { get; set; }
        public Thread Thread { get; set; }
    }
}
