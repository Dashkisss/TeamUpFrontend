using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using comp231_002__Team1_TeamUp_SportsBooking.Models;

namespace comp231_002__Team1_TeamUp_SportsBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ============================================
        // LOGIN (GET)
        // ============================================
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // ============================================
        // LOGIN (POST)
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine("LOGIN POST HIT");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("LOGIN MODEL INVALID");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                Console.WriteLine("LOGIN FAILED: USER NOT FOUND");
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                false,
                false);

            if (!result.Succeeded)
            {
                Console.WriteLine("LOGIN FAILED: WRONG PASSWORD");
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            Console.WriteLine("LOGIN SUCCESS");
            return RedirectToAction("Index", "Home");
        }

        // ============================================
        // REGISTER (GET)
        // ============================================
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // ============================================
        // REGISTER (POST)
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine("REGISTER POST HIT");   // DEBUG

            // 🔍 DEBUG: Show incoming values
            Console.WriteLine($"Email: {model.Email}");
            Console.WriteLine($"Password length: {model.Password?.Length}");
            Console.WriteLine($"Confirm Password length: {model.ConfirmPassword?.Length}");
            Console.WriteLine($"Role: {model.Role}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("REGISTER MODEL INVALID");

                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine("MODEL ERROR: " + err.ErrorMessage);

                return View(model);
            }

            // Create Identity user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            Console.WriteLine("REGISTER: Creating user…");

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                Console.WriteLine("REGISTER FAILED");

                foreach (var error in result.Errors)
                {
                    Console.WriteLine("IDENTITY ERROR: " + error.Description);
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            Console.WriteLine("REGISTER SUCCESS");

            // Optional: assign role ONLY if roles exist
            if (!string.IsNullOrEmpty(model.Role))
            {
                try
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    Console.WriteLine($"ROLE ASSIGNED: {model.Role}");
                }
                catch
                {
                    Console.WriteLine("ROLE ASSIGN FAILED (role may not exist, safe to ignore)");
                }
            }

            // Redirect to Login (not auto-login)
            return RedirectToAction("Login");
        }

        // ============================================
        // LOGOUT
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("USER LOGGED OUT");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
