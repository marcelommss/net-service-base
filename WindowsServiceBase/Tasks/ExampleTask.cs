using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Tasks
{
    public class ExampleTask : LogTask
    {
        public ExampleTask()
        {
            Name = "MoveFromCaptureTask";
        }

        public List<FileInfo> Items { get; set; }

        public override async Task<bool> Process()
        {
            try
            {

                if (!await base.Process())
                    return false;

                Items = new List<FileInfo>();

                TryGetList();

                if (Items == null || Items.Count == 0)
                    return true;

                Items = Items.OrderBy(x => x.Name).ToList();

                LogInformation($"Found {Items.Count} items on C: at {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");

                foreach (var item in Items)
                    await DisplayItem(item);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (Items != null) Items.Clear();
            }
            return true;
        }

        public async Task<bool> DisplayItem(FileInfo fileInfo)
        {
            if (fileInfo == null)
                return false;

            try
            {
                LogInformation(fileInfo.Name);
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Error while gettting filename");
                return false;
            }
            finally
            {
            }
        }

        public bool TryGetList()
        {
            int tries = 0;
            while (tries < 5)
            {
                if (GetList())
                    return true;
                if (tries > 4)
                    return false;
                tries++;
                Thread.Sleep(tries * 3500);
            }
            return true;
        }

        private bool GetList()
        {
            try
            {
                if (Items == null)
                    Items = new List<FileInfo>();
                Items.Clear();

                var dir = new DirectoryInfo("c:/");
                Items = dir.GetFiles().ToList();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}