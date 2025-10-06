using Library;

namespace Ucu.Poo.RoleplayGame;

public class Bow : IAttackItem
{
    public string Name { get; }
    public int AttackValue 
    {
        get
        {
            return 15;
        } 
    }

    public Bow(string name)
    {
        Name = name;
    }
}
