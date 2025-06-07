using System.ComponentModel.DataAnnotations;

namespace IsItConvergent.Models.DbModels
{
    public class UserAccessLevel
    {
        [Key]
        public string UserId { get; set; } = string.Empty; // Unique identifier for the user
        public int AccessLevel { get; set; } // 0 = no access, 1 = read-only, 2 = read-write, 3 = admin
        public bool IsActive { get; set; } // Indicates if the access level is active
    }
}