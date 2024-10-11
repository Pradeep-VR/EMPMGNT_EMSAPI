using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Authorization.Services;

/*******************************************  INTERFACE   *****************************************************/
public interface IRefreshService
{
    Task<string> GenerateToken(string UserName);
}

/************************************************  SERVICE   ************************************************/
public class RefreshService : IRefreshService
{
    private readonly ApplicationDBContext _DbContext;

    public RefreshService(ApplicationDBContext dBContext)
    {
        this._DbContext = dBContext;
    }

    public async Task<string> GenerateToken(string EmpName)
    {
        var RandomNo = new byte[32];
        using (var randomNoGenerator = RandomNumberGenerator.Create())
        {
            randomNoGenerator.GetBytes(RandomNo);
            string refreshToken = Convert.ToBase64String(RandomNo);

            var ExistToken = this._DbContext.Refreshtokens.FirstOrDefaultAsync(x => x.Empid == EmpName).Result;
            if (ExistToken != null)
            {
                ExistToken.Refreshtoken1 = refreshToken;
            }
            else
            {
                await this._DbContext.Refreshtokens.AddAsync(new Refreshtoken
                {
                    Empid = EmpName,
                    Tokenid = new Random().Next().ToString(),
                    Refreshtoken1 = refreshToken
                });
            }

            await this._DbContext.SaveChangesAsync();
            return refreshToken;
        }
    }
}
