using System.Net.Mail;
using System.Text;
using GeoliseFlix.DataTransferObjects;
using GeoliseFlix.Models;
using GeoliseFlix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace GeoliseFlix.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserStore<AppUser> _userStore;
    private readonly IUserEmailStore<AppUser> _emailStore;
    private readonly IEmailSender _emailSender;

    public AccountController(
         ILogger<AccountController> logger, 
         SignInManager<AppUser> signInManager, 
         UserManager<AppUser> userManager, 
         IUserStore<AppUser> userStore,
         IEmailSender emailSender
    )

    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = (IUserEmailStore<AppUser>)_userStore;
        _emailSender = emailSender;
    }


    [Authorize(Roles = "Administrador")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginDto login = new();
        login.ReturnUrl = returnUrl ?? Url.Content("~/");
        
        return View(login);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        // Verificar o modelo e fazer o login
        if(ModelState.IsValid) // Validação do lado do servidor
        {
            string userName = login.Email;
            if(IsValidEmail(login.Email))
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                    userName = user.UserName;
                //Operadores Lógicos
                // && - e || - ou ! - não != - diferente de
            }
            var result = await _signInManager.PasswordSignInAsync(
                userName, login.Password, login.RememberMe, lockoutOnFailure: true
            );
            if(result.Succeeded)
            {
                _logger.LogInformation($"Usuário { login.Email } acessou o sistema");
                return LocalRedirect(login.ReturnUrl);
            }
            if(result.IsLockedOut)
            {
                _logger.LogWarning($"Usuário { login.Email } está bloqueado");
                return RedirectToAction("Lockout");
            }
            ModelState.AddModelError("login", "Usuário e/ou Senha Inválidos!!!");
        }
        return View(login);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

     [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        if (ModelState.IsValid)
        {
            var user = Activator.CreateInstance<AppUser>();

            user.Name = register.Name;
            user.DateOfBirth = register.DateOfBirth;
            user.Email = register.Email;

            await _userStore.SetUserNameAsync(
                user, register.Email, CancellationToken.None
            );
            await _emailStore.SetEmailAsync(
                user, register.Email, CancellationToken.None
            );

            var result = await _userManager.CreateAsync(
                user, register.Password 

            );
            if(result.Succeeded)
            {
                _logger.LogInformation($"Novo usuário registrado com o email {user.Email}");

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(
                    "ConfirmEmail", "Account", 
                    new { userId = userId, code = code },
                    protocol: Request.Scheme
                );
                
            }

        }
        return View(register);
    }





    private bool IsValidEmail(string email)
    {
        try 
        {
            MailAddress m = new(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
         
    }

}
