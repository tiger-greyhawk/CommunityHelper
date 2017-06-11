using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.DAO
{
    public class UserDao
    {
        private readonly IService _restClient;
        private readonly ConverterJson _converterJson;

        public UserDao(IService restClient, ConverterJson converterJson)
        {
            _restClient = restClient;
            _converterJson = converterJson;
        }

        public User GetUser()
        {
            return _converterJson.ConvertJsonToUser(_restClient.CreateRequest().DoGetAsync("user/me/"));
        }

        public Player SaveUser(User user)
        {
            return
                _converterJson.ConvertJsonToPlayer(
                    _restClient.CreateRequest().RegisterNewUserAsync(_converterJson.ConvertUserToJson(user), "user/register/"));
        }
    }
}