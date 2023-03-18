using MoriAlberto.Live.ReadModel.Model;

namespace MoriAlberto.Live.ReadModel;

public interface IDatabase
{
    IQueryable<Streaming> Streamings { get; }
}
