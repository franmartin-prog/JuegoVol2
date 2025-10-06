using Library;

namespace Ucu.Poo.RoleplayGame;

public class Helmet : IDefenseItem
{
    public string Name { get; }
    public int DefenseValue
    {
        get
        {
            return 18;
        }
    }

    public Helmet(string name)
    {
        Name = name;
    }
}
