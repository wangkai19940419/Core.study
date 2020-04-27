using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Wk.Study.IService.Model;
using Wk.Study.Model.Models;


namespace Wk.Study.Service.ProfileMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAddModel, SysUser>();
            CreateMap<SysUser, UserViewModel>();
        }
    }
}
