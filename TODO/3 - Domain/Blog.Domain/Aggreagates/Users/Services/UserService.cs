using Todo.Domain.Aggreagates.Auth;
using Todo.Domain.Aggreagates.Users.Repository;
using Todo.Domain.Exceptions;
using Todo.Domain.Services;
using Todo.Domain.Util;
using MediatR;

namespace Todo.Domain.Aggreagates.Users.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IMediator mediator,
                             IUserRepository userRepository,
                             IAuthService authService
                             ) : base(mediator)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public User Insert(User user)
        {
            user.Password = HashUtil.ComputeSha256Hash(user.Password);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return user;
        }


        public User Update(int id, User user)
        {
            user.Id = id;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            _userRepository.Remove(GetById(id));
            _userRepository.SaveChanges();
            return true;
        }

        public User GetById(int id)
        {
            return _userRepository.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public string Signin(Login login)
        {
            login.Password = HashUtil.ComputeSha256Hash(login.Password);
            var user = _userRepository.Signin(login);
            if (user == null)
                throw new AuthenticationExcecption("Invalid username or password");

            return _authService.GenerateJwtToken(user);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _userRepository.Dispose();
        }


    }
}
