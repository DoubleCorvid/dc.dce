using System;

namespace DoubleCorvid.DungeonCrawlExtraction.Common;

public class Rarity {
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    
    public required string ColorHex { get; init; }

    public required float DefaultWeight { get; init; }
}