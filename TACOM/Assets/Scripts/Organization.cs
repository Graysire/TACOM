using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/4/2019

//Represents a grouping of individuals or organizations within a faction
public class Organization : AttackerBase
{
    private string orgName;
    private IAttacker[] orgComponents;
    //protected Faction orgFaction;

    //default constructor
    public Organization()
    {
        orgName = "Default";
        orgComponents = new IAttacker[0];
        opsType = AttackableOpsType.Line;
        unitType = AttackableUnitType.Infantry;
    }

    //constructor inputting name and array of components
    public Organization(string name, IAttacker[] sub, AttackableOpsType ops = AttackableOpsType.Line, AttackableUnitType unit = AttackableUnitType.Infantry)
    {
        orgName = name;
        orgComponents = sub;
        opsType = ops;
        unitType = unit;
    }

    //copy constructor
    public Organization(Organization other) //copy constructor
    {
        Debug.Log("Copy");
        orgName = other.orgName;
        orgComponents = new Organization[other.orgComponents.Length];
        for (int i = 0; i < other.orgComponents.Length; i++)
        {
            orgComponents[i] = other.orgComponents[i];
        }
    }

    public override string ToString() //returns Organization as a string
    {
        string toString = orgName + " (Total Threat/Weight: " + GetThreat() + "/" + GetWeight() + ", Command: " + GetThreat(AttackableOpsType.Command) + "/" + GetWeight(AttackableOpsType.Command) +
            ", Logistics: " + GetThreat(AttackableOpsType.Logistics) + "/" + GetWeight(AttackableOpsType.Logistics) + ", Line: " + GetThreat(AttackableOpsType.Line) + "/" + GetWeight(AttackableOpsType.Line) + ")\n";
        for (int i = 0; i < orgComponents.Length; i++) 
        {
            toString += "\t" + orgComponents[i].ToStringTabbed(2) + "\n";
        }
        return toString;
    }
    public override string ToStringTabbed(int numTabs) //returns Organization as a string with tabs to create a visibile hierarchy
    {
        string toString = orgName + " (Total Threat/Weight: " + GetThreat() + "/" + GetWeight() + ", Command: " + GetThreat(AttackableOpsType.Command) + "/" + GetWeight(AttackableOpsType.Command) +
            ", Logistics: " + GetThreat(AttackableOpsType.Logistics) + "/" + GetWeight(AttackableOpsType.Logistics) + ", Line: " + GetThreat(AttackableOpsType.Line) + "/" + GetWeight(AttackableOpsType.Line) + ")\n";
        for (int i = 0; i < orgComponents.Length; i++)
        {
            for (int a = 0; a < numTabs; a++)
            {
                toString += "\t";
            }
            toString += orgComponents[i].ToStringTabbed(numTabs + 1) + "\n";
        }
        return toString;
    }
    public override int GetThreat()
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetThreat();
        }
        return total;
    }
    public override int GetThreat(AttackableOpsType ops)
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            if (a.GetType().IsSubclassOf(typeof(AttackableBase)))
            {
                total += ((AttackableBase) a).GetThreat(ops);
            }
        }
        return total;
    }
    public override int GetThreat(AttackableUnitType unit)
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            if (a.GetType().IsSubclassOf(typeof(AttackableBase)))
            {
                total += ((AttackableBase)a).GetThreat(unit);
            }
        }
        return total;
    }
    public override int GetMinThreat()
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetMinThreat();
        }
        return total;
    }
    public override void Attack(IAttackable target)
    {
        for (int i = 0; i < orgComponents.Length; i++)
        {
            orgComponents[i].Attack(target.GetTarget());
        }
    }

    //replaces all SimpleCharacters with their Character versions
    public void ReplaceSimple()
    {
        for (int i = 0; i < orgComponents.Length; i++)
        {
            if (orgComponents[i].GetType() == typeof(SimpleCharacter))
            {
                orgComponents[i] = CharFactory.CreateChar((SimpleCharacter)orgComponents[i]);

            }
            else if (orgComponents[i].GetType() == typeof(Organization))
            {
                ((Organization)orgComponents[i]).ReplaceSimple();
            }

        }
    }

    public override IAttackable GetTarget()
    {
        int totalWeight = GetWeight();
        //Debug.Log(orgName + " (Total: " + GetThreat() + ", Command: " + GetThreat(AttackableOpsType.Command) +
        //    ", Logistics: " + GetThreat(AttackableOpsType.Logistics) + ", Line: " + GetThreat(AttackableOpsType.Line) + ") Weighting: " + totalWeight+"\n");
        int count = -1;
        while (totalWeight > 0)
        {
            count++;
            totalWeight -= orgComponents[count].GetWeight();
        }
        return orgComponents[count].GetTarget();
    }

    public override int GetWeight()
    {
        return GetThreat(AttackableOpsType.Command) + GetThreat(AttackableOpsType.Logistics) * 3 + GetThreat(AttackableOpsType.Line) * 6;
    }

    public override int GetWeight(AttackableOpsType ops)
    {
        switch (ops)
        {
            case AttackableOpsType.Command:
                return GetThreat(ops);
            case AttackableOpsType.Logistics:
                return GetThreat(ops) * 3;
            case AttackableOpsType.Line:
                return GetThreat(ops) * 6;
            default:
                Debug.Log("GetWeight(ops) Default Case Warning");
                return GetWeight();
        }
    }
}


