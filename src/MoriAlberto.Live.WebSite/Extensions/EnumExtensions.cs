using System.ComponentModel;
using System.Reflection;

namespace MoriAlberto.Live.WebSite;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        string _string = value.ToString();

        FieldInfo fi = value.GetType().GetField(_string)!;
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Any() ? attributes[0].Description : _string;
    }
}
