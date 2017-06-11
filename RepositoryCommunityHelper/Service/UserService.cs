using RepositoryCommunityHelper.DAO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class UserService
    {
        private readonly UserDao _userDao;

        public UserService(UserDao userDao)
        {
            _userDao = userDao;
        }

        public User GetUser()
        {
            return _userDao.GetUser();
        }

        public Player SaveUser(User user)
        {
            return _userDao.SaveUser(user);
        }
    }
}