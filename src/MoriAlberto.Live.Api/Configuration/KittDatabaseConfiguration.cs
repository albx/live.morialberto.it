namespace MoriAlberto.Live.Api.Configuration;

public record KittDatabaseConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
}
