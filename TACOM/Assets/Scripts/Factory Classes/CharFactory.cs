using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/19/2019

//hub class that manages the creation of standard Characters
public class CharFactory
{
    //enumeration of all standard Characters
    public enum CharType
    {
        GCF_Trooper, GCF_LTrooper, GCF_LCorporal, GCF_Corporal, GCF_LSergeant, GCF_Sergeant, GCF_LSergeant_Major, GCF_Sergeant_Major, GCF_LCSergeant_Major, GCF_CSergeant_Major,
        GCF_LCaptain, GCF_Captain, GCF_LColonel, GCF_Colonel, GCF_LGeneral, GCF_General, GCF_LCGeneral, GCF_CGeneral
    }

    //returns a SimpleCharacter based on type
    public static SimpleCharacter CreateChar(CharType type)
    {
        return new SimpleCharacter(type);
    }

    //returns the normal Character version of a SimpleCharacter based on its type
    public static Character CreateChar(SimpleCharacter type)
    {
        switch (type.GetCharType())
        {
            case CharType.GCF_Trooper:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 1);
            default:
                return new Character();
        }
    }
}
