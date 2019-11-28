using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/27/2019

//AbstractCharacter has all of the basic methods requried by all Characters
public abstract class AbstractCharacter : AttackerBase
{
    protected Organization parentOrg; //the organization the characer belongs to

    public abstract override string ToString();
    public Organization GetParentOrg()
    {
        return parentOrg;
    }
    public void setParentOrg(Organization parent)
    {
        parentOrg = parent;
    }

}
