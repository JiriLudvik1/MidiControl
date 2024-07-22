namespace MidiControl;

public class HashSetComparer : IEqualityComparer<HashSet<int>>
{
    public bool Equals(HashSet<int>? x, HashSet<int>? y)
    {
        if (x is null && y is null)
        {
            return true;
        }
        
        if (x is null || y is null)
        {
            return false;
        }
        
        return x.SetEquals(y);
    }

    public int GetHashCode(HashSet<int> obj)
    {
        if (obj is null)
            return 0;

        int hash = 0;
        foreach (var item in obj)
        {
            hash ^= item.GetHashCode();
        }
        return hash;
    }
}