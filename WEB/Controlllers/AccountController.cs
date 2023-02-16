using DOMAIN.Identities;
using DTO.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;

namespace WEB.Controlllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IFileWork _fileWork;
		private readonly IEmailWork _emailWork;

		public AccountController(SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IFileWork fileWork, IEmailWork emailWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
            _fileWork = fileWork;
			_emailWork = emailWork;
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginDto dto)
		{
			var appUser = _userManager.FindByEmailAsync(dto.Email).Result;
			if (appUser is not null)
			{
				if (_userManager.CheckPasswordAsync(appUser, dto.Password).Result)
				{
					if (!_userManager.IsLockedOutAsync(appUser).Result)
					{
						_userManager.SetLockoutEndDateAsync(appUser, null).Wait();
						_userManager.ResetAccessFailedCountAsync(appUser).Wait();
						var result = _signInManager.PasswordSignInAsync(appUser, dto.Password, true, false).Result;
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
						else
						{
                            ModelState.AddModelError("", "unexpected error");
                        }
					}
					else
					{
                        ModelState.AddModelError("", $"Locked account for {_userManager.GetLockoutEndDateAsync(appUser).Result}");
                    }
				}
				else
				{
					_userManager.AccessFailedAsync(appUser).Wait();
                    ModelState.AddModelError("", "Unregistered email or invalid password");
                }
			}
			else
			{
				ModelState.AddModelError("", "Unregistered email or invalid password");
			}

			return View(dto);
		}


		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterDto dto)
		{
			var appUser = new AppUser
			{
				Email = dto.Email,
				PhoneNumber= dto.PhoneNumber,
				UserName = dto.Email.Split("@")[0],
				PhotoPath = dto.Photo != null ? _fileWork.UploadFile(dto.Photo,"_uploads","profile-photos") : null,
			};

			var created = _userManager.CreateAsync(appUser,dto.Password).Result;

			if (created.Succeeded)
			{
				return RedirectToAction("Login");
			}
			else
			{
				created.Errors.ToList().ForEach(x => {
					ModelState.AddModelError("", x.Description);
				});
                return View(dto);
            }
		}

		[HttpGet]
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync().Wait();
			return RedirectToAction("Login");
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ForgotPassword(ForgotDto dto)
		{
			var user = _userManager.FindByEmailAsync(dto.Email).Result;
			if (user != null)
			{
				var resetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                string resetLink = Url.Action("ResetPassword", "Password", new
                {
                    UserId = user.Id,
                    Token = resetToken,
                }, HttpContext.Request.Scheme);

                _emailWork.ToAddresses.Add(dto.Email);
				_emailWork.BccAddresses.Add("senfurkan92@gmail.com");
				_emailWork.MailBody = $"<a href='{resetLink}'>Password Recovery</a>";
				var result = _emailWork.SendMail();

				if (result.IsSuccess)
				{

				}
				else
				{
					ModelState.AddModelError("", result.Exception!.Message);
				}
			}
			else
			{
                ModelState.AddModelError("", "Unregistered email");
            }
			return View(dto);
		}

		[HttpGet]
		public IActionResult RecoverPassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RecoverPassword(ResetPasswordDto dto)
		{
			return View(dto);
		}
	}
}
