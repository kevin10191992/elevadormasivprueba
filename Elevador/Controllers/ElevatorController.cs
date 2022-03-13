using Elevador.Interface;
using Elevador.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elevador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElevatorController : ControllerBase
    {

        private readonly ILogger<ElevatorController> _logger;
        private readonly IElevator _elevator;
        private readonly ElevatorRules _elevatorRules;

        public ElevatorController(ILogger<ElevatorController> logger, IElevator elevator, IConfiguration configuration)
        {
            _logger = logger;
            _elevator = elevator;
            _elevatorRules = new ElevatorRules(configuration);
        }

        [HttpGet]
        [Route("GetCurrentState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ElevatorState>> GetCurrentState()
        {
            ElevatorState resp = await _elevator.GetElevatorState();
            _logger.LogInformation("The elevator is Actually in the floor #{number} at:{time}", resp.CurrentFloor, DateTime.Now);
            return Ok(resp);
        }

        [HttpPost]
        [Route("CallFromInside")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ElevatorResponse>> CallFromInside([FromBody] CallFromInsideRequest InRequest)
        {
            _logger.LogInformation("The elevator is called from Inside at:{time}", DateTime.Now);
            ElevatorResponse response;
            ElevatorWork request = new ElevatorWork
            {
                CalledFromInside = true,
                ToFloor = InRequest.ToFloor
            };

            if (_elevatorRules.IsValidToFloor(InRequest.ToFloor))
            {
                response = await _elevator.CallFromInside(request);
                return Ok(response);
            }
            else
            {
                response = new ElevatorResponse(0, $"The selected ToFloor is not valid, the max ToFloor is: {_elevatorRules.MaxFloors}");
                response.RequestAccepted = false;
                return BadRequest(response);
            }
        }


        [HttpPost]
        [Route("CallFromOutside")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ElevatorResponse>> CallFromOutside([FromBody] CallFromOutSideRequest OutRequest)
        {
            _logger.LogInformation("The elevator is called from Outside at:{time}", DateTime.Now);
            ElevatorResponse response;
            ElevatorWork request = new ElevatorWork
            {
                CalledFromInside = false,
                FromFloor = OutRequest.FromFloor
            };

            if (_elevatorRules.IsValidFromFloor(OutRequest.FromFloor))
            {
                response = await _elevator.CallFromOutside(request);
                return Ok(response);
            }
            else
            {
                response = new ElevatorResponse(0, $"The selected FromFloor is not valid, the range of floors is: {_elevatorRules.InitialFloor} - {_elevatorRules.MaxFloors}");
                response.RequestAccepted = false;
                return BadRequest(response);

            }


        }
    }
}
