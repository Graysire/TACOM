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
        Ability a = new Ability(new ImmediateEffect("health", -20));
        c.UseAbility(a, c2);

        //tests Removable Effects
        RemovableEffect e = new RemovableEffect("health", -40);
        Ability a2 = new Ability(e);
        c.UseAbility(a2, c2);
        c2.RemoveEffect(e);

        //tests Temporary Effects
        TemporaryEffect e2 = new TemporaryEffect("health", -60, 2);
        Ability a3 = new Ability(e2);
        c.UseAbility(a3, c2);
        c2.TickCharacter();
        c2.TickCharacter();

    }

}
