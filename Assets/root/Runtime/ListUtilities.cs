using System.Collections.Generic;

public static class ListUtilities
{
    public static int AddWithIndex<T>(this List<T> list, T value)
    {
        list.Add(value);
        return list.Count - 1;
    }
}