using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsItConvergent.Models.DbModels
{
    [Table("LinuxApp")] // <-- Add this line
    public class LinuxApp
    {
        //Identification info
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IIC_ID { get; set; }//Is It Convergent ID
        public string Version { get; set; } = string.Empty;

        //Display info
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }
}