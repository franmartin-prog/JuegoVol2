using Library;

namespace Ucu.Poo.RoleplayGame;

public class Armor : IDefenseItem
{
    public string Name { get; }
    public int DefenseValue
    {
        get
        {
            return 25;
        }
    }

    public Armor(string name)
    {
        Name = name;
    }
}
