public interface IUserSearchPort
{
    Task<IEnumerable<User>> SearchAsync(string namePart);
}