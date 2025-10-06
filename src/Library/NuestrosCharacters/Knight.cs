
    public List<IItems> RemoveItem()
    {
        throw new NotImplementedException();
    }

    public int GetAttack()
    {
        int attack = InitialAttack;
        foreach (var item in KnightItems)
        {
            attack += item.AttackValue; //???
        }

        return attack;;
    }

    public int GetDefense()
    {
        int defense = InitialDefense;
        foreach (var item in KnightItems)
        {
            defense += item.DefenseValue; //??
        }

        return defense;
    }

    public int Heal()
    {
        foreach (var item in KnightItems)
        {
            Life += item.Healing;
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
}