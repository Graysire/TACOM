﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Grayson Hill
 Last Updated: 12/9/19
     */

//base implementation of the IAttackable interface
public abstract class AttackableBase : IAttackable
{
    protected AttackableOpsType opsType; //the purpose of the attackable (i.e. command)
    protected AttackableUnitType unitType; //the type of the attackable (i.e. infantry)
    protected bool isAlive = true; //whether the attackable is incapacitated
    protected bool inCombat = false; //whether the attackable is in combat

    public virtual IAttackable GetTarget()
    {
        return this;
    }

    public virtual int GetThreat()
    {
        Debug.Log("Attackable Base GetThreat");
        return 0;
    }
    public virtual int GetWeight()
    {
        if (isAlive)
        {
            return GetThreat();
        }
        else
        {
            return 0;
        }
    }
    public virtual int GetWeight(AttackableOpsType ops)
    {
        if (isAlive)
        {
            return GetThreat(ops);
        }
        else
        {
            return 0;
        }
    }
    public virtual int GetWeight(AttackableUnitType unit)
    {
        if (isAlive)
        {
            return GetThreat(unit);
        }
        else
        {
            return 0;
        }
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
    public virtual int GetSize()
    {
        return 1;
    }
    public virtual int GetSize(AttackableOpsType ops)
    {
        Debug.Log("Attackable Base ops GetSize");
        return 1;
    }
    public virtual int GetSize(AttackableUnitType unit)
    {
        Debug.Log("Attackable Base unit GetSize");
        return 1;
    }

    public virtual string GetSTWString()
    {
        return "(S/T/W Total: " + GetSize() + "/" + GetThreat() + "/" + GetWeight() +
            ", Command: " + GetSize(AttackableOpsType.Command) + "/" + GetThreat(AttackableOpsType.Command) + "/" + GetWeight(AttackableOpsType.Command) +
            ", Logistics: " + GetSize(AttackableOpsType.Logistics) + "/" + GetThreat(AttackableOpsType.Logistics) + "/" + GetWeight(AttackableOpsType.Logistics) +
            ", Line: " + GetSize(AttackableOpsType.Line) + "/" + GetThreat(AttackableOpsType.Line) + "/" + GetWeight(AttackableOpsType.Line) + ")";
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

    public virtual bool GetIsAlive()
    {
        return isAlive;
    }

    public virtual void CheckIsAlive()
    {
        return;
    }

    public virtual bool GetInCombat()
    {
        return inCombat;
    }

    public virtual void SetInCombat(bool inCombt)
    {
        inCombat = inCombt;
    }

    //enum used for the purpose of an attackable
    public enum AttackableOpsType { Command, Logistics, Line}

    //enum used for the composition of an attackable
    public enum AttackableUnitType { Infantry, Armor}
}
