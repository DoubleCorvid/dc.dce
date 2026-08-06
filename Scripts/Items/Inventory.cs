using System.Collections.Generic;
using System.Linq;

namespace DoubleCorvid.DungeonCrawlExtraction.Items;

public class Inventory : GameObject {
    public IReadOnlyDictionary<InventoryCategory, InventoryCategoryCollection> Categories  {
        get => _categories;
        init => _categories = value.ToDictionary ();
    }

    private Dictionary<InventoryCategory, InventoryCategoryCollection> _categories = [];

    public InventoryCategoryCollection this [InventoryCategory category] => _categories [category]; 

    public int UpdateStackCount (InventoryCategory category, int index, int count) => this [category][index].UpdateCount (count);

    public (BaseItem? item, int Count) UpdateStackItem (InventoryCategory category, int index, BaseItem item, int count) => _categories [category][index].UpdateItem (item, count);

    public (BaseItem? item, int Count) ClearStack (InventoryCategory category, int index) => this [category][index].Clear ();
}
