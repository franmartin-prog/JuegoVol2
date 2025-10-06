using Library;

namespace Ucu.Poo.RoleplayGame;

public class SpellsBook : IMagicItem
{
    public string Name { get; }
    public int MagicCost { get; }
    public Spell[] Spells { get; set; }
    
    public int AttackValue
    {
        get
        {
            int value = 0;
            foreach (Spell spell in this.Spells)
            {
                value += spell.AttackValue;
            }
            return value;
        }
    }

    public int DefenseValue
    {
        get
        {
            int value = 0;
            foreach (Spell spell in this.Spells)
            {
                value += spell.DefenseValue;
            }
            return value;
        }
    }

    public SpellsBook(string name, int magicCost, Spell[] spells)
    {
        Name = name;
        MagicCost = magicCost;
        Spells = spells;
    }
}
