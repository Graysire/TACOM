using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents an event that applies an immediate change to a character
public class ImmediateEffect
{
    //the name of the attribute to be affected
    protected CharacterAttributes attribute;
    //the numerical value the attribute will be changed by
    protected int strength;

    //the name of the effect
    protected string name;

    //Default Constructor, defaults to affecting health with a strength of 0
    public ImmediateEffect()
    {
        name = "WorldEffect";
        attribute = CharacterAttributes.Health;
        strength = 0;
    }

    //Constructor with inputs, att for attribute and value for strength
    public ImmediateEffect(string name, CharacterAttributes att, int value)
    {
        this.name = name;
        attribute = att;
        strength = value;
    }

    //returns the attribute this Effect affects
    public CharacterAttributes GetAttribute()
    {
        return attribute;
    }

    //returns the strength of this Effect
    public int GetStrength()
    {
        return strength;
    }

    //applies the modification to the target
    public virtual void ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + strength;
            
        targetInfo.target.ChangeAttribute(attribute, strength);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";
        //Debug.Log(targetInfo.logMessage);
    }

}
