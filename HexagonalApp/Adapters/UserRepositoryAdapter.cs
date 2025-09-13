using Microsoft.EntityFrameworkCore;

public class UserRepositoryAdapter : IUserSearchPort
{
    private readonly AppDbContext _context;
    public UserRepositoryAdapter(AppDbContext context) => _context = context;

    public async Task<IEnumerable<User>> SearchAsync(string namePart)
    {
        var list = await _context.users
                                 .Where(u => u.Name.Contains(namePart))
                                 .ToListAsync();
        return list; // List<User> ‚Í IEnumerable<User> ‚ÉˆÃ–Ù•ÏŠ·‰Â”\
    }
}