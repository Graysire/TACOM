using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents anything a character can do to affect another character
public class Ability
{
    [SerializeField]
    //list of all effects that the ability will apply when used
    List<ImmediateEffect> effects = new List<ImmediateEffect>();

    //default constructor
    public Ability()
    { }

    //Constructor for a Single Effect Ability
    public Ability(ImmediateEffect eff)
    {
        effects.Add(eff);
    }

    //Constructor for Multiple Effect Ability
    public Ability(ImmediateEffect[] eff)
    {
        effects.AddRange(eff);
    }

    public void ApplyEffects(CharacterTargetInfo targetInfo)
    {
        foreach (ImmediateEffect eff in effects)
        {
            //Debug.Log("apply");
            eff.ApplyEffect(targetInfo);
        }
    }
}
