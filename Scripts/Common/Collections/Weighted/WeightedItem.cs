namespace DoubleCorvid.DungeonCrawlExtraction.Common.Collections;

public class WeightedItem<T> {
    public required WeightedItem<T>? Previous { get; init; }

    public required T? Item { get; init; }

    public int Weight { get; init; }

    public int TotalWeight => Previous?.TotalWeight ?? 0 + Weight;
}