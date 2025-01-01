using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.States
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
