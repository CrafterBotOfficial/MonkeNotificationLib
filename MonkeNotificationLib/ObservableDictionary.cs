using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MonkeNotificationLib;

internal class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    private void RaiseCollectionChanged()
    {
        try
        {
            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        catch (Exception ex)
        {
            Main.Log(ex.ToString(), BepInEx.Logging.LogLevel.Error);
        }
    }

    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        RaiseCollectionChanged();
    }

    public void Insert(TKey key, TValue value)
    {
        if (ContainsKey(key)) return;
        var newDict = new Dictionary<TKey, TValue>()
        {
            { key, value },
        };
        foreach (var pair in this)
        {
            newDict.Add(pair.Key, pair.Value);
        }
        Clear();
        foreach (var pair in newDict)
        {
            Add(pair.Key, pair.Value);
        }
        RaiseCollectionChanged();
    }

    public new bool Remove(TKey key)
    {
        bool result = base.Remove(key);
        if (result) RaiseCollectionChanged();
        return result;
    }
}
