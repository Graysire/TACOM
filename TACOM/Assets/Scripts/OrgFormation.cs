using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/19/2019

//An organization that contains other orgaizations
public class OrgFormation : Organization
{
    private Organization[] subOrgs; //organizations that are a part of this organization
    public OrgFormation()
    {
        orgName = "Default";
        subOrgs = new Organization[0];
        Debug.Log("Default");
    }

    public OrgFormation(string name, Organization[] sub)
    {
        orgName = name;
        subOrgs = sub;
    }
    public OrgFormation(OrgFormation other) //copy constructor
    {
        Debug.Log("Copy");
        orgName = other.orgName;
        subOrgs = new Organization[other.subOrgs.Length];
        for(int i = 0; i < other.subOrgs.Length; i++)
        {
            subOrgs[i] = other.subOrgs[i];
        }
    }

    //returns as a string
    public override string ToString()
    {
        string toString = orgName + "\n";
        for (int i = 0; i < subOrgs.Length; i++)
        {
            toString += "\t" + subOrgs[i].ToStringTabbed(2) + "\n";
        }
        return toString;
    }

    //returns the toString tabbed in numTabs times
    public override string ToStringTabbed(int numTabs)
    {
        string toString = orgName + "\n";
        for (int i = 0; i < subOrgs.Length; i++)
        {
            for (int a = 0; a < numTabs; a++)
            {
                toString += "\t";
            }
            toString += subOrgs[i].ToStringTabbed(numTabs + 1) + "\n";
        }
        return toString;
    }

    //returns number of Characters in this Org and all its subOrgs
    public override int GetOrgSize()
    {
        int size = 0;
        for (int i = 0; i < subOrgs.Length; i++)
        {
            size += subOrgs[i].GetOrgSize();
        }
        return size;
    }
}
