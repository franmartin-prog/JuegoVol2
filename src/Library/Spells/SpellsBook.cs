using Library;

namespace Ucu.Poo.RoleplayGame;

public class SpellsBook
{
    public static List<ISpells> spells = new();
    public void AddSpell(ISpells spell)
    {
        if (!spells.Contains(spell))
        {
            spells.Add(spell);
        }
    }
    public void RemoveSpell(ISpells spell)
    {
        if (spells.Contains(spell))
        {
            spells.Remove(spell);
        }
    }
}
