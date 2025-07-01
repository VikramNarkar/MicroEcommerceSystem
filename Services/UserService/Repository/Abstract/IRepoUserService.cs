using UserService.Models;

namespace UserService.Repository.Abstract
{
    public interface IRepoUserService
    {
        public IEnumerable<User> GetPagedUsers(int page, int pageSize);

        public int TotalCount { get; }
    }
}
