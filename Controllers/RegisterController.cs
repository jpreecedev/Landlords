namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels;
    using Model.Database;
    using Landlords.Services;
    using System;
    using System.Net;
    using Database;

    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly LLDbContext _context;

        public RegisterController(IUserRepository userRepository, IEmailSender emailSender, LLDbContext context)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _context = context;
        }

        [HttpPost("resendverification")]
        public async Task<IActionResult> ResendVerificationCode()
        {
            var user = User.GetApplicationUser(_context);
            var code = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

            await _emailSender.SendEmailAsync(new EmailData
            {
                RecipientName = $"{user.FirstName} {user.LastName}",
                RecipientEmail = user.Email,
                Subject = "Confirm Email",
                Body = $"Click this <a href=\"http://localhost:52812/api/register/confirmemail?userId={user.Id}&code={Uri.EscapeDataString(code)}\">link</a> mofo."
            });

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RegisterUserViewModel data)
        {
            IdentityResult registrationResult = null;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = user.Email = data.EmailAddress;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;

                registrationResult = await _userRepository.CreateAsync(user, data.Password);
                if (registrationResult.Succeeded)
                {
                    // TODO
                    await _userRepository.AddToRoleAsync(user, ApplicationRoles.Landlord);

                    var code = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
                    await _emailSender.SendEmailAsync(new EmailData
                    {
                        RecipientName = $"{user.FirstName} {user.LastName}",
                        RecipientEmail = user.Email,
                        Subject = "Confirm Email",
                        Body = $"Click this <a href=\"http://localhost:52812/api/register/confirmemail?userId={user.Id}&code={Uri.EscapeDataString(code)}\">link</a> mofo."
                    });
                    return Ok();
                }
            }

            return BadRequest(new
            {
                Errors = ModelState.ErrorCount > 0 ? ModelState.ToErrorCollection() : registrationResult.Errors.ToGeneric().ToErrorCollection()
            });
        }

        [HttpGet("confirmemail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string code)
        {
            var result = await _userRepository.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return Redirect("http://localhost:8080/profile?confirmed=true");
            }
            return BadRequest(new {Errors = result.Errors.ToErrorCollection()});
        }
    }
}