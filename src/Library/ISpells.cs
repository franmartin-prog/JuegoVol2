namespace Library;

public interface ISpells
{
    public string Name { get;  }
    int Cost { get;  } // para leer un spell tenes que "perder" / "gastar" algo
    
}