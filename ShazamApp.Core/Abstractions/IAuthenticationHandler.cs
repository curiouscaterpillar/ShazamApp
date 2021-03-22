using ShazamApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShazamApp.Core.Abstractions
{
    public interface IAuthenticationHandler
    {
        Task<bool> AuthenticateAsync(AuthenticationRequest authRequest);
    }
}
