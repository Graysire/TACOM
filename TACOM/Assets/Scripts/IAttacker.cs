using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Grayson Hill
 * Last Updated: 11/27/19
 */

//used for anything capable of attacking Attackables
public interface IAttacker : IAttackable
{
    //Attacks the target attackable
    void Attack(IAttackable target);
    //returns ToString tabbed in numTabs times for display as a part of a hierarchy
    string ToStringTabbed(int numTabs);
}
