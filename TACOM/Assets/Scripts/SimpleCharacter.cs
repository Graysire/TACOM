using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/3/2019

//SimpleCharacter serves as a placeholder until an instance of Character is needed
public class SimpleCharacter : AbstractCharacter
{
    private readonly CharFactory.CharType charType; //the type of character

    public SimpleCharacter() //default constructor
    {
        charType = CharFactory.CharType.GCF_Trooper;
    }

    //constructors accepts type of the character
    public SimpleCharacter(CharFactory.CharType type, AttackableOpsType ops, AttackableUnitType unit)
    {
        charType = type;
        opsType = ops;
        unitType = unit;
    }

    //returns type of the character
    public CharFactory.CharType GetCharType()
    {
        return charType;
    }

    public override string ToString()
    {
        return "" + charType;
    }

    public override string ToStringTabbed(int numTabs)
    {
        return ToString();
    }
}
