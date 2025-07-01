using UserService.Data;
using UserService.Models;
using UserService.Repository.Abstract;

namespace UserService.Repository
{
    public class RepoUserService : IRepoUserService
    {
        private readonly UserDbContext _userDbContext;

        public RepoUserService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;              
        }

        public IEnumerable<User> GetPagedUsers(int page, int pageSize)
        {
            IEnumerable<User> pagedUsers = new List<User>();

            var users = _userDbContext.Users;

            pagedUsers = users.Skip(pageSize * (page - 1)).Take(pageSize);

            return pagedUsers;
        }

        public int TotalCount => _userDbContext.Users.Count();        
    }

}