using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Grayson Hill
 * Last Updated 12/9/19
 * */

//base class provides default implementation for the IAttacker Interface
public abstract class AttackerBase : AttackableBase, IAttacker
{
    public virtual void Attack(IAttackable target)
    {
        Debug.Log("AttackableBase attacked " + target);
    }

    public virtual string ToStringTabbed(int numTabs)
    {
        string toString = "";
        for (int a = 0; a < numTabs; a++)
        {
            toString += "\t";
        }
        toString += ToString();
        return toString;
    }

    public virtual void Engage(IAttackable target)
    {
        inCombat = true;
        target.SetInCombat(true);
        new Combat(this, target);
    }
}
