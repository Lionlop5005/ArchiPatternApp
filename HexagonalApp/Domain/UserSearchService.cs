/// <summary>
/// HexagonalApp　サービス
/// </summary>
public class UserSearchService
{
    private readonly IUserSearchPort _port;
    public UserSearchService(IUserSearchPort port) => _port = port;

    public Task<IEnumerable<User>> Search(string name) => _port.SearchAsync(name);
}