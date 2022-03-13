using Elevador.Interface;
using Elevador.Models;

namespace Elevador.Service
{
    public class ElevatorService : IElevator
    {
        private readonly ILogger<ElevatorService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IElevatorState _elevatorState;

        public ElevatorService(ILogger<ElevatorService> logger, IConfiguration configuration, IElevatorState elevatorState)
        {
            _logger = logger;
            _configuration = configuration;
            _elevatorState = elevatorState;
        }

        public async Task<ElevatorResponse> CallFromInside(ElevatorWork request)
        {
            request.FromFloor = await _elevatorState.GetCurrentFloor();
            await _elevatorState.AddElevatorWork(request);

            return new ElevatorResponse(request.ToFloor);
        }

        public async Task<ElevatorResponse> CallFromOutside(ElevatorWork request)
        {
            await _elevatorState.AddElevatorWork(request);
            return new ElevatorResponse(request.FromFloor);
        }

        public async Task<ElevatorState> GetElevatorState()
        {
            ElevatorState resp = new ElevatorState
            {
                CurrentFloor = await _elevatorState.GetCurrentFloor(),
                ListPendingRequest = await _elevatorState.GetPendingElevatorWork()
            };

            return resp;
        }



        public async Task<int> MoveElevator(ElevatorWork request)
        {
            int FromFloor = await _elevatorState.GetCurrentFloor();
            int ToFloor;
            TimeSpan timeSpan = TimeSpan.FromSeconds(_configuration.GetValue<int>("SpeedInSeconds"));
            bool UnitTestLog = _configuration.GetValue<bool>("UnitTestLog");
            int Miliseconds = int.Parse(timeSpan.TotalMilliseconds.ToString());
            int CurrentFloor = int.Parse(FromFloor.ToString());

            if (request.CalledFromInside)
            {
                ToFloor = request.ToFloor;
            }
            else
            {
                ToFloor = request.FromFloor;
            }

            if (UnitTestLog) Console.WriteLine($"Elevator start work from floor {FromFloor} to floor {ToFloor}");



            if (FromFloor > ToFloor)
            {
                for (int i = FromFloor; i > ToFloor; i--)
                {
                    await Task.Delay(Miliseconds);
                    CurrentFloor = i - 1;
                    _logger.LogInformation("Elevator is now in floor # {floor} ", CurrentFloor);
                    if (UnitTestLog) Console.WriteLine($"Elevator is now in floor # {CurrentFloor}");
                    if (CurrentFloor == ToFloor) { break; }
                }

            }
            else
            {

                for (int i = FromFloor; i < ToFloor; i++)
                {
                    await Task.Delay(Miliseconds);
                    CurrentFloor = i + 1;
                    _logger.LogInformation("Elevator is now in floor # {floor} ", CurrentFloor);
                    if (UnitTestLog) Console.WriteLine($"Elevator is now in floor # {CurrentFloor}");
                    if (CurrentFloor == ToFloor) { break; }
                }
            }

            request.RequestCompleted = true;
            request.CompletedTime = DateTime.Now;
            await _elevatorState.UpdateCurrentFloor(CurrentFloor);
            await _elevatorState.UpdateElevatorWork(request);

            if (UnitTestLog) Console.WriteLine("RequestCompleted");
            return CurrentFloor;
        }
    }
}
