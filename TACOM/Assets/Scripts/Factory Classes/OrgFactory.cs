using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/4/2019

//hub class that manages the creation of standard Organizations
public class OrgFactory
{
    //enumeration of all standard Organizations
    public enum OrgType { GCF_ISquad, GCF_ILanceCompanyHQ, GCF_ILanceCompany,GCF_ICompanyHQ, GCF_ICompanyAntiArmorSquad, GCF_ICompanyIndirectFireSquad, GCF_ICompany,
        GCF_LanceRegiment,GCF_Regiment,GCF_LanceCorps,GCF_Corps,GCF_LanceCommand,GCF_Command}

    //returns a new Organization based on the OrgType input
    public static Organization CreateOrg(OrgType type)
    {
        IAttacker[] tempAttack;
        switch (type)
        {
            case OrgType.GCF_ISquad:
                //GCF Sergeant, GCF Corporal, 2 GCF Lance Trooper, 4 GCF Trooper
                tempAttack = new IAttacker[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_Corporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper)};
                return new Organization("Infantry Squad", tempAttack, AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ILanceCompanyHQ:
                //GCF Lance Captain, GCF Lance Sergeant Major, 2 GCF Lance Corporal, 2 GCF Lance Trooper, GCF Trooper
                tempAttack = new IAttacker[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCaptain, AttackableBase.AttackableOpsType.Command), CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant_Major, AttackableBase.AttackableOpsType.Command),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper, AttackableBase.AttackableOpsType.Logistics), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper)};
                return new Organization("Infantry Lance Company Headquarters Squad", tempAttack, AttackableBase.AttackableOpsType.Command, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ILanceCompany:
                //GCF Infantry Lance COmpany HQ, 4 GCF Infantry Squad
                tempAttack = new IAttacker[] {
                    CreateOrg(OrgType.GCF_ILanceCompanyHQ), CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad),
                    CreateOrg(OrgType.GCF_ISquad), CreateOrg(OrgType.GCF_ISquad) };
                return new Organization("Infantry Lance Company", tempAttack, AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ICompanyAntiArmorSquad:
                //GCF Lance Sergeant, 2 GCF Lance Corporal, 4 GCF Lance Trooper, 2 GCF Trooper
                tempAttack = new IAttacker[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper) };
                return new Organization("Infantry Company Anti-Armor Squad", tempAttack, AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ICompanyIndirectFireSquad:
                //GCF Lance Sergeant, 1 GCF Lance Corporal, 2 GCF Lance Trooper, 2 GCF Trooper
                tempAttack = new IAttacker[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant), CharFactory.CreateChar(CharFactory.CharType.GCF_LCorporal),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper), CharFactory.CreateChar(CharFactory.CharType.GCF_LTrooper),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper), CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper) };
                return new Organization("Infantry Company Indirect Fire Squad", tempAttack, AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ICompanyHQ:
                //GCF Captain, GCF Lance Captain, GCF Lance Sergeant Major, 2 GCF Sergeant, GCF Lance Sergeant, GCF Trooper
                tempAttack = new IAttacker[] {
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Captain, AttackableBase.AttackableOpsType.Command), CharFactory.CreateChar(CharFactory.CharType.GCF_LCaptain, AttackableBase.AttackableOpsType.Command),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant_Major, AttackableBase.AttackableOpsType.Command), CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant, AttackableBase.AttackableOpsType.Logistics),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Sergeant, AttackableBase.AttackableOpsType.Logistics), CharFactory.CreateChar(CharFactory.CharType.GCF_LSergeant, AttackableBase.AttackableOpsType.Logistics),
                    CharFactory.CreateChar(CharFactory.CharType.GCF_Trooper, AttackableBase.AttackableOpsType.Logistics)};
                return new Organization("Infantry Company Headquarters Squad", tempAttack, AttackableBase.AttackableOpsType.Command, AttackableBase.AttackableUnitType.Infantry);
            case OrgType.GCF_ICompany:
                //GCF Infantry Company HQ, GCF Infantry Company Anti-Armor Squad, GCF Infantry Company Indirect Fire Squad, 4 GCF Infantry Lance Company
                tempAttack = new IAttacker[] {
                    CreateOrg(OrgType.GCF_ICompanyHQ), CreateOrg(OrgType.GCF_ICompanyIndirectFireSquad), CreateOrg(OrgType.GCF_ICompanyAntiArmorSquad),
                    CreateOrg(OrgType.GCF_ILanceCompany), CreateOrg(OrgType.GCF_ILanceCompany), CreateOrg(OrgType.GCF_ILanceCompany),
                    CreateOrg(OrgType.GCF_ILanceCompany)};
                return new Organization("Infantry Company", tempAttack, AttackableBase.AttackableOpsType.Line, AttackableBase.AttackableUnitType.Infantry);
            default:
                return new Organization();
        }
    }
}


