using IsItConvergent.Models.DbModels;
using IsItConvergent.Models.ViewModels;

namespace IsItConvergent.Models.DataModels;

public class HomeData
{
    public static HomePage_VM GetHomePageData(IsItConvergentDbContext db, string userId = "")
    {
        var accessLevel = GetAccessLevel(db, userId);
        var rawApps = db.LinuxApps.ToList();
        var apps = rawApps.Select(x => new LinuxApp_VM
        {
            IIC_ID = x.IIC_ID,
            Name = x.Name,
            Description = x.Description,
            IconUrl = x.IconUrl,
            Version = x.Version
        }).ToList();
        // For now, return a dummy HomePage_VM object.
        var homePageData = new HomePage_VM
        {
            Title = "Is It Convergent?",
            AccessLevel = accessLevel,
            // LinuxApps = GetDemoLinuxApps()
            LinuxApps = apps
        };

        return homePageData;
    }

    public static int GetAccessLevel(IsItConvergentDbContext db, string userId = "")
    {
        // This method can be used to determine the access level of the user.
        // For now, it returns a dummy access level of 0 (Guest).
        var user = db.UserAccessLevels.FirstOrDefault(x => x.UserId == userId)?.AccessLevel ?? 0;
        if (user == 0)
        {
            if (userId == "0f81d593-8eaa-4f77-afbd-705f647c6607")
            {
                db.UserAccessLevels.Add(new UserAccessLevel
                {
                    UserId = userId,
                    AccessLevel = 3 // Admin
                });
                db.SaveChanges();
            }
            // Any other users should be given a default access level of 1 (User) if they do not already have one.
            else if (userId != "")
            {
                db.UserAccessLevels.Add(new UserAccessLevel
                {
                    UserId = userId,
                    AccessLevel = 1 // User
                });
                db.SaveChanges();
            }
            else
            {
                user = 0; // Guest
            }
        }
        return user;
    }



    /// <summary>
    /// Load more fleshed out product details for this specific app.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static LinuxApp_VM GetProductDetails(IsItConvergentDbContext db, int appId, string userId = "")
    {
        var accessLevel = GetAccessLevel(db, userId);

        /*
        Get all ratings for this app from other users.

        */
        var communityRatings = UserRatingsData.GetCommunityRatings(db, appId, userId);
        


        var yourRatings = UserRatingsData.GetCurrentUserRatings(db, userId, appId);
        var targetApp = db.LinuxApps.FirstOrDefault(x => x.IIC_ID == appId);

        var output = new LinuxApp_VM
        {
            IIC_ID = targetApp?.IIC_ID ?? 0,
            Name = targetApp?.Name ?? "Unknown App",
            Description = targetApp?.Description ?? "No details available for this app.",
            IconUrl = targetApp?.IconUrl ?? "https://example.com/unknown_icon.png",
            Version = targetApp?.Version ?? "0.0.0",
            AccessLevel = accessLevel,
            CommunityRatings = communityRatings,
            YourRatings = yourRatings,
        };
        return output;
    }

    public static int AddNewApp(IsItConvergentDbContext db, LinuxApp_VM formData)
    {
        // This method can be used to add a new app to the database.
        // For now, it returns a dummy ID for the newly added app.
        var newApp = new LinuxApp
        {
            Name = formData.Name,
            Description = formData.Description,
            IconUrl = formData.IconUrl,
            Version = formData.Version
        };

        db.LinuxApps.Add(newApp);
        db.SaveChanges();

        return newApp.IIC_ID; // Return the ID of the newly added app
    }

    public static List<LinuxApp_VM> GetDemoLinuxApps()
    {
        // This method can be used to retrieve a list of Linux apps for demo purposes.
        // For now, it returns an empty list.
        return new List<LinuxApp_VM>
            {
                new LinuxApp_VM
                {
                    IIC_ID = 1,
                    Name = "Firefox",
                    Description = "Fast, Private & Safe Web Browser",
                    IconUrl = "https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Frepo%2Fappstream%2Fx86_64%2Ficons%2F128x128%2Forg.mozilla.firefox.png&w=256&q=100",
                    Version = "138.0.4"
                },
                //https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Frepo%2Fappstream%2Fx86_64%2Ficons%2F128x128%2Forg.mozilla.firefox.png&w=256&q=100
                new LinuxApp_VM
                {
                    IIC_ID = 2,
                    Name = "Telegram",
                    Description = "New era of messaging",
                    IconUrl = "https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Fmedia%2Forg%2Ftelegram%2Fdesktop%2F2811c4c7156f7ee84796dbec9559aa71%2Ficons%2F128x128%2Forg.telegram.desktop.png&w=256&q=100",
                    Version = "5.14.3"
                },
                new LinuxApp_VM
                {
                    IIC_ID = 3,
                    Name = "Elisa",
                    Description = "Play local music and listen to online radio",
                    IconUrl = "https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Fmedia%2Forg%2Fkde%2Felisa.desktop%2Fb933e8a86e378ec958c0a25af06c69a1%2Ficons%2F128x128%2Forg.kde.elisa.desktop.png&w=256&q=100",
                    Version = "25.04.1"
                },

            new LinuxApp_VM
                {
                    IIC_ID = 4,
                    Name = "Steam",
                    Description = "Launcher for the Steam software distribution service",
                    IconUrl = "https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Fmedia%2Fcom%2Fvalvesoftware%2FSteam%2Fb681fc80441a6843424afa8a28f9ef8e%2Ficons%2F128x128%2Fcom.valvesoftware.Steam.png&w=256&q=100",
                    Version = "1.0.0.81"
                },

                         new LinuxApp_VM
                {
                    IIC_ID = 5,
                    Name = "Visual Studio Code",
                    Description = "Code editing. Redefined.",
                    IconUrl = "https://flathub.org/_next/image?url=https%3A%2F%2Fdl.flathub.org%2Fmedia%2Fcom%2Fvisualstudio%2Fcode%2F366ac07943dcd2ac0eafd96c8952b915%2Ficons%2F128x128%2Fcom.visualstudio.code.png&w=256&q=100",
                    Version = "1.100.2"
                }
            };
    }
}