namespace Elevador.Models
{
    public class ElevatorWork
    {
        public int ElevatorWorkId { get; set; }
        public bool CalledFromInside { get; set; }
        public int FromFloor { get; set; }
        public int ToFloor { get; set; }
        public bool RequestCompleted { get; set; } = false;
        public DateTime RequestTime { get; set; } = DateTime.Now;
        public DateTime? CompletedTime { get; set; }
    }
}
