using Library;

namespace Ucu.Poo.RoleplayGame;

public class Vandage : IHealingItem
{
    public string Name { get; }
    public int HealingValue { get
    {
        return 20;
    }}

    public Vandage(string name)
    {
        Name = name;
    }
}