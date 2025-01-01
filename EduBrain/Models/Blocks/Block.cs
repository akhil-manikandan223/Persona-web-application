using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.Blocks
{
    public class Block
    {
        [Key]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
    }
}
