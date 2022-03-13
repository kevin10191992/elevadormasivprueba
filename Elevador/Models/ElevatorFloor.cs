namespace Elevador.Models
{
    public class ElevatorFloor
    {
        public int ElevatorFloorId { get; set; }
        public int CurrentElevatorFloor { get; set; }
        public DateTime CurrenteTime { get; set; } = DateTime.Now;
    }
}
