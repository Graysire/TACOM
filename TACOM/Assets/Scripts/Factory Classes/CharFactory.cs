using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/5/2019

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
    public static SimpleCharacter CreateChar(CharType type, AttackableBase.AttackableOpsType ops = AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType unit = AttackableBase.AttackableUnitType.Infantry)
    {
        return new SimpleCharacter(type, ops, unit);
    }

    //returns the normal Character version of a SimpleCharacter based on its type
    public static Character CreateChar(SimpleCharacter type)
    {
        switch (type.GetCharType())
        {
            case CharType.GCF_Trooper:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 1, "Trooper", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_LTrooper:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 1, "Lance Trooper", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_Corporal:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 2, "Corporal", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_LCorporal:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 2, "Lance Corporal", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_Sergeant:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 3, "Sergeant", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_LSergeant:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 3, "Lance Sergeant", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_LSergeant_Major:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 5, "Lance Sergeant Major", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_LCaptain:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 4, "Lance Captain", type.GetOpsType(), type.GetUnitType());
            case CharType.GCF_Captain:
                return new Character(20, 50, 5, 100, new Weapon(20, 5, 4, 2), 6, "Captain", type.GetOpsType(), type.GetUnitType());
            default:
                return new Character();
        }
    }
}
