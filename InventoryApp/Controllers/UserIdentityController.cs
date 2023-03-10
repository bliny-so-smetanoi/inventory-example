using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers;

public class UserIdentityController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult Logout()
    {
        if (Request.Cookies.ContainsKey("auth"))
        {
            Response.Cookies.Delete("auth");
            return Redirect("/Home/Index");
        }
        else
        {
            return Ok();
        }
    }
    
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        if (Request.Cookies.ContainsKey("auth"))
        {
            return Json(new {Message = "Already logged in" });
        }
        else
        {
            var cookiesOption = new CookieOptions();
            cookiesOption.Expires = DateTimeOffset.Now.AddDays(1);
            cookiesOption.Path = "/";
            Response.Cookies.Append("auth", "1234", cookiesOption);
            return Redirect("~/Home");
        }
        
        return Json(new { });
    }
}