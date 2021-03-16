using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Core.Models.Auth
{
   public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LasttName { get; set; }

    }
}
