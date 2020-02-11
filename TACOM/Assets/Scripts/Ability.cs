using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents anything a character can do to affect another character
public class Ability
{
    [SerializeField]
    //list of all effects that the ability will apply when used
    List<Effect> effects = new List<Effect>();

    public Ability()
    { }

    public Ability(Effect eff)
    {
        effects.Add(eff);
    }

    public void applyEffects(Character target)
    {
        foreach (Effect eff in effects)
        {
            Debug.Log("apply");
            eff.doEffect(target);
        }
    }
}
