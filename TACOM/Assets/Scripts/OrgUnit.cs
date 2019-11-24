using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/19/2019

//Organization that is composed of characters, if the Organization hierarchy is a tree, this is the leaves
public class OrgUnit : Organization
{
    private AbstractCharacter[] subChars; //characters that are a part of this organization
    public OrgUnit()
    {
        orgName = "Default";
        subChars = new AbstractCharacter[0];
        Debug.Log("Default");
    }

    public OrgUnit(string name, AbstractCharacter[] cha)
    {
        orgName = name;
        subChars = cha;
        for (int i = 0; i < subChars.Length; i++)
        {
            subChars[i].setParentOrg(this);
        }
    }
    public OrgUnit(OrgUnit other) //copy constructor
    {
        orgName = other.orgName;
        subChars = new Character[other.subChars.Length];
        for (int i = 0; i < other.subChars.Length; i++)
        {
            subChars[i] = other.subChars[i];
        }
    }

    public override string ToString()
    {
        string toString = orgName + "\n";
        for (int i = 0; i < subChars.Length; i++)
        {
            toString += "\t" + subChars[i].ToString() + "\n";
        }
        return toString;
    }

    //returns the toString tabbed in numTabs times
    public override string ToStringTabbed(int numTabs)
    {
        string toString = orgName + "\n";
        for (int i = 0; i < subChars.Length; i++)
        {
            for (int a = 0; a < numTabs; a++)
            {
                toString += "\t";
            }
            toString += subChars[i].ToString() + "\n";
        }
        return toString;
    }

    //returns number of Characters in the OrgUnit
    public override int GetOrgSize()
    {
        return subChars.Length;
    }

    //replaces all SimpleCharacters with their Character versions
    public void ReplaceSimple()
    {
        for (int i = 0; i < subChars.Length; i++)
        {
            if (subChars[i].GetType() == typeof(SimpleCharacter))
            {
                subChars[i] = CharFactory.CreateChar((SimpleCharacter)subChars[i]);

            }

        }
    }
}
