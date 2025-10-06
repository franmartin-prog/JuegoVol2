namespace Ucu.Poo.RoleplayGame;

public class AttackSpell
{
    public string Name { get; }
    public int Cost { get; }
    public int Attack { get; }

    public AttackSpell(string name, int cost, int attack)
    {
        Name = name;
        Cost = cost;
        Attack = attack;
    }
}