using System.ComponentModel.DataAnnotations;

namespace IsItConvergent.Models.DbModels
{
    /// <summary>
    /// This table contains metadata about the possible scores for usability ratings of apps.
    /// </summary>
    public class UsabilityGrade
    {
        [Key]
        public int Grade { get; set; } //1-5
        public bool IsActive { get; set; }
        public string ShortDescrip { get; set; } = string.Empty; // Short description of the usability grade
        public string FullDescrip { get; set; } = string.Empty; // Description of the usability grade

    }
}