using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tester for classes that do not rely on Monobehaviour or ScriptableObject
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Organization o1 = Factory.CreateOrg(Factory.OrgType.GCF_ISquad);
        Organization o2 = Factory.CreateOrg(Factory.OrgType.GCF_ILanceCompanyHQ);
        Organization o3 = Factory.CreateOrg(Factory.OrgType.GCF_ILanceCompany);
    }

}
