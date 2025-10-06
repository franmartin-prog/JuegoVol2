using Library;

namespace Ucu.Poo.RoleplayGame;

public class Spell : ISpells
{
    public string Name { get; }
    public int Cost { get; }

    public Spell(string name, int cost, int attackValue, int defenseValue)
    {
        Name = name;
        Cost = cost;
        DefenseValue = defenseValue;
        AttackValue = attackValue;
    }
    public int AttackValue { get; }

    public int DefenseValue { get; }
}

