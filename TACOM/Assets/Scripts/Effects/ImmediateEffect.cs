﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents an event that applies an immediate change to a character
public class ImmediateEffect
{
    //the name of the attribute to be affected
    string attribute;
    //the numerical value the attribute will be changed by
    int strength;

    //Default Constructor, defaults to affecting health with a strength of 0
    public ImmediateEffect()
    {
        attribute = "health";
        strength = 0;
    }

    //Constructor with inputs, att for attribute and value for strength
    public ImmediateEffect(string att,int value)
    {
        attribute = att;
        strength = value;
    }

    //returns the attribute this Effect affects
    public string GetAttribute()
    {
        return attribute;
    }

    //returns the strength of this Effect
    public int GetStrength()
    {
        return strength;
    }

    //applies the modification to the target
    public virtual void ApplyEffect(Character target)
    {
        target.ChangeAttribute(attribute, strength);
    }

}