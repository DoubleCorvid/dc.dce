using System;

namespace DoubleCorvid.DungeonCrawlExtraction;

public class GameObject {
    public required Guid Id { get; init; }

    public Guid SubId { get; init; } = Guid.Empty;

    public string Name {
        get {
            if (string.IsNullOrEmpty (_name)) {
                _name = $"{Id}{(SubId == Guid.Empty ? "" : SubId)}";
            }

            return _name;
        }
        set => _name = value;
    }

    private string _name = "";
}