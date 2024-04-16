using Microsoft.Datasync.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingApp.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationToken> GetAuthenticationToken();
    }
}
