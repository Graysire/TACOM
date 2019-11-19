using System.Collections;
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
        Debug.Log("Lance COmpany Size: " + o3.GetOrgSize());
        Debug.Log(o3);
    }

}
