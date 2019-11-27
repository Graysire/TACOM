﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tester for classes that do not rely on Monobehaviour or ScriptableObject
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Organization o1 = OrgFactory.CreateOrg(OrgFactory.OrgType.GCF_ISquad);
        Organization o2 = OrgFactory.CreateOrg(OrgFactory.OrgType.GCF_ILanceCompanyHQ);
        Organization o3 = OrgFactory.CreateOrg(OrgFactory.OrgType.GCF_ILanceCompany);
        Organization o4 = OrgFactory.CreateOrg(OrgFactory.OrgType.GCF_ICompany);
        Debug.Log("Company Size: " + o4.GetOrgSize());
        Debug.Log(o4);

        Character c1 = CharFactory.CreateChar(new SimpleCharacter(CharFactory.CharType.GCF_Trooper));
        Debug.Log(c1);
        Character c2 = CharFactory.CreateChar(new SimpleCharacter(CharFactory.CharType.GCF_Trooper));
        Debug.Log(c2);
        c1.Attack(c2);
        Debug.Log(c1);
        Debug.Log(c2);

    }

}
