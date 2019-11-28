using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Grayson Hill
 Last Updated: 11/27/19
     */

//base implementation of the IAttackable interface
public abstract class AttackableBase : IAttackable
{
    public virtual IAttackable GetTarget()
    {
        return this;
    }

    public virtual int GetThreat()
    {
        Debug.Log("Attackable Base GetThreat");
        return 0;
    }

    public virtual int GetMinThreat()
    {
        return 1;
    }
}
