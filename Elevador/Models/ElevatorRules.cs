namespace Elevador.Models
{
    public class ElevatorRules
    {
        private readonly IConfiguration _configuration;

        public int MaxFloors { get; }
        public int InitialFloor { get; }

        public int SpeedInSeconds { get; }

        public string HumanSpeechRules { get; }

        public ElevatorRules(IConfiguration configuration)
        {
            _configuration = configuration;
            MaxFloors = _configuration.GetValue<int>("MaxFloors");
            InitialFloor = _configuration.GetValue<int>("InitialFloor"); ;
            SpeedInSeconds = _configuration.GetValue<int>("SpeedInSeconds");
            HumanSpeechRules = string.Format(_configuration.GetValue<string>("HumanSpeechRules"), MaxFloors, InitialFloor, SpeedInSeconds);
        }

        public bool IsValidToFloor(int ToFloor)
        {
            return ToFloor <= MaxFloors;
        }

        public bool IsValidFromFloor(int FromFloor)
        {
            return FromFloor >= InitialFloor;
        }
    }
}
