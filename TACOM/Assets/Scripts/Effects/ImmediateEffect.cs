﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents an event that applies an immediate change to a character
public class ImmediateEffect
{
    //the name of the attribute to be affected
    protected CharacterAttributes attribute;
    //the based numerical value the attribute will be changed by
    protected int power;

    //Source character's attribute that provide a bonus to the power
    CharacterAttributes powerBonus;
    //number of dice rolled to determine the final power ofthe Effect
    protected int numDice;
    //number ofsides each die has
    protected int diceSides;

    //if true, power is subtracted from attribute, otherwise damage is added
    protected bool isDamage;

    //the name of the effect
    protected string name;

    //Default Constructor, defaults to affecting health with a strength of 0
    public ImmediateEffect()
    {
        name = "WorldEffect";
        attribute = CharacterAttributes.Health;
        power = 0;
    }

    //Constructor with inputs, att for attribute and value for strength
    public ImmediateEffect(string name, CharacterAttributes att, int value, bool isDmg = true, int num = 1, int sides = 10)
    {
        this.name = name;
        attribute = att;
        power = value;
        numDice = num;
        diceSides = sides;
        isDamage = isDmg;
    }

    //returns the attribute this Effect affects
    public CharacterAttributes GetAttribute()
    {
        return attribute;
    }

    //returns the strength of this Effect
    public int GetPower()
    {
        return power;
    }

    //returns whether this effect is a damaging effect
    public bool GetIsDamage()
    {
        return isDamage;
    }

    //applies the modification to the target
    public virtual void ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        int finalPower = power;
        for (int i = 0; i < numDice; i++)
        {
            finalPower += Random.Range(1, diceSides + 1);
        }

        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (isDamage?-1 * finalPower:finalPower) + 
            "(" + numDice + "d" + diceSides + "+" + power + ")";
        
        //if isDamage, change the target attribute by -1* finalPower, otherwise change it by finalPower
        targetInfo.target.ChangeAttribute(attribute, isDamage?-1 *finalPower:finalPower);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";
        //Debug.Log(targetInfo.logMessage);
    }

}
