using System.ComponentModel.DataAnnotations;

namespace Elevador.Models
{
    public class CallFromInsideRequest
    {
        [Range(0,int.MaxValue)]
        public int ToFloor { get; set; }
    }
}
