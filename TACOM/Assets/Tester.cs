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
        Character c = new Character();
        Character c2 = new Character();
        ImmediateEffect e = new ImmediateEffect("health", -20);
        Ability a = new Ability(e);
        c.UseAbility(a, c2);

        //tests Removable Effects
        RemovableEffect e1 = new RemovableEffect("health", -40);
        Ability a2 = new Ability(e1);
        c.UseAbility(a2, c2);
        c2.RemoveEffect(e1);

        //tests Temporary Effects
        TemporaryEffect e2 = new TemporaryEffect("health", -60, 2);
        Ability a3 = new Ability(e2);
        c.UseAbility(a3, c2);
        c2.TickCharacter();
        c2.TickCharacter();

        //tests Periodic Temporary Effects
        ImmediateEffect[] eff = { e };
        PeriodicTemporaryEffect e3 = new PeriodicTemporaryEffect("health", -20, 3, 1, eff, true);
        Ability a4 = new Ability(e3);
        c.UseAbility(a4, c2);
        c2.TickCharacter();
        c2.TickCharacter();
        c2.TickCharacter();


    }

}
