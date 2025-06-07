namespace IsItConvergent.Models.ViewModels
{
    public class CommunityRatings_VM
    {
        public int FormFactorTypeId { get; set; } // The ID of the form factor type (e.g. Desktop, Laptop, etc.)
        public string FormFactorTypeName { get; set; } = string.Empty; // The name of the form factor type
        public double AverageRating { get; set; } = 0.0; // The average rating for this form factor type
        public int TotalRatings { get; set; } = 0; // The total number of ratings for this form factor type

        public List<AppUsabilityRating_VM> UserRatings { get; set; } = new List<AppUsabilityRating_VM>(); // List of user ratings for this form factor type
    }
}
