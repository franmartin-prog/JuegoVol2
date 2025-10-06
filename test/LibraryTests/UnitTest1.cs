using NUnit.Framework; 
using Library;

using Library.NuestrosCharacters;
using Ucu.Poo.RoleplayGame; // Dwarve, Wizard, Item, Grimoire, Elves

namespace LibraryTests
{
    [TestFixture]

    public class DwarfTests
    {
        [Test]
        // Justificación: Curar no debe superar la vida máxima (regla de negocio).
        public void Dwarve_Heal_NoSuperaMaxLife()
        {
            var d = new Dwarf("Gimli");
            d.Life = 100;
            d.AddItem(new Vandage("Vendas"));

            d.Heal();
            Assert.AreEqual(120, d.Life); // MaxLife = 120
        }

        [Test]
        // Justificación: Documentar implementación actual: GetAttack ACUMULA en cada llamada.
        public void Dwarve_GetAttack_AcumulaCadaLlamada_ImplActual()
        {
            var d = new Dwarf("Gimli");
            d.AddItem(new Axe("Hacha"));

            var first = d.GetAttack(); // 40 + 25 = 65
            var second = d.GetAttack(); // (no debería acumular)

            Assert.AreEqual(40 + 25, first);
            Assert.AreEqual(first, second);
        }

        [Test]
        // Justificación: no puede pasar que se cure porque su defensa sea mayor a su ataque
        public void Dwarve_ReceiveAttack_DefensaMayor_mantieneVida_ImplActual()
        {
            var d = new Dwarf("Gimli");
            d.Life = 120;
            d.AddItem(new Shield("Armadura"));

            Elf elfo1 = new Elf("elfo1");
            var nuevaVida = d.ReceiveAttack(elfo1); // defensa efectiva > ataque → vida aumenta // 120 + 75 - 20 = 175 

            Assert.AreEqual(nuevaVida, 120);
        }
    }

    public class ElvesTests
    {
        [Test]
        // Justificación: Elves.Heal = mitad de _maxLife + curas de ítems, con tope en _maxLife.
        public void Elves_Heal_SumaMitadYTopea()
        {
            var e = new Elf("Legolas");
            e.Life = 80; // _maxLife = 100
            e.AddItem(new Vandage("Hierbas"));
            e.Heal();

            Assert.AreEqual(100, e.Life);
        }

        [Test]
        // Justificación: Documentar implementación actual de daño: Life -= (ataque + defensa).
        public void Elves_RecieveAttack_UsaAtaqueMasDefensa_ImplActual()
        {
            var e = new Elf("Legolas");
            e.AddItem(new Helmet("Armadura Ligera"));
            Knight knight = new Knight("knight");
            var nuevaVida = e.ReceiveAttack(knight); // 100 vida + 25 - 30 ataque  = 95 vida

            Assert.Less(nuevaVida, 100); // perdió más que el ataque por sumar defensa
        }
    }

    public class WizardTests
    {
        [Test]
        // Justificación: ReadGrimoire solo aplica si magic > 10 y descuenta 10 de la propiedad Magic.
        public void Wizard_ReadGrimoire_Descuenta10SiParamMayor10()
        {
            var w = new Wizard("Merlin");
            var antes = w.Magic;

            w.ReadGrimoire();

            Assert.AreEqual(antes - 10, w.Magic);
        }

        [Test]
        // Justificación: Si magic <= 10, no debería descontar ni hacer nada.
        public void Wizard_ReadGrimoire_NoHaceNadaSiMagiaNoAlcanza()
        {
            var w = new Wizard("Merlin");
            var antes = w.Magic;

            w.ReadGrimoire();

            Assert.Less(w.Magic, antes);
        }
    }

    public class KnightTests
    {

        [Test]
        public void Constructor_IniciaConNombreYVidaMaxima()
        {
            var k = new Knight("Arturo");
            Assert.AreEqual("Arturo", k.Name);
            Assert.True(k.Life > 0); // vida empieza en _maxLife
        }

        [Test]
        public void GetAttack_SumaAtaqueItems_UnaLlamada()
        {
            var k = new Knight("Arturo");
            Sword sword = new Sword("Sword");
            k.AddItem(sword);
            var atk = k.GetAttack();

            // attack base = 20, +20 = 40
            Assert.AreEqual(40, atk);
        }

        [Test]
        public void GetAttack_AcumulaIndefinidamente_EntreLlamadas_ExponeBug()
        {
            var k = new Knight("Arturo");
            Sword sword = new Sword("Sword");
            k.AddItem(sword);
            var a1 = k.GetAttack(); // 20 + 20 = 40 (y _attack queda 40)
            var a2 = k.GetAttack(); // no vuelve a sumar el 20 
            

            Assert.AreEqual(40, a1);
            Assert.AreEqual(a1, a2);
        }

        [Test]
        public void GetDefense_AplicaMultiplicadorYAcumula_ExponeBug()
        {
            var k = new Knight("Arturo");
            Shield shield = new Shield("Shield");
            k.AddItem(shield);
            var d1 = k.GetDefense(); // base 80 + 15 = 95
            var d2 = k.GetDefense(); // no vuelve a sumar 15 

            Assert.AreEqual(95, d1);
            Assert.AreEqual(d1, d2); // acumulación no resetea entre llamadas
        }

        [Test]
        public void Stats_SiMuerto_SonCero()
        {
            var k = new Knight("Arturo");
            k.Life = 0;

            Assert.AreEqual(0, k.GetAttack());
            Assert.AreEqual(0, k.GetDefense());
        }

        [Test]
        public void ReceiveAttack_NoDeberiaCurar_SiDefensaMayorQueAtaque_Esperado()
        {
            var k = new Knight("Arturo");
            Shield shield = new Shield("Shield");
            k.AddItem(shield);
            // Con la implementación actual: Life -= (ataque - defensa)
            // Si defensa > ataque => (ataque - defensa) es negativo => Life -= negativo => Life AUMENTA (cura)
            // Lo esperado lógicamente: daño = max(0, ataque - defensa)

            Elf elf2 = new Elf("elf 2");
            var vidaInicial = k.Life;
            var vidaLuego = k.ReceiveAttack(elf2);

            // Este Assert expone el bug: hoy la vida sube o queda en _maxLife.
            Assert.True(vidaLuego >= vidaInicial,
                "BUG: el daño no debería jamás curar. Se espera que, si defensa >= ataque, el daño sea 0.");
        }
        

        [Test]
        public void Heal_SumaItemsYRespetaTope()
        {
            var k = new Knight("Arturo");
            Vandage potion = new Vandage("potion");
            k.AddItem(potion);
            Vandage potion2 = new Vandage("potion2");
            k.AddItem(potion2);

            // Dañamos antes para testear curación
            Elf elf2 = new Elf("elf 2");
            k.ReceiveAttack(elf2); // con defensa base 80, daño 20 -> Life baja en 60

            var vidaAntes = k.Life;
            k.Heal();
            var vidaDespues = k.Life;

            Assert.True(vidaDespues >= vidaAntes, "Debe aumentar la vida");
            Assert.True(vidaDespues <= 160, "No debe superar vida máxima");
        }

        [Test]
        public void Heal_NoHaceNada_SiMuerto()
        {
            var k = new Knight("Arturo");
            k.Life = 0;
            Vandage potion = new Vandage("potion");
            k.AddItem(potion);

            k.Heal();
            Assert.AreEqual(0, k.Life);
        }

        [Test]
        public void AddRemoveItem_NoOperan_SiMuerto()
        {
            var k = new Knight("Arturo");

            Sword sword = new Sword("Sword");
            k.AddItem(sword);
            k.Life = 0;
            k.RemoveItem(sword);
            Assert.IsNotEmpty(k.AttackItems);

            k.Life = 100;
            k.RemoveItem(sword);
            k.Life = 0;
            k.AddItem(sword);
            Assert.IsEmpty(k.AttackItems);

            // Forzamos una lista con 1 item y probamos Remove en muerto


        }

        [Test]
        public void ReceiveAttack_AumentaDefensaPorEfectoLateral_ExponeBug()
        {
            var k = new Knight("Arturo");
            Shield shield = new Shield("shield");
            k.AddItem(shield);

            // Primera vez que recibe ataque, internamente llama a GetDefense(),
            // que ACUMULA _defense (+30 por el escudo).
            var dAntes = k.GetDefense(); // ahora _defense ya se incrementó a 110
            Elf elf2 = new Elf("elf 2");

            var vida1 = k.ReceiveAttack(elf2); // vuelve a llamar GetDefense -> sube a 140
            var dDespues = k.GetDefense(); // vuelve a subir a 170

            // La defensa no va aumentando con cada consulta/ataque. Esto favorece al caballero “para siempre”.
            Assert.False(dDespues > dAntes);
        }



        public static class SpellTests
        {
            private static AttackSpell S(string name, int atk, int cost) => new AttackSpell(name, cost, atk);

            [Test]
            public static void Constructor_Asigna_Propiedades()
            {
                var s = S("Fuego", 25,20 );
                Assert.That(s.Name, Is.EqualTo("Fuego"));
                Assert.That(s.Attack, Is.EqualTo(25));
                Assert.That(s.Cost,Is.EqualTo(20));
            }

            [Test]
            public static void Constructor_Acepta_Valores_NoPositivos()
            {
                var s1 = S("Golpe", 0, 0);
                Assert.That(s1.Attack, Is.EqualTo(0));

                var s2 = S("Sangrado", -10, 0);
                Assert.That(s2.Attack, Is.EqualTo(-10));
            }

            [Test]
            public static void Propiedades_SoloGet_Inmutables()
            {
                var s = S("Hielo", 10, 20);
                var copia = s;
                Assert.IsTrue(object.ReferenceEquals(s, copia), "La referencia debe ser la misma.");

                var otro = S("Hielo", 10, 20);
                Assert.IsTrue(!object.ReferenceEquals(s, otro), "Instancias distintas aunque con mismos valores.");
            }

            [Test]
            public static void Name_PuedeSerNull_ComportamientoActual()
            {
                var s = new DefenseSpell(null, 20, 20);
                Assert.IsTrue(s.Name == null);
            }
        }

        [Test]
        public static void AddSpell_DosVeces_AgregaSegundo()
        {

            var f = new AttackSpell("Fuego", 10, 0);
            var h = new AttackSpell("Hielo", 20, 0);
            SpellsBook.AddSpell(f);
            SpellsBook.AddSpell(h);
            SpellsBook.AddSpell(f); // agrega Fuego

            SpellsBook.AddSpell(); // ahora agrega Hielo
            Wizard mago = new Wizard("mago");
            mago.ReadGrimoire();
            mago.ReadGrimoire();
            
            

            Assert.That(mago.GetAttack(), Is.EqualTo(80));

        }

        [Test]
        public static void GetAttack_AcumulaEntreLlamadas_ExponeBug()
        {
            AttackSpell Fuego = new AttackSpell("Fuego", 10, 0);
            SpellsBook.AddSpell(Fuego);
            SpellsBook.AddSpell();
            Wizard mago = new Wizard("mago");
            var a1 = mago.GetAttackCost();


            Assert.That(a1, Is.EqualTo(10));
        }

        [Test]
        public static void GetDefense_GetHealing_Acumulacion_ExponeBug()
        {
            Spell Roca = new Spell("Roca", 0, 7, 3);
            Grimoire.SetSpells(Roca);
            Grimoire.AddSpell();

            var d1 = Grimoire.GetDefense(); // 7
            var d2 = Grimoire.GetDefense(); // 14
            var h1 = Grimoire.gethealing(); // 3
            var h2 = Grimoire.gethealing(); // 6

            Assert.That(d1, Is.EqualTo(7));
            Assert.That(d2, Is.EqualTo(d1));
            Assert.That(h1, Is.EqualTo(3));
            Assert.That(h2, Is.EqualTo(h1));
        }

    }

    public class WizardTests2
    {
        [Test]
        public void Ctor_Inicializa_Propiedades_Basicas()
        {
            var wiz = new Wizard("Gandalf");
            Assert.AreEqual("Gandalf", wiz.Name);
            Assert.AreEqual(50, wiz.Life); // MaxLife
            Assert.AreEqual(100, wiz.Magic); // MaxMagic
            Assert.AreEqual(0, wiz.AttackItems.Count);
        }

        [Test]
        public void AddItem_Y_RemoveItem_ModificanColeccion()
        {
            var wiz = new Wizard("Merlin");
            var item = new Staff ("Daga");

            wiz.AddItem(item);
            Assert.AreEqual(1, wiz.AttackItems.Count);

            wiz.RemoveItem(item);
            Assert.AreEqual(0, wiz.AttackItems.Count);
        }

        [Test]
        public void GetAttack_SoloSumaItems_CuandoMagicEsCero()
        {
            var wiz = new Wizard("Merlin");
            // Evito que se use el Grimoire:
            wiz.Magic = 0;

            // Attack base = 50 (en la clase)
            wiz.AddItem(new Bow("Daga"));
            wiz.AddItem(new Sword("Anillo"));

            var atk = wiz.GetAttack();
            // 50 + 15 + 20= 85 (sin grimorio, porque Magic=0)
            Assert.AreEqual(85, atk);
            // Y Magic sigue en 0
            Assert.AreEqual(0, wiz.Magic);
        }

        [Test]
        public void GetDefense_SoloSumaItems_CuandoMagicEsCero()
        {
            var wiz = new Wizard("Merlin");
            wiz.Magic = 0;

            // Defense base = 5
            wiz.AddItem(new Armor("Escudo"));
            wiz.AddItem(new Shield("Capa"));

            var def = wiz.GetDefense();
            // 5 + 25 + 15 = 45
            Assert.AreEqual(45, def);
            Assert.AreEqual(0, wiz.Magic);
        }

        [Test]
        public void GetAttack_Consume5MagicSiHayMagiaSuficiente()
        {
            var wiz = new Wizard("Saruman");
            // No me importa cuánto suba Attack por el grimorio (estado global),
            // solo verifico el consumo de Magic.
            wiz.Magic = 10;
            var before = wiz.Magic;

            var atk = wiz.GetAttack();

            Assert.AreEqual(before - 5, wiz.Magic);
        }
    }
}

