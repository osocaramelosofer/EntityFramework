using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MachineStatusCatalog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Color { get; set; }


        public Boolean Enabled { get; set; }
    }
}
