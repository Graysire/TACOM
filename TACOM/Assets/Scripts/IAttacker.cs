using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Grayson Hill
 * Last Updated: 12/24/19
 */

//used for anything capable of attacking Attackables
public interface IAttacker : IAttackable
{
    //Attacks the target attackable
    void Attack(IAttackable target);
    //Attacks the target Organization
    void Attack(Organization target);
    //Attacks the target Character
    void Attack(Character target);

    //Starts a new combat with the target attackable
    void Engage(IAttackable target);

    //returns ToString tabbed in numTabs times for display as a part of a hierarchy
    string ToStringTabbed(int numTabs);
}
