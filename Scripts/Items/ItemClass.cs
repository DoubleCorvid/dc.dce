using System.Collections.Generic;

namespace DoubleCorvid.DungeonCrawlExtraction.Items;

public class ItemClass : GameObject {
    public required string Description { get; init; }

    public int SortingRank { get; init; }

    public required IReadOnlyList<ItemType> Types { get; init; }
}