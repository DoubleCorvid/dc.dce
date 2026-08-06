using System.Collections.Generic;
using System.Linq;

namespace DoubleCorvid.DungeonCrawlExtraction.Common.Collections;

public class WeightedList<T> where T : class {
    private readonly List<WeightedItem<T>> _items = [];

    public IReadOnlyList<WeightedItem<T>> Items => _items.AsReadOnly ();

    public int TotalWeight => _items.Last ().TotalWeight;

    public WeightedList () {
        _items.Add (new WeightedItem<T> {
            Previous = default,
            Item = default,
            Weight = 0,
        });
    }

    public WeightedList (Dictionary<T, int> items) : this () {
        AddItems (items);
    }

    public void AddItem (T item, int weight) {
        var weightedItem = new WeightedItem<T> {
            Previous = _items.Last (),
            Item = item,
            Weight = weight,
        };

        _items.Add (weightedItem);
    }

    public void AddItems (Dictionary<T, int> items) {
        foreach ((var item, var weight) in items) {
            AddItem (item, weight);
        }
    }

    public WeightedItem<T>? GetItem (int roll) {
        foreach (var item in _items) {
            if (roll <= item.TotalWeight) {
                return item;
            }
        }

        return null;
    }

    public void Clear () => _items.Clear ();
}
