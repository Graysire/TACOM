using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Updated: 12/5/19
 */

//used for anything that can be attacked
public interface IAttackable
{
    //returns a target to be used for an attack
    IAttackable GetTarget();
    //returns an int representing base likelihood of an Attackable being targetted
    int GetThreat();
    int GetThreat(AttackableBase.AttackableOpsType ops); //the threat of the specific OpsType
    int GetThreat(AttackableBase.AttackableUnitType unit); //the threat of the specific UnitType

    //returns an int representing the calculated weight of an Attackable for being targetted
    int GetWeight();
    int GetWeight(AttackableBase.AttackableOpsType ops); //weight of specific OpsType
    int GetWeight(AttackableBase.AttackableUnitType unit); //weight of specific UnitType


    //returns the number of Attackables that don't contain other Attackables within this Attackable
    int GetMinThreat();
    int GetMinThreat(AttackableBase.AttackableOpsType ops);
    int GetMinThreat(AttackableBase.AttackableUnitType unit);

    //returns the string of MinThreat, Threat, and Weight put together
    string GetSTWString();
    
    //void TakeDamage(int dmg);
}
