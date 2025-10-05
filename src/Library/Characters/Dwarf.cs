using Library;

namespace Ucu.Poo.RoleplayGame;

public class Dwarf : ICharacter<IItems>
{
    public string Name { get; }
    public int MaxLife { get; }
    public int InitialAttack { get; }
    public int InitialDefense { get; }
    public int Life { get; set; }

    public Dwarf(string name, int maxLife, int initialAttack, int initialDefense)
    {
        Name = name;
        MaxLife = maxLife;
        InitialAttack = initialAttack;
        InitialDefense = initialDefense;
        Life = maxLife;
    }

    private List<IItems> DwarfItem = new List<IItems>();

    public void AddItem(IItems item)
    {
        DwarfItem.Add(item);
    }

    public List<IItems> RemoveItem()
    {
        throw new NotImplementedException();
    }

    public int GetAttack()
    {
        int attack = InitialAttack;
        foreach (var item in DwarfItem)
        {
            attack += item.Attack; //???
        }

        return attack;;
    }

    public int GetDefense()
    {
        int defense = InitialDefense;
        foreach (var item in DwarfItem)
        {
            defense += item.Defense; //??
        }

        return defense;
    }

    public int Heal()
    {
        foreach (var item in DwarfItem)
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