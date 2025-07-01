using UserService.Models;

namespace UserService.Repository
{
    public class FakeUserService
    {
        private List<User> _users;
        public FakeUserService()
        {
            _users = Enumerable.Range(1, 30).Select(i => new User
            {
                Id = i,
                FirstName = "First" + i.ToString(),
                LastName = "Last" + i.ToString(),
                Email = "user" + i.ToString() + "@demo.com"
            }).ToList();
        }

        public IEnumerable<User> GetPagedUsers(int page, int pageSize) 
        {
            IEnumerable<User> pagedUsers = new List<User>();
            pagedUsers = _users.Skip(pageSize * (page - 1)).Take(pageSize);

            return pagedUsers;
        }

        public int TotalCount => _users.Count;
    }
}
