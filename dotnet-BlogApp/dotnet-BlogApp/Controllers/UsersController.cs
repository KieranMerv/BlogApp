﻿using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;
using dotnet_BlogApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_BlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Read: Login User
        [HttpPost]
        public async Task<ActionResult<ReturnAppUserVM>> LoginAppUser(LoginAppUserVM loginAppUserVM)
        {
            var loginAppUser = await _userManager.FindByEmailAsync(loginAppUserVM.Email);

            if (loginAppUser == null) return Unauthorized("User with this email does not exist.");

            var result = await _signInManager.CheckPasswordSignInAsync(loginAppUser, loginAppUserVM.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var newReturnUserVM = new ReturnAppUserVM
            {
                UserName = loginAppUser.UserName,
                Token = await _tokenService.CreateTokenAsync(loginAppUser),
                Alias = loginAppUser.Alias
            };

            return Ok(newReturnUserVM);
        }

        // Create: Register User
        [HttpPost]
        public async Task<ActionResult<ReturnAppUserVM>> CreateAppUser(RegisterAppUserVM registerUserVM)
        {
            if (await _userManager.FindByEmailAsync(registerUserVM.Email) != null) 
                return BadRequest("User with this email already exists. Please choose another email.");

            if (registerUserVM.Password != registerUserVM.ConfirmPassword)
                return BadRequest("Password and confirm password need to match.");

            var newAppUser = new AppUser
            {
                UserName = registerUserVM.UserName,
                Email = registerUserVM.Email,
                Alias = registerUserVM.Alias
            };

            var result = await _userManager.CreateAsync(newAppUser, registerUserVM.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation("User created a new account with password.");

            var newReturnUserVM = new ReturnAppUserVM
            {
                UserName = newAppUser.UserName,
                Token = await _tokenService.CreateTokenAsync(newAppUser),
                Alias = newAppUser.Alias
            };

            var roleResult = await _userManager.AddToRoleAsync(newAppUser, "normalAppUser");

            if (!roleResult.Succeeded) 
            {
                roleResult.Errors
                    .Append(new IdentityError 
                    { 
                        Code = "Custom", 
                        Description = "User was added but normalAppUser role could not be assigned." 
                    });
                
                return BadRequest(roleResult.Errors);
            }

            return Ok(newReturnUserVM);
        }

        // Update: Update User
        [HttpPut]
        public async Task<ActionResult<ReturnAppUserVM>> UpdateAppUser(UpdateAppUserVM updateAppUserVM)
        {
            var updateAppUser = await _userManager.FindByEmailAsync(updateAppUserVM.Email);

            if (updateAppUser == null) 
                return BadRequest("User with this email does not exist.");

            // Verify password
            if (await _userManager.CheckPasswordAsync(updateAppUser, updateAppUserVM.Password))
                return Unauthorized("Password incorrect, unable to update current user.");

            // Check and update individual fields
            if (updateAppUserVM.UserName != null)
                updateAppUser.UserName = updateAppUserVM.UserName;

            if (updateAppUserVM.NewPassword != null)
                await _userManager.ChangePasswordAsync(updateAppUser, updateAppUserVM.Password, updateAppUserVM.NewPassword);

            if (updateAppUserVM.NewEmail != null)
            {
                if (await _userManager.FindByEmailAsync(updateAppUserVM.NewEmail) != null)
                    return BadRequest("User with specified new email already exists.");

                var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(updateAppUser, updateAppUserVM.NewEmail);
                await _userManager.ChangeEmailAsync(updateAppUser, updateAppUserVM.NewEmail, changeEmailToken);
            }

            if (updateAppUserVM.Alias != null)
                updateAppUser.Alias = updateAppUserVM.Alias;

            var result = await _userManager.UpdateAsync(updateAppUser);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User updated");
        }

        // Delete: Delete User
        [HttpDelete]
        public async Task<ActionResult> DeleteAppUser([FromBody] DeleteAppUserVM deleteAppUserVM)
        {
            var deleteAppUser = await _userManager.FindByEmailAsync(deleteAppUserVM.Email);

            if (deleteAppUser == null)
                return Unauthorized("User with this email does not exist.");

            if (await _userManager.CheckPasswordAsync(deleteAppUser, deleteAppUserVM.Password))
                return Unauthorized("Password incorrect, unable to delete current user.");

            var result = await _userManager.DeleteAsync(deleteAppUser);

            if (!result.Succeeded) return BadRequest(result.Errors);

            _logger.LogInformation($"User with ID {deleteAppUser.Id} deleted themselves.");

            return Ok("User deleted.");
        }
    }
}
