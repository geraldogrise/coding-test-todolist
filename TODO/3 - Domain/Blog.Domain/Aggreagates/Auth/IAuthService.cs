using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Domain.Aggreagates.Auth
{
    public interface IAuthService
    {
        public string GenerateJwtToken(User user);
    }
}
