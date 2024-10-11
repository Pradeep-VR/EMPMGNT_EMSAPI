using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TEAMLIFTSS.Authorization.Services;
using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos;

namespace TEAMLIFTSS.Authorization;

[Route("api/[controller]")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    private readonly ApplicationDBContext _DbContext;
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshService refreshService;

    public AuthorizeController(ApplicationDBContext dBContext, IOptions<JwtSettings> jwtSett, IRefreshService refreshService)
    {
        this._DbContext = dBContext;
        this._jwtSettings = jwtSett.Value;
        this.refreshService = refreshService;
    }

    [HttpPost("GenerateToken")]
    public async Task<IActionResult> GenerateToken([FromBody] EmpDtlsCreate empDtls)
    {
        try
        {
            if (empDtls == null)
            {
                return Unauthorized(new
                {
                    error = "Employee Details is Null",
                    data = empDtls
                });
            }
            else
            {
                var Employe = await this._DbContext.Employeedetails.Where(x => x.Empid == empDtls.Empid && x.Secretcode == empDtls.Secretcode && x.Active == true
                                                                && x.Empviolation == false && x.Deviceviolation < 3).FirstOrDefaultAsync();
                if (Employe != null)
                {
                    //Generate Token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(this._jwtSettings.JwtSecurityKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, Employe.Empid),
                        new Claim(ClaimTypes.Role, Employe.Emprole)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                    };


                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    return Ok(new TokenResponse()
                    {
                        Token = jwtToken,
                        RefreshToken = await this.refreshService.GenerateToken(empDtls.Empid)
                    });

                }
                else
                {
                    return Unauthorized(new
                    {
                        error = "Employee Details Not Found.Please Contact Admin.",
                        data = empDtls
                    });
                }
            }
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                exeption = ex.Message,
            });
        }
    }


    [HttpPost("GenerateRefreshToken")]

    public async Task<IActionResult> GenerateRefToken([FromBody] TokenResponse token)
    {
        var RefToken = await this._DbContext.Refreshtokens.FirstOrDefaultAsync(item => item.Refreshtoken1 == token.RefreshToken);
        if (RefToken != null)
        {
            //Generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(this._jwtSettings.JwtSecurityKey);


            SecurityToken securitytoken;
            var principal = tokenHandler.ValidateToken(token.Token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out securitytoken);

            var _token = securitytoken as JwtSecurityToken;
            if (_token != null && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                string EmpName = principal.Identity?.Name == null ? "" : principal.Identity?.Name;
                var _existData = await this._DbContext.Refreshtokens.FirstOrDefaultAsync(item => item.Empid == EmpName && item.Refreshtoken1 == token.RefreshToken);
                if (_existData != null)
                {

                    var _newtoken = new JwtSecurityToken(
                        claims: principal.Claims.ToArray(),
                        expires: DateTime.Now.AddMinutes(3),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.JwtSecurityKey)),
                        SecurityAlgorithms.HmacSha256)
                        );
                    var JwtToken = tokenHandler.WriteToken(_newtoken);
                    return Ok(new TokenResponse()
                    {
                        Token = JwtToken,
                        RefreshToken = await refreshService.GenerateToken(EmpName)
                    });

                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }
        else
        {
            return Unauthorized();
        }

    }
}
