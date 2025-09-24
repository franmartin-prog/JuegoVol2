namespace Library;

public interface IMagicCharacter : ICharacter<IItems>
{
    int MaxMagic { get;  }
    
    void AddMagicItem(IMagicItem item);
    void RemoveMagicItem(IMagicItem item);
}