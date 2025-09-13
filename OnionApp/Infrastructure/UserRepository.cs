using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<User>> SearchByNameAsync(string namePart)
        => await _context.users.Where(u => u.Name.Contains(namePart)).ToListAsync();
}