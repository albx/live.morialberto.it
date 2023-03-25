namespace MoriAlberto.Live.Models;

public class StreamingsSearchParameters
{
    public string Query { get; set; } = string.Empty;

    public SortDirection Sort { get; set; } = SortDirection.Ascending;

    public int Page { get; set; } = 1;

    public enum SortDirection
    {
        Ascending,
        Descending,
    }
}
