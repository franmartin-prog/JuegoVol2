namespace Library;

public interface ICharacter<T> where T : IItems
{
    public string Name { get;  }
    int MaxLife { get;  }
    int InitialAttack { get;  }
    int InitialDefense { get;  }

    void AddItem(T item);
    List<T> RemoveItem();

    int GetAttack();
    int GetDefense();
    int Heal();
    int ReceiveAttack(ICharacter<T> character);
}