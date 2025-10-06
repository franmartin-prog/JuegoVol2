using Library;

namespace Ucu.Poo.RoleplayGame;

public class DefenseSpell : ISpells
{
    public string Name { get; }
    public int Cost { get; }
    public int Defense { get; }

    public DefenseSpell(string name, int cost, int defense)
    {
        Name = name;
        Cost = cost;
        Defense = defense;
    }
}

