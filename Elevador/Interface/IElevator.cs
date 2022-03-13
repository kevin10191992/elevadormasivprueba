using Elevador.Models;

namespace Elevador.Interface
{
    public interface IElevator
    {
        Task<int> MoveElevator(ElevatorWork request);
        Task<ElevatorState> GetElevatorState();
        Task<ElevatorResponse> CallFromInside(ElevatorWork request);
        Task<ElevatorResponse> CallFromOutside(ElevatorWork request);
    }
}
