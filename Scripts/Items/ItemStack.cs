using System;

namespace DoubleCorvid.DungeonCrawlExtraction.Items;

public class ItemStack : GameObject {
    public required int Rank { get; set; }

    public BaseItem? Item {
        get => _item;
        init => _item = value;
    }

    public BaseItem? _item = null;

    public int Count {
        get => _count;
        init => _count = value;
    }

    private int _count = 0;

    public int MaxCount { get; init; } = 1;

    public bool IsEmpty => Item is null || Count <= 0;

    public int UpdateCount(int count) {
        var prev = _count;

        _count = Math.Max(0, Math.Min(count, MaxCount));

        return prev;
    }

    public (BaseItem? item, int Count) UpdateItem (BaseItem? item, int count = 1) {
        var prev = (_item, _count);

        _item = item;

        UpdateCount (count);

        return prev;
    }

    public (BaseItem? item, int Count) Clear () => UpdateItem (null, 0);

    public ItemStack Copy () => new () {
        Id = Id,
        Rank = Rank,
        Name = Name,
        Count = _count,
        MaxCount = MaxCount,
        Item = _item,
    };

    public ItemStack CopyWithUpdates (Guid? id = null, int? rank = null, string? name = null, int? count = null, BaseItem? item = null, int? maxCount = null) => new () {
        Id = id ?? Id,
        Rank = rank ?? Rank,
        Name = name ?? Name,
        Count = count ?? _count,
        MaxCount = maxCount ?? MaxCount,
        Item = item ?? _item,
    };
}
