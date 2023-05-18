using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "F1A3C5E7-9D11-4BFE-8F4D-9E5C1D6A7B8C";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        public readonly List<UserAccount> _userAcctouns;

        public JwtTokenHandler()
        {
            _userAcctouns = new List<UserAccount>
            {
                new UserAccount{UserName="admin",Password="admin",Role="Admin"},
                new UserAccount{UserName="user",Password="user",Role="User"}
            };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            var user = _userAcctouns.SingleOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);
            if (user == null)
            {
                return null;
            }
            var tokenExpirationTime = System.DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new[] {
                 new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                 });

            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpirationTime,
                SigningCredentials = signinCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName = user.UserName,
                Role = user.Role,
                Token = token,
                TokenExpirationTime = tokenExpirationTime
            };

        }
    }
}
