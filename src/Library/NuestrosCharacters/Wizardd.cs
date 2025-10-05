namespace Library.NuestrosCharacters;

public class Wizardd : IMagicCharacter
{
    public string Name { get; }
    public int MaxLife { get; }
    public int InitialAttack { get; }
    public int InitialDefense { get; }
    public int Life { get; set; }
    public int MaxMagic { get; }

    public Wizardd(string name, int maxLife, int initialAttack, int initialDefense, int maxMagic)
    {
        Name = name;
        MaxLife = maxLife;
        InitialAttack = initialAttack;
        InitialDefense = initialDefense;
        MaxMagic = maxMagic;
        Life = MaxLife;
    }

    private List<IItems> WizardItems = new List<IItems>();
    private List<ISpells> WizarddSpells = new List<ISpells>();
    public void AddItem(IItems item)
    {
        WizardItems.Add(item);
    }

    public List<IItems> RemoveItem() // como funciona?
    {
        throw new NotImplementedException();
    }

    public int GetAttack()
    {
        int attack = InitialAttack;
        foreach (var item in WizardItems)
        {
            attack += item.Attack; //???
        }

        return attack;
    }

    public int GetDefense()
    {
        int defense = InitialDefense;
        foreach (var item in WizardItems)
        {
            defense += item.Defense; //??
        }

        return defense;
    }

    public int Heal()
    {
        foreach (var item in WizardItems)
        {
            Life += item.Healing;
        }

        foreach (var VARIABLE in COLLECTION)
        {
            
        }
        // Tope por si se pasa de la vida máxima
        if (Life > MaxLife)
        {
            Life = MaxLife;
        }

        return Life;
    }

    public int ReceiveAttack(ICharacter<IItems> character)
    {
        return Life = Life + GetDefense() - character.GetAttack();
    }
    
    public void AddMagicItem(IMagicItem item)
    {
        WizardItems.Add(item);
    }

    public void RemoveMagicItem(IMagicItem item)
    {
        WizardItems.Remove(item);
    }
}