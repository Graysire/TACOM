using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents anything a character can do to affect another character
public class Ability
{
    [SerializeField]
    //list of all effects that the ability will apply when used
    List<ImmediateEffect> effects = new List<ImmediateEffect>();

    public Ability()
    { }

    public Ability(ImmediateEffect eff)
    {
        effects.Add(eff);
    }

    public void applyEffects(Character target)
    {
        foreach (ImmediateEffect eff in effects)
        {
            Debug.Log("apply");
            eff.applyEffect(target);
        }
    }
}
