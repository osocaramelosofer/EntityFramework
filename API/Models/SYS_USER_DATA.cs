using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class SYS_USER_DATA
    {
        [Key]
        public int Id { get; set; }
        public string USERID { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string PWD { get; set; }
        public string ROLID { get; set; }
        public string MAIL { get; set; }
        public Boolean ENABLED { get; set; }
        public DateTime LAST_ACCESS { get; set; }
        public string COMPANY { get; set; }
        public Boolean RESET_PASSWORD { get; set; }
    }
}
