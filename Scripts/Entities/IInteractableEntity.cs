namespace DoubleCorvid.DungeonCrawlExtraction.Entities;

public interface IInteractableEntity {
    bool CanBeInteractedWith { get; }
    
    void Interact ();
}

