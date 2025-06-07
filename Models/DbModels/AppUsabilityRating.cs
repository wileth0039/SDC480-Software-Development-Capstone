/*
Performance ratings.

- perfect score: No issues found using this app in this form factor.
- good score: Completely functional, but some minor issues found (such as small text or cropped images).
- bad score: Some features work, but some are unavailable or broken in this form factor.
- Unusable: Completely usable in this form factor.
- unknown score: The app has not been tested in this form factor.
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsItConvergent.Models.DbModels;

// Composite key: AppId, UserId, FormFactorTypeId
[PrimaryKey(nameof(AppId), nameof(UserId), nameof(FormFactorTypeId))]
public class AppUsabilityRating
{
    // Identification parameters
    public int AppId { get; set; }
    public string UserId { get; set; } = string.Empty; // User who submitted the rating, can be empty for guest users
    public string Version { get; set; } = string.Empty; // Version of the app being rated

    public int FormFactorTypeId { get; set; }
    public int UsabilityScore { get; set; } // Score from 1 to 5
    public string Comments { get; set; } = string.Empty; // Additional comments on usability
    public DateTime DateAdded { get; set; } = DateTime.UtcNow; // Date when the rating was added
    public DateTime DateModified { get; set; } = DateTime.UtcNow; // Date when the rating was last modified
}