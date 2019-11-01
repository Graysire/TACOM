using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 10/31/2019

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
}
