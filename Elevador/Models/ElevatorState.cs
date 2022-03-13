namespace Elevador.Models
{
    public class ElevatorState
    {
        public int CurrentFloor { get; set; }
        public List<ElevatorWork>? ListPendingRequest { get; set; }
    }
}
