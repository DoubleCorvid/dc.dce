namespace DoubleCorvid.DungeonCrawlExtraction.Entities;

public interface IPickupableEntity<T> where T : GameObject {
    T? Entity { get; }

    bool CanBePickedUp { get; }

    T? Pickup ();
}
