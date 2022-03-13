using Coravel.Invocable;
using Elevador.Interface;
using Elevador.Models;

namespace Elevador.Jobs
{
    public class ElevatorJob : IInvocable
    {
        private readonly IElevator _elevator;
        private readonly ILogger<ElevatorJob> _logger;

        public ElevatorJob(IElevator elevator, ILogger<ElevatorJob> logger)
        {
            _elevator = elevator;
            _logger = logger;
        }

        public async Task Invoke()
        {
            ElevatorState ActualState = await _elevator.GetElevatorState();
            if (ActualState.ListPendingRequest != null)
            {
                if (ActualState.ListPendingRequest.Count > 0)
                {
                    _logger.LogInformation("Iniciate Elevator Work at :{time}", DateTime.Now);

                    foreach (var req in ActualState.ListPendingRequest)
                    {
                        ActualState.CurrentFloor = await _elevator.MoveElevator(req);
                    }

                    _logger.LogInformation("Elevator movement has finished. Total request completed {cant}", ActualState.ListPendingRequest.Count);
                }
            }
        }
    }
}
