﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Effect that stays on a character and cna be removed to reverse its changes
public class RemovableEffect : ImmediateEffect
{
    protected int powerApplied;

    //Default Constructor
    public RemovableEffect() :base()
    {
    }

    //Constructor with inputs, att for attribute and value for strength
    public RemovableEffect(string name, CharacterAttributes att, int value, bool isDmg = true, bool affectedByArmor = true, int num = 0, int sides = 0) 
        : base(name, att,value, isDmg, affectedByArmor, num,sides)
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
    public override void ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        //initial finalPower is the base power - the target's armor if this effect is affected by armor
        int finalPower = power - (isAffectedByArmor ? targetInfo.target.GetAttribute(CharacterAttributes.Armor) : 0);
        for (int i = 0; i < numDice; i++)
        {
            finalPower += Random.Range(1, diceSides + 1);
        }
        //ensures finalPower is never negative
        if (finalPower < 0)
        {
            finalPower = 0;
        }

        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (isDamage ? -1 * finalPower : finalPower) +
            "(" + numDice + "d" + diceSides + "+" + power + (isAffectedByArmor ? "-" + targetInfo.target.GetAttribute(CharacterAttributes.Armor) : "") + ")";

        //if isDamage, change the target attribute by -1* finalPower, otherwise change it by finalPower
        targetInfo.target.ChangeAttribute(attribute, isDamage ? -1 * finalPower : finalPower);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";

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
