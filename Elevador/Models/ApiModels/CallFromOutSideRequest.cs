using System.ComponentModel.DataAnnotations;

namespace Elevador.Models
{
    public class CallFromOutSideRequest
    {
        [Range(0, int.MaxValue)]
        public int FromFloor { get; set; }
    }
}
