using Library;

namespace Ucu.Poo.RoleplayGame;

public class Axe : IAttackItem
{
    public string Name { get; }
    public int AttackValue 
    {
        get
        {
            return 25;
        } 
    }
}
