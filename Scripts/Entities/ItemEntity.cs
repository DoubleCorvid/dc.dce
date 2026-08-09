using DoubleCorvid.DungeonCrawlExtraction.Items;
using Godot;

namespace DoubleCorvid.DungeonCrawlExtraction.Entities;

public partial class ItemEntity : Node3D, IPickupableEntity<BaseItem> {
    public BaseItem? Entity { get; set; }

    public bool CanBePickedUp { get; set; } = true;

    public BaseItem? Pickup () {
        var temp = Entity;

        QueueFree ();

        return temp;
    }
}