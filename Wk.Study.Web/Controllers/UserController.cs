using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wk.Study.IService;
using Wk.Study.IService.Model;
using Wk.Study.Service.Services;

namespace Wk.Study.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController:ControllerBase
    {
        
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("AddUser")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserAsync([FromBody]UserAddModel model)
        {
            model.CreateTime = DateTime.Now;
            return Ok(await userService.AddUserAsync(model));
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAsync([FromQuery]int id)
        {
            var result = await userService.GetUserAsync(id);
            return Ok(result);
        }


        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel lm)
        {
            var result = await userService.LoginAsync(lm);
            return Ok(result);
        }


    }
}
