using Library;

namespace Ucu.Poo.RoleplayGame;

public class Staff : IDefenseItem, IAttackItem
{
    public string Name { get; }
    public int AttackValue 
    {
        get
        {
            return 100;
        } 
    }

    public int DefenseValue
    {
        get
        {
            return 100;
        }
    }

    public Staff(string name)
    {
        Name = name;
    }
}
