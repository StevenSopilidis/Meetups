using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        TokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        //check if email or username is already taken
        if(await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            ModelState.AddModelError("email", "Email already taken");
            return ValidationProblem();
        }
        if(await _userManager.Users.AnyAsync(u => u.UserName == registerDto.Username))
        {
            ModelState.AddModelError("username", "Username already taken");
            return ValidationProblem();
        }
        var user = new AppUser
        {
            UserName= registerDto.Username,
            DisplayName= registerDto.DisplayName,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if(result.Succeeded)
        {
            return CreateUserObject(user);
        }

        return BadRequest("Could not register");
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        if (loginDto.Email == null) return Unauthorized();
        //get the user
        var user = await _userManager.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        //if wrong email
        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        //if valid password
        if (result.Succeeded)
        {
            return CreateUserObject(user);
        }

        //if invalid password
        return Unauthorized();
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);
    }


    private UserDto CreateUserObject(AppUser user)
    {
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Image = user?.Photos?.FirstOrDefault(p => p.IsMain)?.Url,
            Token= _tokenService.CreateToken(user),
            Username= user.UserName
        };
    }
}   
}