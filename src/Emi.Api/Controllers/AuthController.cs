using System.Security.Claims;
using Emi.Infrastructure.Options;
using Emi.Api.Contracts.Auth;
using Emi.Application.Abstractions.Security;
using Emi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Emi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwt;
    private readonly JwtOptions _opts;

    public AuthController(UserManager<AppUser> userManager, IJwtTokenService jwt, IOptions<JwtOptions> opts)
    => (_userManager, _jwt, _opts) = (userManager, jwt, opts.Value);

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest r)
    {
        var exists = await _userManager.FindByEmailAsync(r.Email);
        if (exists is not null) return Conflict("User already exists");

        var user = new AppUser { UserName = r.Email, Email = r.Email, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user, r.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "User"); // rol por defecto
        return CreatedAtAction(nameof(Register), new { email = user.Email }, null);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest r)
    {
        var user = await _userManager.FindByEmailAsync(r.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, r.Password))
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwt.CreateToken(user.Id, user.UserName!, roles);
        return Ok(new AuthResponse { Token = token, Expires = DateTime.UtcNow.AddHours(2) });
    }
}