using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tester for classes that do not rely on Monobehaviour or ScriptableObject
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] att = { 9, 9, 9, 9, 9, 9, 4, 100, 6 };
        //tests Immediate Effects
        Character c = new Character("Test Character 1", att);
        Character c2 = new Character("Test Character 2", att);
        ImmediateEffect e = new ImmediateEffect("TestImmediateEffect-20",CharacterAttributes.Health, 20);
        Ability a = new Ability("TestAbilityImmediate",e, CharacterAttributes.Perception, CharacterAttributes.Defense);
        c.UseAbility(a, new CharacterTargetInfo(c,c2));

        //tests Removable Effects
        RemovableEffect e1 = new RemovableEffect("TestRemovableEffect-40",CharacterAttributes.Health, 40);
        Ability a2 = new Ability("TestAbilityRemovable",e1, CharacterAttributes.Perception, CharacterAttributes.Defense);
        c.UseAbility(a2, new CharacterTargetInfo(c, c2));
        //e1.RemoveEffect(new CharacterTargetInfo(c2, c2));

        //tests Temporary Effects
        TemporaryEffect e2 = new TemporaryEffect("TestTemporaryEffect-60",CharacterAttributes.Health, 60, 2);
        Ability a3 = new Ability("TestAbilityTemporary",e2, CharacterAttributes.Perception, CharacterAttributes.Defense);
        c.UseAbility(a3, new CharacterTargetInfo(c, c2));
        c2.TickCharacter();
        c2.TickCharacter();

        //tests Periodic Temporary Effects
        ImmediateEffect[] eff = { e };
        PeriodicTemporaryEffect e3 = new PeriodicTemporaryEffect("TestPeriodicEffect-20",CharacterAttributes.Health, 20, 2, 1, eff, true);
        Ability a4 = new Ability("TestAbilityPeriodic",e3, CharacterAttributes.Perception, CharacterAttributes.Defense);
        c.UseAbility(a4, new CharacterTargetInfo(c, c2));
        c2.TickCharacter();
        c2.TickCharacter();
        c2.TickCharacter();


    }

}
