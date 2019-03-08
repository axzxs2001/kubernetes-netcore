using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcore001.Respostory
{
    public interface IUserRepository
    {
        List<User> GetUsers();
    }
}
