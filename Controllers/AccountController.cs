using MeetingRoom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoom.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username != null && password != null && username.Equals("user") && password.Equals("123"))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "RoomDetails");
            }
            else if (username != null && password != null && username.Equals("admin") && password.Equals("123"))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "RoomDetails");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}
