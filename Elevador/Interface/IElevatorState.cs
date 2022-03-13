using Elevador.Models;

namespace Elevador.Interface
{
    public interface IElevatorState
    {
        Task AddElevatorWork(ElevatorWork request);
        Task UpdateElevatorWork(ElevatorWork request);
        Task<List<ElevatorWork>> GetPendingElevatorWork();
        Task<int> GetCurrentFloor();
        Task UpdateCurrentFloor(int Current);

    }
}
