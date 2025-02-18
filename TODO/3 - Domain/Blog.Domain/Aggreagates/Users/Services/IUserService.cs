using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Aggreagates.Users.Services
{
    public interface IUserService : IDisposable
    {
       public User Insert(User user);

       public User Update(int id, User user);

       public bool Delete(int id);

        public User GetById(int id);

        public string Signin(Login login);

        public List<User> GetAll();
    }
}
