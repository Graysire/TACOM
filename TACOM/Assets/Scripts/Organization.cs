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
}
