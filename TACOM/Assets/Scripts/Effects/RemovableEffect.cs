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
    public RemovableEffect(CharacterAttributes att, int value) : base(att,value)
    {

    }

    //removes the effect from the character
    public virtual void RemoveEffect(Character target)
    {
        target.ChangeAttribute(attribute, strength * -1);
    }

    //override, adds this effect to the character's active effects
    public override void ApplyEffect(Character target)
    {
        base.ApplyEffect(target);
        target.AddEffect(this);
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
