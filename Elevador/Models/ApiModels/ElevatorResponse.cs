namespace Elevador.Models
{
    public class ElevatorResponse
    {
        public bool RequestAccepted { get; set; } = true;
        public string Message { get; set; }

        public ElevatorResponse(int Floor, string ResponseText = "")
        {
            if (string.IsNullOrEmpty(ResponseText))
            {
                Message = $"Please Wait while the elevator moving to floor {Floor}";
            }
            else
            {
                Message = ResponseText;
            }

        }
    }
}
