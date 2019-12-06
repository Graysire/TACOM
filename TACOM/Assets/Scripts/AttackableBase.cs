using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Grayson Hill
 Last Updated: 12/4/19
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

    public virtual int GetWeight()
    {
        return GetThreat();
    }

    public virtual int GetWeight(AttackableOpsType ops)
    {
        return GetThreat(ops);
    }

    public virtual int GetWeight(AttackableUnitType unit)
    {
        return GetThreat(unit);
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

    public virtual int GetMinThreat(AttackableOpsType ops)
    {
        Debug.Log("Attackable Base ops GetMinThreat");
        return 1;
    }

    public virtual int GetMinThreat(AttackableUnitType unit)
    {
        Debug.Log("Attackable Base unit GetMinThreat");
        return 1;
    }

    public virtual string GetSTWString()
    {
        return "(S/T/W Total: " + GetMinThreat() + "/" + GetThreat() + "/" + GetWeight() +
            ", Command: " + GetMinThreat(AttackableOpsType.Command) + "/" + GetThreat(AttackableOpsType.Command) + "/" + GetWeight(AttackableOpsType.Command) +
            ", Logistics: " + GetMinThreat(AttackableOpsType.Logistics) + "/" + GetThreat(AttackableOpsType.Logistics) + "/" + GetWeight(AttackableOpsType.Logistics) +
            ", Line: " + GetMinThreat(AttackableOpsType.Line) + "/" + GetThreat(AttackableOpsType.Line) + "/" + GetWeight(AttackableOpsType.Line) + ")";
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
