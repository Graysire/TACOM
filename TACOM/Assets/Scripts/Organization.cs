using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/27/2019

//Represents a grouping of individuals or organizations within a faction
public class Organization : AttackerBase
{
    protected string orgName;
    protected OrgCombatType combatType;
    protected IAttacker[] orgComponents;
    //protected Faction orgFaction;

    //default constructor
    public Organization()
    {
        orgName = "Default";
        orgComponents = new IAttacker[0];
    }

    //constructor inputting name and array of components
    public Organization(string name, IAttacker[] sub)
    {
        orgName = name;
        orgComponents = sub;
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
        string toString = orgName + "\n";
        for (int i = 0; i < orgComponents.Length; i++)
        {
            toString += "\t" + orgComponents[i].ToStringTabbed(2) + "\n";
        }
        return toString;
    }
    public override string ToStringTabbed(int numTabs) //returns Organization as a string with tabs to create a visibile hierarchy
    {
        string toString = orgName + "\n";
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
    //public override abstract IAttackable GetTarget();

    //replaces all SimpleCharacters with their Character versions
    public void ReplaceSimple()
    {
        for (int i = 0; i < orgComponents.Length; i++)
        {
            if (orgComponents[i].GetType() == typeof(SimpleCharacter))
            {
                orgComponents[i] = CharFactory.CreateChar((SimpleCharacter)orgComponents[i]);

            }

        }
    }

    protected enum OrgCombatType { Line, Logistics, Command};

}


