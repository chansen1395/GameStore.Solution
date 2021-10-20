using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameStore.Models;
using System.Threading.Tasks;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GameStore.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private readonly GameStoreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    // private readonly RoleAdmin<IdentityRole> roleAdmin;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, GameStoreContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(LoginViewModel model)
    {
    if(model.Email != null && model.Password != null)
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
        ModelState.AddModelError("password", "The email or password is incorrect");
        ModelState.AddModelError("", "The email or password provided is incorrect.");
          return View();
        }
      }
    else
      {
        return View();
      }
    }
    
    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      // string test = Request.Headers["Referer"].ToString();
      // System.Console.WriteLine(test);
      if (result.Succeeded)
      {
        // return Redirect(referrer);
        return RedirectToAction("Index", "Home");
        // return Redirect(model.PreviousUrl);
        // return Redirect(Request.Headers["Referer"].ToString());
      }
      else
      {
        ModelState.AddModelError("password", "The email or password is incorrect");
        ModelState.AddModelError("", "The email or password provided is incorrect.");
        return View();
      }
    }
  }
}