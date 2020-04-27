using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wk.Study.IService.Model;
using Wk.Study.Library.ConfigModel;

namespace Wk.Study.IService
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(UserAddModel model);

        Task<UserViewModel> GetUserAsync(int id);

        Task<TokenModel> LoginAsync(LoginModel lm);
    }
}
