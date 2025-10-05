using Library;

namespace Ucu.Poo.RoleplayGame;

public class Sword : IAttackItem, IItems
{
    public string Name { get; }
    public int AttackValue
    {
        get
        {
            return 20;
        } 
    }
}
