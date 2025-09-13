/// <summary>
/// OnionApp　サービス
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;

    public Task<IEnumerable<User>> SearchAsync(string namePart) => _repo.SearchByNameAsync(namePart);
}