namespace Library.NuestrosCharacters;

public class Knight : ICharacter<IItems>
{
    public string Name { get; }
    public int MaxLife { get; }
    public int InitialAttack { get; }
    public int InitialDefense { get; }
    public int Life { get; set; }

    public Knight(string name, int maxLife, int initialAttack, int initialDefense)
    {
        Name = name;
        MaxLife = maxLife;
        InitialAttack = initialAttack;
        InitialDefense = initialDefense;
        Life = maxLife;
    }

    private List<IItems> KnightItems = new List<IItems>();

    public void AddItem(IItems item)
    {
        KnightItems.Add(item);
    }

    public List<IItems> RemoveItem()
    {
        throw new NotImplementedException();
    }

    public int GetAttack()
    {
        int attack = InitialAttack;
        foreach (var item in KnightItems)
        {
            attack += item.Attack; //???
        }

        return attack;;
    }

    public int GetDefense()
    {
        int defense = InitialDefense;
        foreach (var item in KnightItems)
        {
            defense += item.Defense; //??
        }

        return defense;
    }

    public int Heal()
    {
        foreach (var item in KnightItems)
        {
            Life += item.Healing;
        }
        // Tope por si se pasa de la vida máxima
        if (Life > MaxLife)
        {
            Life = MaxLife;
        }

        return Life;
    }

    public int ReceiveAttack(ICharacter<IItems> character)
    {
        return Life = Life + GetDefense() - character.GetAttack();
    }
}