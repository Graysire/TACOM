using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Grayson Hill
 Last Updated: 12/3/19
     */

//base implementation of the IAttackable interface
public abstract class AttackableBase : IAttackable
{
    protected AttackableOpsType opsType;
    protected AttackableUnitType unitType;

    public virtual IAttackable GetTarget()
    {
        return this;
    }

    public virtual int GetThreat()
    {
        Debug.Log("Attackable Base GetThreat");
        return 0;
    }

    public virtual int GetThreat(AttackableOpsType ops)
    {
        Debug.Log("Attackable Base ops GetThreat");
        return 0;
    }

    public virtual int GetThreat(AttackableUnitType unit)
    {
        Debug.Log("Attackable Base unit GetThreat");
        return 0;
    }

    public virtual int GetMinThreat()
    {
        return 1;
    }

    //returns Attackables operational type
    public virtual AttackableOpsType GetOpsType()
    {
        return opsType;
    }

    //returns Attackables unit type
    public virtual AttackableUnitType GetUnitType()
    {
        return unitType;
    }

    //enum used for the purpose of an attackable
    public enum AttackableOpsType { Command, Logistics, Line}

    //enum used for the composition of an attackable
    public enum AttackableUnitType { Infantry, Armor}
}
