using System.Collections.Generic;

namespace DoubleCorvid.DungeonCrawlExtraction.Items;

public class InventoryCategoryCollection {
    public required InventoryCategory Category { get; init; }

    public IReadOnlyList<ItemStack> Items {
        get => _items;
        init => _items = [.. value];
    }

    public List<ItemStack> _items = [];

    public ItemStack this [int index] => _items [index];

    public float MaxStack { get; init; } = 1f;

    public int UpdateStackCount (int index, int count) => _items [index].UpdateCount (count);

    public (BaseItem? item, int Count) UpdateStackItem (int index, BaseItem item, int count) => this [index].UpdateItem (item, count);

    public (BaseItem? item, int Count) ClearStack (int index) => this [index].Clear ();

}
