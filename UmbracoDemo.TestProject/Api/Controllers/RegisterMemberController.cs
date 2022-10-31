using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Models;
using UmbracoDemo.TestProject.Api.Models;




using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace UmbracoDemo.TestProject.Api.Controllers
{
    public class RegisterMemberController : UmbracoApiController
    {
        public IConfiguration _configuration;
        private IMemberManager _memberManager;
        private IMemberService _memberService;
        IMemberSignInManager _memberSignInManager;
        private ILogger _logger;



        public RegisterMemberController(
            IMemberManager memberManager,
            IMemberService memberService,
            ILogger<RegisterMemberController> logger,
            IConfiguration configuration, IMemberSignInManager memberSignInManager)
        {
            _memberManager = memberManager;
            _memberService = memberService;
            _logger = logger;
            _configuration = configuration;
            _memberSignInManager = memberSignInManager;
        }


        [Authorize]
        [HttpGet("getTest")]
        [ProducesResponseType(typeof(IEnumerable<TestModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTest()
        {
            await _memberSignInManager.SignOutAsync();
            return Ok(new List<TestModel> { new TestModel(), new TestModel(), new TestModel() });

        }




        [HttpPut("registerMember")]
        public async Task<IActionResult> RegisterMember(RegisterMemberModel model)
        {
            if (await _memberManager.FindByEmailAsync(model.Email) != null) return Conflict($"Member with email {model.Email} already exists!");

            var identityMember = MemberIdentityUser.CreateNew(model.Email, model.Email, "member", true, model.FirstName + " " + model.LastName);

            var identyResult = await _memberManager.CreateAsync(identityMember, model.Password);

            if (identyResult.Succeeded)
            {
                await _memberManager.AddToRolesAsync(identityMember, new string[] { "User", "Admin" });
                var member = _memberService.GetByKey(identityMember.Key);
                _memberService.Save(member);
            }
            else
            {

                var errors = identyResult.Errors;
                var errorString = new StringBuilder();


                foreach (var error in errors)
                {
                    errorString.Append($"{error.Code}-{error.Description}");
                }

                _logger.LogError(errorString.ToString());

            }


            return Ok();

        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {

            var isValid = await _memberManager.ValidateCredentialsAsync(model.Username, model.Password);
            if (isValid)
            {
                var isSignedResult = await _memberSignInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    true,
                    false);

                if (isSignedResult.Succeeded)
                {
                    if (model?.Username != null && model?.Password != null)
                    {
                        var member = _memberService.GetByUsername(model.Username);
                        var groups = _memberService.GetAllRoles(member.Username);

                        var claims = new[]
                             {
                            new Claim(JwtRegisteredClaimNames.Sub, member.Name),
                            new Claim("fullName", member.Name.ToString()),

                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         };


                        if (groups.Contains(Policies.Admin)) new Claim("role", Policies.Admin);
                        if (groups.Contains(Policies.Admin)) new Claim("role", Policies.User);


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: signIn
                        );

                        var result = new
                        {
                            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                            User = new
                            {
                                DisplayName = member.Name,
                                Roles = groups,
                                Email = member.Email
                            }
                        };

                        return Ok(result);

                    }

                }
                else if (isSignedResult.IsLockedOut)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            return BadRequest();


        }



    }

}




