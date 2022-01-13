using System.Threading.Tasks;

namespace Keyrus.Services.Interfaces
{
    public interface IProcessTask
    {
        Task<bool> Process();
    }
}
