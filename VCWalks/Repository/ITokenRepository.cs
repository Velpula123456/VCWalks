using Microsoft.AspNetCore.Identity;

namespace VCWalks.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
