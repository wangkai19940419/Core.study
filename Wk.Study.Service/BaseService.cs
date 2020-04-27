﻿using AutoMapper;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Wk.Study.Model.Models;

namespace Wk.Study.Service
{
    public class BaseService
    {
        public IMapper mapper;
        public wkstudyContext wkstudyContext;

        public BaseService(IMapper mapper, wkstudyContext wkstudyContext)
        {
            this.mapper = mapper;
            this.wkstudyContext = wkstudyContext;
        }
    }
}
