using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsItConvergent.Models.ViewModels
{
    public class LinuxApp_VM
    {
        //Identification info
        public int IIC_ID { get; set; }//Is It Convergent ID

        [Required]
        public string Version { get; set; } = string.Empty;

        //Display info
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string IconUrl { get; set; } = string.Empty;


        //Community Ratings and Stats
        /*
        We want a list of Rating sections:
        One object per rating type (e.g. FormFactorTypeId)
        Each object should contain
        - FormFactorTypeId
        - FormFactorTypeName
        - AverageRating
        - TotalRatings
        - List of UserRatings (UserId, GradeOption, Comments)
        */
        
        public List<CommunityRatings_VM> CommunityRatings { get; set; } = new List<CommunityRatings_VM>(); // List of community ratings for this app

        public int AccessLevel { get; set; }
        //Form Values
        public List<AppUsabilityRating_VM> YourRatings { get; set; } = new List<AppUsabilityRating_VM>(); // List of ratings provided by the user
    }

}

