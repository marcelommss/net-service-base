using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProcessTask
    {
        Task<bool> Process();
    }
}
