using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Updated: 12/24/19
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

    //returns the number of individual Attackables that aren't just containers for more Attackables within this Attackable
    int GetSize();
    int GetSize(AttackableBase.AttackableOpsType ops);
    int GetSize(AttackableBase.AttackableUnitType unit);

    //returns the string of MinThreat, Threat, and Weight put together
    string GetSTWString();

    //returns whether the attackable is not incapacitated
    bool GetIsAlive();
    //returns whether the attackable is in combat
    bool GetInCombat();
    //checks whether the attackable is still alive
    void CheckIsAlive();

    //sets incombat to incombt
    void SetInCombat(bool inCombt);

    void HandleAttack(IAttacker attacker);

    //returns the ops type
    AttackableBase.AttackableOpsType GetOpsType();
    //returns the unit type
    AttackableBase.AttackableUnitType GetUnitType();

    //void TakeDamage(int dmg);
}
