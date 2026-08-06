using System.Collections.Generic;
using DoubleCorvid.DungeonCrawlExtraction.Common;

namespace DoubleCorvid.DungeonCrawlExtraction.Items;

public class BaseItem : GameObject {
    public required string Description { get; init; }

    public required Rarity Rarity { get; init; }

    public bool Sellable { get; init; }

    public required float BuyValue { get; init; }

    public required float SellValue { get; init; }

    public required ItemClass Class {get; init; }

    public required IReadOnlyList<ItemType> Types { get; init; }

    public bool Stackable { get; init; } = false;

    public float MaxStack { get; init; } = 1f;
}
