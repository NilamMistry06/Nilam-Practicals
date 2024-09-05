namespace UserService
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public interface IUserRepository
    {
        public User GetUserById(int id);
        public void AddUser(User user);
        IEnumerable<User> GetAllUsers();
    }
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }

        public void CreateUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}