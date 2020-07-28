using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wk.Study.Library.ConfigModel;
//using Wk.Study.Model.Models;

namespace Wk.Study.Service.Services
{
    public class JwtService
    {
        private readonly JwtSetting jwtSetting;
        public JwtService(IOptions<JwtSetting> jwtSetting)
        {
            this.jwtSetting = jwtSetting.Value;
        }
        /*public string GetToken(SysUser user)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.UserId.ToString(), ClaimValueTypes.Integer32), // 用户id
                new Claim("name", user.UserName), // 用户名
                new Claim("admin", user.IsLimit.ToString(),ClaimValueTypes.Boolean) // 是否是管理员
            };

            var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)), SecurityAlgorithms.HmacSha256);

            //创建令牌
            var token = new JwtSecurityToken(
              issuer: jwtSetting.Issuer,
              audience: jwtSetting.Audience,
              signingCredentials: cred,
              claims: claims,
              //notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(jwtSetting.ExpireSeconds)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }*/
    }
}
