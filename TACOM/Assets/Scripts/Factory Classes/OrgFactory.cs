using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/19/2019

//hub class that manages the creation of standard Organizations
public class OrgFactory
{
    //enumeration of all standard Organizations
    public enum OrgType { GCF_ISquad, GCF_ILanceCompanyHQ, GCF_ILanceCompany,GCF_ICompanyHQ, GCF_ICompanyAntiArmorSquad, GCF_ICompanyIndirectFireSquad, GCF_ICompany,
        GCF_LanceRegiment,GCF_Regiment,GCF_LanceCorps,GCF_Corps,GCF_LanceCommand,GCF_Command}

    //returns a new Organization based on the OrgType input
    public static Organization CreateOrg(OrgType type)
    {
        SimpleCharacter[] tempChar;
        Organization[] tempOrg;
        switch (type)
        {
            case OrgType.GCF_ISquad:
                //GCF Sergeant, GCF Corporal, 2 GCF Lance Trooper, 4 GCF Trooper
                tempChar = new SimpleCharacter[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_Corporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper)};
                return new Organization("Infantry Squad", tempChar);
            case OrgType.GCF_ILanceCompanyHQ:
                //GCF Lance Captain, GCF Lance Sergeant Major, 1 GCF Corporal, 2 GCF Lance Corporal, 2 GCF Lance Trooper
                tempChar = new SimpleCharacter[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCaptain), CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant_Major),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Corporal), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper)};
                return new Organization("Infantry Lance Company Headquarters Squad", tempChar);
            case OrgType.GCF_ILanceCompany:
                //GCF Infantry Lance COmpany HQ, 4 GCF Infantry Squad
                tempOrg = new Organization[] {
                    CreateOrg(OrgType.GCF_ILanceCompanyHQ), CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad),
                    CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad) };
                return new Organization("Infantry Lance Company", tempOrg);
            case OrgType.GCF_ICompanyAntiArmorSquad:
                //GCF Lance Sergeant, 2 GCF Lance Corporal, 4 GCF Lance Trooper, 2 GCF Trooper
                tempChar = new SimpleCharacter[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper) };
                return new Organization("Infantry Company Anti-Armor Squad", tempChar);
            case OrgType.GCF_ICompanyIndirectFireSquad:
                //GCF Lance Sergeant, 1 GCF Lance Corporal, 2 GCF Lance Trooper, 2 GCF Trooper
                tempChar = new SimpleCharacter[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper) };
                return new Organization("Infantry Company Indirect Fire Squad", tempChar);
            case OrgType.GCF_ICompanyHQ:
                //GCF Captain, GCF Lance Captain, GCF Lance Sergeant Major, 2 GCF Sergeant, GCF Lance Sergeant, GCF Trooper
                tempChar = new SimpleCharacter[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Captain), CharFactory.CreateChar(CharFactory.CharType.GCF_LCaptain),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant_Major), CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper)};
                return new Organization("Infantry Company Headquarters Squad", tempChar);
            case OrgType.GCF_ICompany:
                //GCF Infantry Company HQ, GCF Infantry Company Anti-Armor Squad, GCF Infantry Company Indirect Fire Squad, 4 GCF Infantry Lance Company
                tempOrg = new Organization[] {
                    CreateOrg(OrgType.GCF_ICompanyHQ), CreateOrg(OrgType.GCF_ICompanyIndirectFireSquad), CreateOrg(OrgType.GCF_ICompanyAntiArmorSquad),
                    CreateOrg(OrgType.GCF_ILanceCompany), CreateOrg(OrgType.GCF_ILanceCompany), CreateOrg(OrgType.GCF_ILanceCompany),
                    CreateOrg(OrgType.GCF_ILanceCompany)};
                return new Organization("Infantry Company", tempOrg);
            default:
                return new Organization();
        }
    }
}


