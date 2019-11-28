using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Updated: 11/27/19
 */

//used for anything that can be attacked
public interface IAttackable
{
    //returns a target to be used for an attack
    IAttackable GetTarget();
    //returns an int representing the liklihood of an Attackable being targetted
    int GetThreat();
    //returns the number of Attackables that don't contain other Attackables within this Attackable
    int GetMinThreat();
    
    //void TakeDamage(int dmg);
}
