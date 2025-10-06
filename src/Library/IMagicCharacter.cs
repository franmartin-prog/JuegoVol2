namespace Library;

public interface IMagicCharacter : ICharacter<IItems>
{
    int MaxMagic { get;  }
    int Magic { get; set; }
    
}