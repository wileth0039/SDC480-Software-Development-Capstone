using IsItConvergent.Models.DbModels;
using IsItConvergent.Models.ViewModels;

namespace IsItConvergent.Models.DataModels
{
    public static class UserRatingsData
    {
        public static List<CommunityRatings_VM> GetCommunityRatings(IsItConvergentDbContext db, int appId, string userId = "")
        {
            /*
            Flow:
            Part 1: statistics
            1. Select the FormFactorType and Grade for ALL ratings for the specified app. This will be used for statistics.
            2. For each form factor, calculate the average rating and total number of ratings.

            Part 2: Latest Ratings (Per Form Factor)
            1. For each FormFactor that has ratings, get the latest 10 ratings for that form factor.
            */
            var output = new List<CommunityRatings_VM>();

            // Part 1: Statistics
            //Get all ratings for the specified app
            var allRatings = db.AppUsabilityRatings
                .Where(x => x.AppId == appId)
                .ToList();

            //Get all distinct form factor types from the ratings
            var formFactorTypes = FormFactorType();
            var usedCategories = allRatings.Select(x => x.FormFactorTypeId).Distinct().ToList();
            var userIds = allRatings.Select(x => x.UserId).Distinct().ToList();
            var userNames = db.Users
                .Where(x => userIds.Contains(x.Id))
                .ToDictionary(x => x.Id, x => x.UserName);

            foreach (var category in usedCategories)
            {

                var categoryRatings = new CommunityRatings_VM()
                {
                    FormFactorTypeId = category,
                    FormFactorTypeName = formFactorTypes.FirstOrDefault(x => x.FormFactorTypeId == category)?.Name ?? "Unknown",
                    AverageRating = allRatings
                        .Where(x => x.FormFactorTypeId == category)
                        .Average(x => x.UsabilityScore),
                    TotalRatings = allRatings.Count(x => x.FormFactorTypeId == category),
                    UserRatings = allRatings
                        .Where(x => x.FormFactorTypeId == category)
                        .Select(x => new AppUsabilityRating_VM
                        {
                            AppId = x.AppId,
                            UserId = x.UserId,
                            UserName = userNames.TryGetValue(x.UserId, out var name) ? name : "Unknown", // Fix: Avoid possible null reference
                            FormFactorTypeId = x.FormFactorTypeId,
                            UsabilityScore = x.UsabilityScore,
                            Comments = x.Comments,
                            DateAdded = x.DateAdded,
                            DateModified = x.DateModified,
                            CurrentVersion = db.LinuxApps.FirstOrDefault(a => a.IIC_ID == appId)?.Version ?? string.Empty,
                            LastRatedVersion = x.Version
                        })
                        .OrderByDescending(x => x.DateAdded)
                        .Take(10)
                        .ToList()

                };
                output.Add(categoryRatings);
            }
            return output;

        }
        public static List<AppUsabilityRating_VM> GetCurrentUserRatings(IsItConvergentDbContext db, string userId, int appId)
        {
            /*
            Get current user ratings for a specific app.
            If the user has not rated the apps performance in a specific form factor, return an empty object for that category.
            This is used to populate the form with the user's existing ratings, allowing them to update or change their ratings.
            If the user has not rated the app at all, return an empty list.
            */

            //TODO: Get FormFactorTypeIds from DB
            var categories = UserRatingsData.FormFactorType(); // Example form factor type IDs 

            //TODO: Get possible usability scores from the DB.
            var usabilityScores = UserRatingsData.GetUsabilityGrades(); // Example usability scores


            //TODO: Get existing ratings from the DB for the specified user and app.
            var existingRatings = db.AppUsabilityRatings
                .Where(x => x.UserId == userId && x.AppId == appId)
                .ToList();

            // var targetApp = HomeData.GetDemoLinuxApps().FirstOrDefault(x => x.IIC_ID == appId);
            var targetApp = db.LinuxApps.FirstOrDefault(x => x.IIC_ID == appId);

            var userRatings = new List<AppUsabilityRating_VM>();
            foreach (var category in categories)
            {
                var rating = new AppUsabilityRating_VM()
                {
                    AppId = appId,
                    CurrentVersion = targetApp.Version,
                    LastRatedVersion = existingRatings.FirstOrDefault(x => x.FormFactorTypeId == category.FormFactorTypeId)?.Version ?? string.Empty,
                    UserId = userId,
                    FormFactorTypeId = category.FormFactorTypeId,
                    FormFactorTypeDescrip = category.Name,
                    UsabilityScore = existingRatings.FirstOrDefault(x => x.FormFactorTypeId == category.FormFactorTypeId)?.UsabilityScore ?? 0,
                    Comments = existingRatings.FirstOrDefault(x => x.FormFactorTypeId == category.FormFactorTypeId)?.Comments ?? string.Empty,
                    DateAdded = existingRatings.FirstOrDefault(x => x.FormFactorTypeId == category.FormFactorTypeId)?.DateAdded ?? DateTime.UtcNow,
                    DateModified = existingRatings.FirstOrDefault(x => x.FormFactorTypeId == category.FormFactorTypeId)?.DateModified,
                    GradeOptions = usabilityScores
                };
                userRatings.Add(rating);
            }

            return userRatings;
        }

        public static bool SubmitRating(IsItConvergentDbContext db, int appId, string userId, int formFactorTypeId, int usabilityScore, string comments)
        {
            /*
            Submit a new rating for an app.
            If the user has already rated the app in this form factor, update the existing rating.
            Otherwise, create a new rating.
            */

            var existingRating = db.AppUsabilityRatings
                .FirstOrDefault(x => x.AppId == appId && x.UserId == userId && x.FormFactorTypeId == formFactorTypeId);

            if (existingRating != null)
            {
                // Update existing rating
                existingRating.UsabilityScore = usabilityScore;
                existingRating.Comments = comments ?? string.Empty;
                existingRating.DateModified = DateTime.UtcNow;
            }
            else
            {
                // Create new rating
                var newRating = new AppUsabilityRating
                {
                    AppId = appId,
                    UserId = userId,
                    FormFactorTypeId = formFactorTypeId,
                    UsabilityScore = usabilityScore,
                    Comments = comments ?? string.Empty,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };
                db.AppUsabilityRatings.Add(newRating);
            }

            db.SaveChanges();
            return true;
        }

        public static List<FormFactorType> FormFactorType()
        {
            // This method can be used to retrieve a list of form factor type IDs for demo purposes.
            var formFactorTypes = new List<FormFactorType>
            {
                new FormFactorType { FormFactorTypeId = 1, Name = "Desktop PC" },
                new FormFactorType { FormFactorTypeId = 2, Name = "HTPC/Gaming Handheld" },
                new FormFactorType { FormFactorTypeId = 3, Name = "Tablet" },
                new FormFactorType { FormFactorTypeId = 4, Name = "Mobile" }
            };
            return formFactorTypes;
        }
        /// <summary>
        /// Return a debug list of fake UsabilityGrade objects.
        /// </summary>
        /// <returns></returns>
        public static List<UsabilityGrade> GetUsabilityGrades()
        {
            // This method can be used to retrieve a list of usability grades for demo purposes.
            // For now, it returns an empty list.
            return new()
    {
        new()
        {
            Grade = 5,
            IsActive = true,
            ShortDescrip = "Perfect",
            FullDescrip = "No issues found using this app in this form factor."
        },
        new()
        {
            Grade = 4,
            IsActive = true,
            ShortDescrip = "Good",
            FullDescrip = "Completely functional, but some minor issues found (such as small text or cropped images)."
        },
        new()
        {
            Grade = 3,
            IsActive = true,
            ShortDescrip = "Flawed",
            FullDescrip = "Some features work, but some are unavailable or broken in this form factor."
        },
        new()
        {
            Grade = 2,
            IsActive = true,
            ShortDescrip = "Problematic",
            FullDescrip = "Launches, but largely unusable."
        },
        new()
        {
            Grade = 1,
            IsActive = true,
            ShortDescrip = "Unusable",
            FullDescrip = "The app is completely unusable or broken in this form factor."
        },
        new()
        {
            Grade = 0,
            IsActive = true,
            ShortDescrip = "Unknown",
            FullDescrip = "The app has not been tested in this form factor."
        }
    };
        }
    }
}