using Elevador.Interface;
using Elevador.Models;
using Microsoft.EntityFrameworkCore;

namespace Elevador.Context
{
    public class Repository : IRepository
    {

        private readonly ElevatorContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(ElevatorContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddElevatorWork(ElevatorWork request)
        {
            bool resp = false;
            await _context.AddAsync(request);

            if (await _context.SaveChangesAsync() > 0)
            {
                resp = true;
                _logger.LogInformation("Request Added");
            }

            return resp;
        }

        public async Task<int> GetElevatorCurrentFloor()
        {
            ElevatorFloor? Actual = await _context.ElevatorFloor.OrderByDescending(a => a.ElevatorFloorId).FirstOrDefaultAsync();
            if (Actual == null)
            {
                return -1;
            }

            return Actual.CurrentElevatorFloor;

        }

        public async Task<List<ElevatorWork>> GetPendingElevatorWork()
        {
            List<ElevatorWork> list = await _context.ElevatorWork.
                Where(a => !a.RequestCompleted).
                OrderByDescending(x => x.CalledFromInside).
                ThenBy(z => z.RequestTime)
                .ToListAsync();

            return list;
        }

        public async Task<bool> AddElevatorCurrentFloor(int CurrentFloor)
        {

            bool resp = false;

            ElevatorFloor Actual = new ElevatorFloor
            {
                CurrentElevatorFloor = CurrentFloor
            };

            await _context.ElevatorFloor.AddAsync(Actual);

            if (await _context.SaveChangesAsync() > 0)
            {
                resp = true;
                _logger.LogInformation("Elevator Current Floor Added");
            }
            return resp;

        }

        public async Task<bool> UpdateElevatorWork(ElevatorWork request)
        {
            bool resp = false;
            _context.Update(request);
            if (await _context.SaveChangesAsync() > 0)
            {
                resp = true;
                _logger.LogInformation("Request Updated");
            }

            return resp;
        }
    }
}
