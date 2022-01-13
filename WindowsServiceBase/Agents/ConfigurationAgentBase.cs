using Keyrus.Services.Utils;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Keyrus.Services.Agents
{
    public class ConfigurationAgentBase: AgentBase
    {
        public AppConfig Config { get; set; }

        #region Constructors

        public ConfigurationAgentBase()
        {
        }

        #endregion

        #region Methods


        public override async Task<bool> Process()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            return true;
        }

        public string ReadConfiguration()
        {
            try
            {

                if (!File.Exists($"{Directory.GetParent(AppContext.BaseDirectory).FullName}\\appsettings.json"))
                {
                    return string.Empty;
                }
                else
                {
                    return File.ReadAllText($"{Directory.GetParent(AppContext.BaseDirectory).FullName}\\appsettings.json", Encoding.GetEncoding("iso-8859-1"));
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string FormatPath(string data)
        {
            try
            {
                int startData = data.IndexOf("Path\": ") + 8;
                int endData = data.IndexOf("\r\n}");

                var pathData = data.Substring(startData, data.Length - (data.Length - endData) - startData - 4);

                if (pathData.Contains('\\') && !pathData.Contains(@"\\"))
                {
                    data = data.Replace(pathData, StringMethods.RemoveEscape(pathData));
                }

                return data;
            }
            catch (Exception)
            {
                return data;
            }
        }
        #endregion Methods
    }
}