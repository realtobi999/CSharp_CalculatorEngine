using System;

namespace Calculator.Core.Extensions;
public static class DictionaryExtensions
{
    public static TKey NextKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull
    {
        var keys = dictionary.Keys.ToList();
        int index = keys.IndexOf(key);
        
        if (index < 0 || index + 1 >= keys.Count)
        {
            throw new InvalidOperationException("The provided key does not have a next key.");
        }

        return keys[index + 1];
    }
}
