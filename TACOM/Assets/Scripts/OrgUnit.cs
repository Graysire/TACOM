using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 10/31/2019

//Organization that is composed of characters, if the Organization hierarchy is a tree, this is the leaves
public class OrgUnit : Organization
{
    private Character[] subChars; //characters that are a aprt of this organization
    public OrgUnit()
    {
        orgName = "Default";
        subChars = new Character[0];
        Debug.Log("Default");
    }

    public OrgUnit(string name, Character[] cha)
    {
        orgName = name;
        subChars = cha;
    }
    public OrgUnit(OrgUnit other) //copy constructor
    {
        Debug.Log("Copy");
        orgName = other.orgName;
        subChars = new Character[other.subChars.Length];
        for (int i = 0; i < other.subChars.Length; i++)
        {
            subChars[i] = other.subChars[i];
        }
    }
}
