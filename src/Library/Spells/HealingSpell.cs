namespace Ucu.Poo.RoleplayGame;

public class HealingSpell
{
    public string Name { get; }
    public int Cost { get; }
    public int Healing { get; }

    public HealingSpell(string name, int cost, int healing)
    {
        Name = name;
        Cost = cost;
        Healing = healing;
    }
}