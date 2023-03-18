using Microsoft.EntityFrameworkCore;
using MoriAlberto.Live.ReadModel.Model;
using MoriAlberto.Live.ReadModel.Persistence;

namespace MoriAlberto.Live.ReadModel;

public class Database : IDatabase
{
    private readonly LiveDbContext _context;

    public Database(LiveDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<Streaming> Streamings => _context.Streamings.AsNoTracking();
}
