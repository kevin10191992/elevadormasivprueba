using Elevador.Interface;
using System.Threading.Tasks;

namespace Elevador.Models
{
    public class ElevatorStateService : IElevatorState
    {

        private readonly IRepository _dbrepository;
        private readonly IConfiguration _configuration;

        public ElevatorStateService(IRepository dbrepository, IConfiguration configuration)
        {
            _dbrepository = dbrepository;
            _configuration = configuration;
        }


        public async Task AddElevatorWork(ElevatorWork request)
        {
            await _dbrepository.AddElevatorWork(request);
        }

        public async Task UpdateElevatorWork(ElevatorWork request)
        {
            await _dbrepository.UpdateElevatorWork(request);
        }

        public async Task<List<ElevatorWork>> GetPendingElevatorWork()
        {
            return await _dbrepository.GetPendingElevatorWork();
        }

        public async Task<int> GetCurrentFloor()
        {
            int CurrentFloor = await _dbrepository.GetElevatorCurrentFloor();
            if (CurrentFloor == -1)
            {
                int InitialFloor = _configuration.GetValue<int>("InitialFloor");
                await _dbrepository.AddElevatorCurrentFloor(InitialFloor);
                return InitialFloor;
            }

            return CurrentFloor;
        }

        public async Task UpdateCurrentFloor(int Current)
        {
            await _dbrepository.AddElevatorCurrentFloor(Current);
        }
    }
}
