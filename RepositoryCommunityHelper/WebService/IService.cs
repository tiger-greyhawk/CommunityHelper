﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryCommunityHelper.WebService
{
    public interface IService : IDisposable
    {
        ConnectionProperties Create();
        RestClient CreateRequest();
    }
}
