using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 10/31/2019

//central hub for managing the instantiation of all non-monobehaviour/ScriptableObject classes
public class Factory
{
    //type of Organization used in CreateOrg
    public enum OrgType { GCF_ISquad, GCF_ILanceCompanyHQ, GCF_ILanceCompany,GCF_ICompanyHQ, GCF_ICompanyAntiArmorSquad, GCF_ICompanyIndirectFireSquad, GCF_ICompany,GCF_LanceRegiment,GCF_Regiment,GCF_LanceCorps,GCF_Corps,GCF_LanceCommand,GCF_Command}

    //returns a new Organization based on the OrgType input
    public static Organization CreateOrg(OrgType type)
    {
        switch (type)
        {
            case OrgType.GCF_ISquad:
                Debug.Log("Squad");
                //GCF Sergeant, GCF Corporal, 2 GCF Lance Trooper, 4 GCF Trooper
                Character[] temp = new Character[0];
                return new OrgUnit("Squad", temp);
            case OrgType.GCF_ILanceCompanyHQ:
                //GCF Lance Captain, GCF Lance Sergeant Major, 2 GCF Lance Corporal, 2 GCF Lance Trooper, 1 GCF Corporal
                Debug.Log("ILCHQ");
                temp = new Character[0];
                return new OrgUnit("Infantry Lance Company Headquarters Squad", temp);
            case OrgType.GCF_ILanceCompany:
                Debug.Log("ILC");
                Organization[] tempOrg = { CreateOrg(OrgType.GCF_ILanceCompanyHQ), CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad),
                    CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad) };
                return new OrgFormation("Infantry Lance Company", tempOrg);
            default:
                return new OrgFormation();
        }
    }
}


