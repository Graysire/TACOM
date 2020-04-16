using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Effect that stays on a character and can be removed to reverse its changes
public class RemovableEffect : ImmediateEffect
{
    //how much power is actually applied after negation by armor and such
    protected int powerApplied;

    //Default Constructor
    public RemovableEffect() :base()
    {
    }

    //Constructor with inputs, att for attribute and value for strength
    public RemovableEffect(string name, CharacterAttributes att, int value, CharacterAttributes powBonus, float powBonusMultiplier = 0.5f, bool isDmg = true, bool affectedByArmor = true, int num = 0, int sides = 0) 
        : base(name, att,value, powBonus, powBonusMultiplier, isDmg, affectedByArmor, num,sides)
    {

    }

    //Copy constructor
    public RemovableEffect(RemovableEffect other)
    {
        name = other.name;
        attribute = other.attribute;
        power = other.power;
    }


    //removes the effect from the character
    public virtual void RemoveEffect(CharacterTargetInfo targetInfo)
    {
        targetInfo.logMessage += name + " removed, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (isDamage?powerApplied:powerApplied * -1);
        //targetInfo.logMessage += "\n" + name + " deals " + strength + " to " + targetInfo.target.GetName();
        targetInfo.target.ChangeAttribute(attribute, isDamage ? powerApplied : powerApplied * -1);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";
        Debug.Log(targetInfo.logMessage);
    }

    //override, adds this effect to the character's active effects
    public override int ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        int finalPower = base.ApplyEffect(ref targetInfo);
        AddCopy(ref targetInfo, finalPower);
        return finalPower;
    }

    //adds a copy of this effect to the target with a powerApplied of finalPower
    protected virtual void AddCopy(ref CharacterTargetInfo targetInfo, int finalPower)
    {
        RemovableEffect temp = new RemovableEffect(this)
        {
            powerApplied = finalPower
        };
        targetInfo.target.AddEffect(temp);
    }

    //override, two RemoveableEffects are equal if their strength and attribute are the same
    public override bool Equals(object obj)
    {
        if (obj.GetType() == typeof(RemovableEffect))
        {
            if (((RemovableEffect)obj).power == this.power && ((RemovableEffect)obj).attribute == this.attribute)
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
