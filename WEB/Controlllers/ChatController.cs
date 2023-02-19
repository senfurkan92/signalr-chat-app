using DOMAIN.Identities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controlllers
{
    [Authorize]
    public class ChatController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;

        public ChatController(UserManager<AppUser> userManager)
        {
            _userManager= userManager;
        }

        public IActionResult Index()
        {
            TempData["CurrentUserId"] = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View();
        }

        [HttpGet]
        public IActionResult GetChatUser(string filter = "")
        {
            var users = _userManager.Users.Where(x => x.UserName.ToLower().Contains(filter)).ToList();
            return Ok(users);
        }

        [HttpGet]
        public IActionResult GetChatStart(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            return Ok(user);
        }
    }
}
