using EduBrain.Models.States;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Locations
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
