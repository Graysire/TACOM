using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents anything a character can do to affect another character
public class Ability
{
    //list of all effects that the ability will apply when used
    List<ImmediateEffect> effects = new List<ImmediateEffect>();

    //the name of the ability
    string name;

    //default constructor
    public Ability()
    { }

    //Constructor for a Single Effect Ability
    public Ability(string name, ImmediateEffect eff)
    {
        this.name = name;
        effects.Add(eff);
    }

    //Constructor for Multiple Effect Ability
    public Ability(string name, ImmediateEffect[] eff)
    {
        this.name = name;
        effects.AddRange(eff);
    }

    public void ApplyEffects(CharacterTargetInfo targetInfo)
    {
        targetInfo.logMessage += targetInfo.source.GetName() + " uses " + name + " on " + targetInfo.target.GetName();
        foreach (ImmediateEffect eff in effects)
        {
            //Debug.Log("apply");
            eff.ApplyEffect(ref targetInfo);
        }
        Debug.Log(targetInfo.logMessage);
    }
}
