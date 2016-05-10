using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(
        IRoleStore<IdentityRole> store,
        IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<IdentityRole>> logger,
        IHttpContextAccessor contextAccessor)
        : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
        }
    }
}
