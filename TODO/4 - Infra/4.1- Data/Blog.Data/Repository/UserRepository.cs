using Todo.Data.Context;
using Todo.Data.Core.Repository;
using Todo.Domain.Aggreagates.Users;
using Todo.Domain.Aggreagates.Users.Repository;


namespace Todo.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }

        public User Signin(Login login)
        {
            return _context.Users.Where(x => x.Login.Equals(login.Username) && x.Password.Equals(login.Password)).FirstOrDefault();
        }
    }
}
