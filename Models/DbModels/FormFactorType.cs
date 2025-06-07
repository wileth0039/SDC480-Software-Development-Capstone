using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsItConvergent.Models.DbModels
{
    /// <summary>
    /// This table contains metadata about the possible form factors for apps.
    /// </summary>
    public class FormFactorType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FormFactorTypeId { get; set; } // Primary key
        public string Name { get; set; } = string.Empty; // Name of the form factor type
        public bool IsActive { get; set; } // Indicates if the form factor type is active
        public string ShortDescrip { get; set; } = string.Empty; // Short description of the form factor type
        public string FullDescrip { get; set; } = string.Empty; // Description of the form factor type
    }
}