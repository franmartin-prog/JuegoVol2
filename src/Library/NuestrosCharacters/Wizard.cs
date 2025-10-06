using System.Runtime.Serialization;
using Library;
using Ucu.Poo.RoleplayGame;

public class Wizard : IMagicCharacter
{
    public string Name { get; }

    public int MaxLife
    {
        get { return 50; }
    }

    public int InitialAttack
    {
        get { return 50; }
    }

    public int InitialDefense
    {
        get { return 5; }
    }

    public int Life { get; set; }

    public Wizard(string name)

{
        Name = name;
        Life = MaxLife;
        Magic = MaxMagic;
}

    //ATAQUE
    public List<IAttackItem> AttackItems = new List<IAttackItem>();
    private List<ISpells> spells = new List<ISpells>();

    public void ReadGrimoire()
    {
        if (Magic >= 10)
            {
            int contador = 1;
            foreach (var spell in SpellsBook.spells)
            {
                if (contador == 1)
                {
                    if (!spells.Contains(spell))
                    {
                        spells.Add(spell);
                        contador -= 1;
                    }
                    
                }
            }
        }
    }

    public int GetAttackCost()
    {
        int cost = 0;
        foreach (IAttackSpell attack in spells)
        {
            cost += attack.Cost;
        }

        return cost;
    }
    
    private int GetDefenseCost()
    {
        int cost = 0;
        foreach (IDefenseSpell attack in spells)
        {
            cost += attack.Cost;
        }

        return cost;
    }

    private int GetHealCost()
    {
        int cost = 0;
        foreach (IHealingSpell attack in spells)
        {
            cost += attack.Cost;
        }

        return cost;
    }

    public int GetAttack()
    {
        int attack = InitialAttack;
        if (Life > 0)
        {
            foreach (var item in AttackItems)
            {
                attack += item.AttackValue;
            }
            if (Magic >= GetAttackCost())
            {
                foreach (IAttackSpell spell in spells)
                {
                    attack += spell.Attack;
                }

                Magic -= GetAttackCost();
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

            if (Magic >= GetDefenseCost())
            {
                foreach (IDefenseSpell spell in spells)
                {
                    defense += spell.Defense;
                    
                }

                Magic -= GetDefenseCost();
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
            foreach (var item in HealingItems)
            {
                Life += item.HealingValue;
            }
            
            if (Magic >= GetHealCost())
            {
                foreach (IHealingSpell spell in spells)
                {
                    Life += spell.Healing;
                    
                }

                Magic -= GetHealCost();
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

    public int MaxMagic { get; }
    public int Magic { get; set; }
    
}