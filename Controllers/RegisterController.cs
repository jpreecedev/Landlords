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
    using Database;
    using Microsoft.EntityFrameworkCore;

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

        [HttpPost("resendverification"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendVerificationCode()
        {
            var user = User.GetApplicationUser(_context);
            var code = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
            var template = new EmailViewModel
            {
                RecipientName = $"{user.FirstName} {user.LastName}",
                RecipientEmail = user.Email,
                Template = await _context.EmailTemplates.FirstAsync(c => c.Key == "ResendVerification")
            };
            template.SetBody(user.Id.ToString(), code);

            await _emailSender.SendEmailAsync(template);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
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
                    var template = new EmailViewModel
                    {
                        RecipientName = $"{user.FirstName} {user.LastName}",
                        RecipientEmail = user.Email,
                        Template = await _context.EmailTemplates.FirstAsync(c => c.Key == "Register")
                    };
                    template.SetBody(user.Id.ToString(), code);
                    await _emailSender.SendEmailAsync(template);

                    return Ok();
                }
            }

            return BadRequest(new
            {
                Errors = ModelState.ErrorCount > 0 ? ModelState.ToErrorCollection() : registrationResult.Errors.ToGeneric().ToErrorCollection()
            });
        }

        [AllowAnonymous]
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string code)
        {
            var result = await _userRepository.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return Redirect("http://localhost:8080/profile?confirmed=true");
            }
            return BadRequest(new { Errors = result.Errors.ToErrorCollection() });
        }
    }
}