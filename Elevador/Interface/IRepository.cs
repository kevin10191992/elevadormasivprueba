using Elevador.Models;

namespace Elevador.Interface
{
    public interface IRepository
    {
        Task<bool> AddElevatorWork(ElevatorWork request);
        Task<bool> UpdateElevatorWork(ElevatorWork request);
        Task<List<ElevatorWork>> GetPendingElevatorWork();

        Task<int> GetElevatorCurrentFloor();
        Task<bool> AddElevatorCurrentFloor(int CurrentFloor);

    }
}
