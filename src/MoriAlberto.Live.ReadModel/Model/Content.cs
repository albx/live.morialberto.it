namespace MoriAlberto.Live.ReadModel.Model;

public abstract class Content
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? Abstract { get; init; } = string.Empty;

    public virtual SeoData Seo { get; init; } = new();

    #region Inner class
    public record SeoData
    {
        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string Keywords { get; init; } = string.Empty;
    }
    #endregion
}
