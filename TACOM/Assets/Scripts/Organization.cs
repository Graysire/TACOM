using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/9/2019

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
        string toString = orgName + GetSTWString() + "\n";
        for (int i = 0; i < orgComponents.Length; i++) 
        {
            toString += "\t" + orgComponents[i].ToStringTabbed(2) + "\n";
        }
        return toString;
    }
    public override string ToStringTabbed(int numTabs) //returns Organization as a string with tabs to create a visibile hierarchy
    {
        string toString = orgName + " " + GetSTWString() + "\n";
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
            total += a.GetThreat(ops);
            
        }
        return total;
    }
    public override int GetThreat(AttackableUnitType unit)
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetThreat(unit);

        }
        return total;
    }
    public override int GetSize()
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetSize();
        }
        return total;
    }
    public override int GetSize(AttackableOpsType ops)
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetSize(ops);
        }
        return total;
    }
    public override int GetSize(AttackableUnitType unit)
    {
        int total = 0;
        foreach (IAttackable a in orgComponents)
        {
            total += a.GetSize(unit);
        }
        return total;
    }

    public override void Attack(IAttackable target)
    {
        for (int i = 0; i < orgComponents.Length; i++)
        {
            //If a component is a Line Organization that is not in combat and is alive
            if (orgComponents[i].GetType() == typeof(Organization) && orgComponents[i].GetOpsType() == AttackableOpsType.Line && !orgComponents[i].GetInCombat() && orgComponents[i].GetIsAlive())
            {
                orgComponents[i].Engage(target.GetTarget());
            } //else if a component is Alive
            else if (orgComponents[i].GetIsAlive())
            {
                IAttackable temp = target.GetTarget();
                Debug.Log(orgComponents[i].ToString() + "\n\t" + temp.ToString());
                orgComponents[i].Attack(temp);
            }
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
        int totalWeight = Random.Range(0, GetWeight() - 1);
        //Debug.Log(orgName + " (Total: " + GetThreat() + ", Command: " + GetThreat(AttackableOpsType.Command) +
        //    ", Logistics: " + GetThreat(AttackableOpsType.Logistics) + ", Line: " + GetThreat(AttackableOpsType.Line) + ") Weighting: " + totalWeight+"\n");
        int count = -1;
        while (totalWeight >= 0)
        {
            count++;
            //Debug.Log(totalWeight + " " + count + "/" + (orgComponents.Length-1));
            totalWeight -= orgComponents[count].GetWeight();
        }
        return orgComponents[count];
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

    public override void CheckIsAlive()
    {
        if (isAlive) //if alive, check to make sure it's still alive
        {
            for (int i = 0; i < orgComponents.Length; i++)
            {
                orgComponents[i].CheckIsAlive();
                if (orgComponents[i].GetIsAlive()) //if any component is still alive this org is alive
                {
                    return;
                }
            }
            isAlive = false;
        }
        else //if its not alive nothing changes
        {
            return;
        }
    }
}


