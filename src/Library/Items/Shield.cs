using Library;

namespace Ucu.Poo.RoleplayGame;

public class Shield : IDefenseItem
{
    public string Name { get; }
    public int DefenseValue
    {
        get
        {
            return 14;
        }
    }
}
