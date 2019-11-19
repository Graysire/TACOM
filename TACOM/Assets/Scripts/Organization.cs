using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 10/31/2019

//Represents a grouping of individuals or organizations within a faction
public abstract class Organization
{
    protected string orgName;
    //protected Faction orgFaction;
    public Organization()
    {
        orgName = "Default";
    }

    public abstract override string ToString(); //returns Organization as a string
    public abstract string ToStringTabbed(int numTabs); //returns Organization as a string with tabs to create a visibile hierarchy
    public abstract int GetOrgSize(); //returns the size of the org measured by number of characters

}
