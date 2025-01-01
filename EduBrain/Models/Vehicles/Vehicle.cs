using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.Vehicles
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
    }
}
