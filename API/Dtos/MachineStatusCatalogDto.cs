using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class MachineStatusCatalogDto
    {

        [Required(ErrorMessage = "The field Description is required.")]
        [MaxLength(100, ErrorMessage ="The max number of characteres is 100.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field Color is required.")]
        [MaxLength(50, ErrorMessage ="The number max of characteres is 50.")]
        public string Color { get; set; }

        [Required]
        public Boolean Enabled { get; set; }
    }
}
