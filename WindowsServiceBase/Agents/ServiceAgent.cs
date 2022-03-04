using Services.Interfaces;
using Services.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Agents
{
    public class ServiceAgent : LogAgent
    {
        public ServiceAgent(int delay = 1)
        {
            Delay = delay;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }

        public List<IProcessTask> Tasks { get; set; }

        /// <summary>
        /// loop process
        /// </summary>
        public override async Task<bool> Process()
        {
            try
            {
                CreateTaskList();

                if (!await ProcessTasks())
                {
                    LogWarning("await ProcessTasks() error.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, "Process error");
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Create tasks list
        /// </summary>
        private void CreateTaskList()
        {
            Tasks = new List<IProcessTask>();

            var exampleTask = new ExampleTask();
            Tasks.Add(exampleTask);
        }

        /// <summary>
        /// Create tasks list
        /// </summary>
        public void AddTasks(IProcessTask task)
        {
            if (Tasks == null)
                Tasks = new List<IProcessTask>();

            Tasks.Add(task);
        }

        /// <summary>
        /// import data by table type
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ProcessTasks()
        {
            try
            {
                var tasks = new List<Task<bool>>();
                foreach (var task in Tasks)
                {
                    tasks.Add(task.Process());
                }

                await Task.WhenAll(tasks);

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, "ProcessTasks error");
                return false;
            }
        }

    }
}
