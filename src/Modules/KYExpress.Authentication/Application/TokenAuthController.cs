
using KYExpress.Authentication.Application.Dtos.TokenAuth;
using KYExpress.Authentication.Domain;
using KYExpress.Core;
using KYExpress.Core.Domain;
using KYExpress.Core.Domain.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KYExpress.Authentication.Application
{
    /// <summary>
    /// 授权服务
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenAuthController : ControllerBase
    {
        private readonly TokenAuthOptions _tokenAuthOptions = new TokenAuthOptions();
        private readonly IRepository<TB_PDAUserLogin> _userRepository;
        private readonly IQueryService _queryService;
        public TokenAuthController(
            IRepository<TB_PDAUserLogin> userRepository,
            IConfiguration configuration,IQueryService queryService)
        {
            _userRepository = userRepository;
            _queryService = queryService;
            _tokenAuthOptions.SecurityKey = configuration["Authentication:JwtBearer:SecurityKey"];
            _tokenAuthOptions.Issuer = configuration["Authentication:JwtBearer:Issuer"];
            _tokenAuthOptions.Audience = configuration["Authentication:JwtBearer:Audience"];
            _tokenAuthOptions.Expiration = TimeSpan.FromDays(1);
        }

       
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var loginUser = await GetLoginUser(
                model.UserID,
                model.UserPWD
            );
            var accessToken = CreateAccessToken(CreateClaims(loginUser));
            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                ExpireInSeconds = (int)_tokenAuthOptions.Expiration.TotalSeconds,
                UserId = loginUser.UserID
            };
        }

        [HttpPost]
        public async Task<dynamic> ServeProvideData([FromBody]AuthenticateModel model)
        {
            var listPara = new List<string>
            {
                model.UserID,
                model.UserPWD
            };
            var dynamicModel= (await _queryService.QueryFirstAsync<TB_PDAUserLogin>(GetSqlStr(),
                new { model.UserID, model.UserPWD }));
            var resultModel = new AuthenticateResultModel
            {
                UserId = dynamicModel.UserName
            };
            return dynamicModel;
        }
        
        private async Task<TB_PDAUserLogin> GetLoginUser(string userid, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(s => s.UserID == userid);
            if (user == null || !user.CheckPassword(password))
            {
                throw new CustomException( "用户名或密码错误");
            }
            return user;
        }


        private IEnumerable<Claim> CreateClaims(TB_PDAUserLogin user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString(), ClaimValueTypes.String, _tokenAuthOptions.Issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, _tokenAuthOptions.Issuer),
                new Claim("DisplayName", user.UserName, ClaimValueTypes.String, _tokenAuthOptions.Issuer),
            };
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenAuthOptions.Issuer,
                audience: _tokenAuthOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _tokenAuthOptions.Expiration),
                signingCredentials: _tokenAuthOptions.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private string GetSqlStr()
        {
            return @"Select ISNULL(M.IsCompany,'') AS IsCompany, M.UserID, M.UserName, T.IdentityName,M.Department, M.UserWorkshop , T.Workshop , T.CZDepart , isnull(M.PopdomSW,0) as PopdomSW ,isnull(M.PersonSW,0) as PersonSW, GETDATE() as ServerDT , M.SessionID , isnull(M.SessionDT,getdate()) as SessionDT, 
                                T.Telephone, T.ZhiChun,T.DP_110, V.Col_001 AS PointName, M.LoadingSW,
                                (CASE WHEN T.Telephone <> '' THEN T.Telephone WHEN T.TeleMobile <> '' THEN T.TeleMobile ELSE MI.M_MobileNo END ) AS UploadTel, M.IsFreeze
                                From dbo.TB_PDAUserLogin as M
                                Left Join dbo.TB_DeptPeople as T ON M.UserName = T.PeopleName
                                Left Join dbo.TB_DomainSJDCodeNEWGroup AS U ON T.CZDepart = U.Col_001
                                Left Join dbo.TB_DomainSJDCodeNEW AS  V ON V.sys_guid = U.lyd_guid
                                LEFT JOIN MobileInfo AS MI ON T.PeopleName = MI.M_User
                                Where M.UserID = @UserID and M.UserPWD = @UserPWD";

        }

    }
}
