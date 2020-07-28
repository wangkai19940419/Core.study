using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Wk.Study.Common.Helper;
using Wk.Study.Common.Model;
using Wk.Study.IService;
using Wk.Study.IService.Model;
using Wk.Study.Library.ConfigModel;
//using Wk.Study.Model.Models;

namespace Wk.Study.Service.Services
{
//    public class UserService : BaseService,IUserService
//    {
//        private readonly JwtService jwtService;
//        public UserService(IMapper mapper,
//           wkstudyContext wkstudyContext,JwtService jwtService)
//           : base(mapper, wkstudyContext)
//        {
//            base.mapper = mapper;
//            base.wkstudyContext = wkstudyContext;
//            this.jwtService = jwtService;
//        }
//
//        public async Task<bool> AddUserAsync(UserAddModel model)
//        {
//            var user = mapper.Map<SysUser>(model);
//            await wkstudyContext.SysUser.AddAsync(user);
//            await wkstudyContext.SaveChangesAsync();
//            return true;
//            
//        }
//
//        public async Task<UserViewModel> GetUserAsync(int id)
//        {
//            var user =await wkstudyContext.SysUser.FindAsync(id);
//            var model = mapper.Map<SysUser,UserViewModel>(user);
//            return model;
//        }
//
//        public async Task<TokenModel> LoginAsync(LoginModel lm)
//        {
//            using ( var trans= await wkstudyContext.Database.BeginTransactionAsync())
//            //using (var trans = new TransactionScope())
//            {
//                var user = await wkstudyContext.SysUser.AsNoTracking().FirstOrDefaultAsync(x => x.Email == lm.Email && x.Password == lm.Password);
//                if (user == null)
//                    throw new BizException("用户不存在");
//                var token = jwtService.GetToken(user);
//                trans.Commit();
//
//                return new TokenModel { Token = token };
//            }
//           
//        }
//    }
}
