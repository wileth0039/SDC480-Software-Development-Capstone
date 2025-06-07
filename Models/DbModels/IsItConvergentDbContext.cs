using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IsItConvergent.Models.DbModels;

/// <summary>
/// DbContext used for connecting to the IsItConvergent database.
/// </summary>
public class IsItConvergentDbContext : IdentityDbContext
{
    /// <summary>
    /// Constructor for the IsItConvergentDbContext.
    /// </summary>
    /// <param name="options"></param>
    public IsItConvergentDbContext(DbContextOptions<IsItConvergentDbContext> options) : base(options) { }

    #region Tables
    public DbSet<LinuxApp> LinuxApps { get; set; }
    public DbSet<UserAccessLevel> UserAccessLevels { get; set; }
    public DbSet<AppUsabilityRating> AppUsabilityRatings { get; set; }
    #endregion
}