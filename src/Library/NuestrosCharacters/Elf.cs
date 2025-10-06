namespace Library.NuestrosCharacters;

public class Elf : ICharacter<IItems> 
{
    public string Name { get; }
    public int MaxLife
    {
        get { return 100; }
    }
    public int InitialAttack
    {
        get { return 20; }
    }
    public int InitialDefense
    {
        get { return 25; }
    }
    public int Life { get; set; }

    public Elf(string name)
    {
        Name = name;
        Life = MaxLife;
    }

    //ATAQUE
    private List<IAttackItem> AttackItems = new List<IAttackItem>();
    public int GetAttack()
    {
        int attack = InitialAttack;
        if (Life > 0)
        {
            foreach (var item in AttackItems)
            {
                attack += item.AttackValue;
            }
        }
        else
            attack = 0;
        return attack;
    }
   
    //DEFENSA
    private List<IDefenseItem> DefenseItem = new List<IDefenseItem>();
    public int GetDefense()
    {
        int defense = InitialDefense;
        if (Life > 0)
        {
            foreach (var item in DefenseItem)
            {
                defense += item.DefenseValue;
            }

        }
        else
            defense = 0;
        return defense;
    }

    //HEALING
    private List<IHealingItem> HealingItems = new List<IHealingItem>();
    public int Heal()
    {
        if (Life > 0)
        {
            Life += MaxLife / 2;
            foreach (var item in HealingItems)
            {
                Life += item.HealingValue;
            }
            // Tope por si se pasa de la vida máxima
            if (Life > MaxLife)
            {
                Life = MaxLife;
            } 
        }

        else
        {
            Life = 0;
        }
        return Life;
    }

    //Agregar y borrar items
    public void AddItem(IItems item)
    {
        if (item is IAttackItem attack)
            AttackItems.Add(attack);
        else if (item is IDefenseItem defense)
            DefenseItem.Add(defense);
        else if (item is IHealingItem heal)
            HealingItems.Add(heal);
    }

    public List<IItems> RemoveItem(IItems item)
    {
        if (item is IAttackItem attack)
            AttackItems.Remove(attack);
        else if (item is IDefenseItem defense)
            DefenseItem.Remove(defense);
        else if (item is IHealingItem heal)
            HealingItems.Remove(heal);
        return null;
    }

    public int ReceiveAttack(ICharacter<IItems> character)
    {
        return Life = Life + GetDefense() - character.GetAttack();
    }
}