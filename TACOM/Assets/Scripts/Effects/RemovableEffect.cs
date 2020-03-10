using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Effect that stays on a character and cna be removed to reverse its changes
public class RemovableEffect : ImmediateEffect
{
    //Default Constructor
    public RemovableEffect() :base()
    {
    }

    //Constructor with inputs, att for attribute and value for strength
    public RemovableEffect(string name, CharacterAttributes att, int value) : base(name, att,value)
    {

    }

    //removes the effect from the character
    public virtual void RemoveEffect(CharacterTargetInfo targetInfo)
    {
        targetInfo.logMessage += name + " removed, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (strength * -1);
        //targetInfo.logMessage += "\n" + name + " deals " + strength + " to " + targetInfo.target.GetName();
        targetInfo.target.ChangeAttribute(attribute, strength * -1);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";
        Debug.Log(targetInfo.logMessage);
    }

    //override, adds this effect to the character's active effects
    public override void ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        base.ApplyEffect(ref targetInfo);
        targetInfo.target.AddEffect(this);
    }

    //override, two RemoveableEffects are equal if their strength and attribute are the same
    public override bool Equals(object obj)
    {
        if (obj.GetType() == typeof(RemovableEffect))
        {
            if (((RemovableEffect)obj).strength == this.strength && ((RemovableEffect)obj).attribute == this.attribute)
            {
                return true;
            }
        }
        return false;
    }

    //no current reason to change HashCode function, but C# standards require the override be here
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
