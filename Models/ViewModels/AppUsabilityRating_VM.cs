/*
In a Convergence Rating, there's multiple categories:

Desktop PC Usability:

HTPC/Gaming Handheld Usability:

Tablet Usability:

Mobile Usability:
*/


using IsItConvergent.Models.DbModels;

namespace IsItConvergent.Models.ViewModels
{
    public class AppUsabilityRating_VM
    {
        public int AppId { get; set; }
        public string CurrentVersion { get; set; } = string.Empty; // Version of the app being rated
        public string LastRatedVersion { get; set; } = string.Empty; // Last version of the app that was rated by the user
        public string UserId { get; set; } = string.Empty; // User ID of the person rating the app
        public string UserName { get; set; } = string.Empty; // Username of the person rating the app


        public int FormFactorTypeId { get; set; }
        public string FormFactorTypeDescrip { get; set; } = string.Empty; // Description of the form factor type (e.g., Desktop PC, Tablet, etc.)
        public int UsabilityScore { get; set; } // Score from 1 to 5
        public string Comments { get; set; } = string.Empty; // Additional comments on usability
        public DateTime DateAdded { get; set; } = DateTime.UtcNow; // Date when the rating was added
        public DateTime? DateModified { get; set; } // Date when the rating was last modified

        public List<UsabilityGrade> GradeOptions  { get; set; } = new List<UsabilityGrade>(); // List of available usability grades for selection
        // Additional properties can be added as needed
    }
}
