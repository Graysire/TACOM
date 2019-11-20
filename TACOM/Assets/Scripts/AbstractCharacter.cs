using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/19/2019

//AbstractCharacter has all of the basic methods requried by all Characters
public abstract class AbstractCharacter
{
    protected Organization parentOrg; //the organization the characer belongs to

    public abstract override string ToString();
    public Organization GetParentOrg()
    {
        return parentOrg;
    }

}
