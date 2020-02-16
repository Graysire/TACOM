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
        Ability b = new Ability(e);
        c.UseAbility(b, c2);
        c2.RemoveEffect(e);

    }

}
