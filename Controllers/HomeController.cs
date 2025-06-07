using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IsItConvergent.Models;
using IsItConvergent.Models.DbModels;
using IsItConvergent.Models.DataModels;
using IsItConvergent.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IsItConvergent.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IsItConvergentDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;




    public HomeController(ILogger<HomeController> logger, IsItConvergentDbContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _dbContext = context;
        _userManager = userManager;
    }


    public IActionResult Index()
    {
        string userId = _userManager.GetUserId(User) ?? string.Empty;
        var homePageData = HomeData.GetHomePageData(_dbContext, userId);
        return View(homePageData);
    }

    public IActionResult AddApp(LinuxApp_VM formData)
    {
        /*
        flow:
        call the HomeData.AddNewApp method with the form data
        if successful, redirect to the Index action
        if not successful, return the Index view with an error message
        */
        var newAppId = HomeData.AddNewApp(_dbContext, formData);
        return RedirectToAction("ProductPage", new { id = newAppId });
    }

    public IActionResult ProductPage(int id)
    {
        string userId = _userManager.GetUserId(User) ?? string.Empty;

        //Load more fleshed out product details for this specific app.
        var appDetails = HomeData.GetProductDetails(_dbContext, id, userId);
        return View(appDetails); // Placeholder
    }

    [HttpPost]
    public IActionResult SubmitUserRating(int UR_AppId, int UR_FormFactorTypeId, int UR_GradeOption, string UR_Comments)
    {
        /*
        flow:
        call the UserRatingsData.SubmitRating method with the form data
        if successful, redirect to the ProductPage action with the app ID
        if not successful, return the ProductPage view with an error message
        */
        var userId = _userManager.GetUserId(User) ?? string.Empty;
        UserRatingsData.SubmitRating(_dbContext, UR_AppId, userId, UR_FormFactorTypeId, UR_GradeOption, UR_Comments);
        return RedirectToAction("ProductPage", new { id = UR_AppId });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
