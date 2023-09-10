namespace ConstraintSolverTest;

public class DualKeyMap<TKey1, TKey2, TValue> where TKey1 : notnull where TKey2 : notnull
{
    private readonly Dictionary<TKey1, Dictionary<TKey2, TValue>> _data = new();

    public void Add(TKey1 key1, TKey2 key2, TValue value)
    {
        if (!_data.ContainsKey(key1)) _data[key1] = new Dictionary<TKey2, TValue>();

        _data[key1][key2] = value;
    }

    public IEnumerable<TValue> GetVariations(TKey1 key1)
    {
        return _data.TryGetValue(key1, out var value) ?
            // Assuming your data structure has TValue as the value type.
            value.Values : Enumerable.Empty<TValue>(); // Return an empty collection if the key is not found.
    }

    public TValue? GetSingleResult(TKey1 key1, TKey2 key2)
    {
        if (_data.ContainsKey(key1) && _data[key1].ContainsKey(key2)) return _data[key1][key2];

        return default; // Return a default value or throw an exception as needed.
    }
    
    public List<TValue> GetValues()
    {
        return _data.Values.SelectMany(x => x.Values).ToList();
    }
}