namespace MoriAlberto.Live.WebSite.Services;

public static class PageCursorProvider
{
    private static Dictionary<int, string?> _cursors = new()
    {
        [0] = null
    };

    public static string? GetCursor(int pageIndex)
    {
        return _cursors[pageIndex];
    }

    public static void AddCursor(int pageIndex, string? cursor)
    {
        if (_cursors.ContainsKey(pageIndex))
        {
            _cursors[pageIndex] = cursor;
        }
        else
        {
            _cursors.Add(pageIndex, cursor);
        }
        
    }

    public static void RemoveCursor(int pageIndex)
    {
        _cursors.Remove(pageIndex);
    }
}
