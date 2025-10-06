using Library;

namespace Ucu.Poo.RoleplayGame;

public class Shield : IDefenseItem
{
    public string Name { get; }
    public int DefenseValue
    {
        get
        {
            return 15;
        }
    }

    public Shield(string name)
    {
        Name = name;
    }
}
