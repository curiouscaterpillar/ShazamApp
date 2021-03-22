using System;
using System.Collections.Generic;
using System.Text;

namespace ShazamApp.Core.Models
{
    public class AuthenticationRequest
    {
        public string Code { get; set; }

        public string Secret { get; set; }
    }
}
