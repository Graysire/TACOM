using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 12/4/2019

//Character represents all characters in the game with full statistics
public class Character : AbstractCharacter
{
    private int rangedSkill; //Character's accuracy in ranged combat
    private int rangedDefense; //Character's ability to dodge ranged attacks
    private int armor; //Character's damage reduction
    private int maxHealth; //Character's maximum health
    private int health; //Character's current health
    private int threat;
    private Weapon weapon; //Character's weapon

    //Default Constructor
    public Character()
    {
        rangedSkill = 0;
        rangedDefense = 0;
        armor = 0;
        maxHealth = 0;
        health = 0;
        threat = 0;
        weapon = new Weapon();
        opsType = AttackableOpsType.Line;
        unitType = AttackableUnitType.Infantry;
    }

    //Constructor giving ranged skill, ranged defense, armor, health, and weapon
    public Character(int rSkill, int rDef, int armr, int hp, Weapon wep, int threat, AttackableOpsType ops = AttackableOpsType.Line, AttackableUnitType unit = AttackableUnitType.Infantry)
    {
        rangedSkill = rSkill;
        rangedDefense = rDef;
        armor = armr;
        maxHealth = hp;
        health = hp;
        weapon = wep;
        this.threat = threat;
        opsType = ops;
        unitType = unit;
    }

    //returns Character as a string
    public override string ToString()
    {
        return "RS = " + rangedSkill + ", RD = " + rangedDefense + ", " + "A: " + armor + ", HP: " + health + "/" + maxHealth + ", W: " + GetWeight();
    }

    //Generic Attack against an attackable target
    public override void Attack(IAttackable target)
    {
        Debug.Log("Character attacking Non-Character");
    }

    //Attack against a Character target
    public void Attack(Character target)
    {
        int usedFireRate = Random.Range(1, weapon.getMaxFireRate()); //the number of shots fired
        //string debug = "UFR: " + usedFireRate;
        for (int i = 0; i < Mathf.Ceil((float)usedFireRate / weapon.getBurstNumber()); i++) //roll an attack for each (shots fired/burst number rounded up)
        {
            //roll 1d100 + skill - target defense - recoil * shots fire - 1
            int roll = Random.Range(1, 101) + rangedSkill - target.rangedDefense - weapon.getRecoil() * (usedFireRate - 1);
            //debug += ", ROLL: " + roll;
            if (roll > 0) //if hit the target
            {
                int damageMod;
                if (roll > weapon.getDamage() * usedFireRate) //if roll is greater than maximum potential extra damage, deal maximum extra damage
                {
                    damageMod = weapon.getDamage() * usedFireRate;
                    //debug += ", MaxDmg: " + damageMod;
                }
                else //otherwise the extra damage is equal to the roll
                {
                    damageMod = roll;
                    //debug += ", Dmg: " + damageMod;
                }
                int damageResult = roll + damageMod - usedFireRate * target.armor; //final damage is roll + damage modifer - target's armor * shots fired
                //debug += ", TOT:" + damageResult;
                if (damageResult > 0) //if any damage is dealt
                {
                    target.TakeDamage(damageResult); //deal damage
                }
            }
        }
        //Debug.Log(debug);

    }

    //Character reduces their health by dmg
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    //returns self
    public override IAttackable GetTarget()
    {
        return this;
    }
    
    //returns threat level
    public override int GetThreat()
    {
        return threat;
    }

    //returns threat level if opType matches ops
    public override int GetThreat(AttackableOpsType ops)
    {
        if (opsType == ops)
        {
            return threat;
        }
        else
        {
            return 0;
        }
    }

    public override int GetThreat(AttackableUnitType unit)
    {
        if (unitType == unit)
        {
            return threat;
        }
        else
        {
            return 0;
        }
    }

    public override string ToStringTabbed(int numTabs)
    {
        return ToString();
    }

    public override int GetWeight()
    {
        return GetThreat(AttackableOpsType.Command) + GetThreat(AttackableOpsType.Logistics) * 3 + GetThreat(AttackableOpsType.Line) * 6;
    }

    public override int GetWeight(AttackableOpsType ops)
    {
        switch (ops)
        {
            case AttackableOpsType.Command:
                return GetThreat(ops);
            case AttackableOpsType.Logistics:
                return GetThreat(ops) * 3;
            case AttackableOpsType.Line:
                return GetThreat(ops) * 6;
            default:
                Debug.Log("GetWeight(ops) Default Case Warning");
                return GetWeight();
        }
    }
}

