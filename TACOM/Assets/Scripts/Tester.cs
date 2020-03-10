using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tester for classes that do not rely on Monobehaviour or ScriptableObject
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //tests Immediate Effects
        Character c = new Character("Test Character 1");
        Character c2 = new Character("Test Character 2");
        ImmediateEffect e = new ImmediateEffect(CharacterAttributes.Health, -20);
        Ability a = new Ability(e);
        c.UseAbility(a, new CharacterTargetInfo(c,c2));

        //tests Removable Effects
        RemovableEffect e1 = new RemovableEffect(CharacterAttributes.Health, -40);
        Ability a2 = new Ability(e1);
        c.UseAbility(a2, new CharacterTargetInfo(c, c2));
        c2.RemoveEffect(e1);

        //tests Temporary Effects
        TemporaryEffect e2 = new TemporaryEffect(CharacterAttributes.Health, -60, 2);
        Ability a3 = new Ability(e2);
        c.UseAbility(a3, new CharacterTargetInfo(c, c2));
        c2.TickCharacter();
        c2.TickCharacter();

        //tests Periodic Temporary Effects
        ImmediateEffect[] eff = { e };
        PeriodicTemporaryEffect e3 = new PeriodicTemporaryEffect(CharacterAttributes.Health, -20, 2, 1, eff, true);
        Ability a4 = new Ability(e3);
        c.UseAbility(a4, new CharacterTargetInfo(c, c2));
        c2.TickCharacter();
        c2.TickCharacter();
        c2.TickCharacter();


    }

}
