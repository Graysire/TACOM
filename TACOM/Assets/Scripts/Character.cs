using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grayson Hill
//Last Edited: 11/27/2019

//Character represents all characters in the game with full statistics
public class Character : AbstractCharacter
{
    private int rangedSkill;
    private int rangedDefense;
    private int armor;
    private int maxHealth;
    private int health;
    private Weapon weapon;

    public Character()
    {
        rangedSkill = 0;
        rangedDefense = 0;
        armor = 0;
        maxHealth = 0;
        health = 0;
        weapon = new Weapon();
    }

    public Character(int rSkill, int rDef, int armr, int hp, Weapon wep)
    {
        rangedSkill = rSkill;
        rangedDefense = rDef;
        armor = armr;
        maxHealth = hp;
        health = hp;
        weapon = wep;
    }

    public override string ToString()
    {
        return "RS = " + rangedSkill + ", RD = " + rangedDefense + ", " + "A: " + armor + ", HP: " + health + "/" + maxHealth;
    }

    public void Attack(Character target)
    {
        int usedFireRate = Random.Range(1, weapon.getMaxFireRate());
        string temp = "UFR: " + usedFireRate;
        for (int i = 0; i < Mathf.Ceil((float) usedFireRate / weapon.getBurstNumber()); i++)
        {
            int roll = Random.Range(1, 101) + rangedSkill - target.rangedDefense - weapon.getRecoil() * (usedFireRate - 1);
            temp += ", ROLL: " + roll;
            if (roll > 0)
            {
                int damageMod;
                if (roll > weapon.getDamage() * usedFireRate)
                {
                    damageMod = weapon.getDamage() * usedFireRate;
                    temp += ", MaxDmg: " + damageMod;
                }
                else
                {
                    damageMod = roll;
                    temp += ", Dmg: " + damageMod;
                }
                int damageResult = roll + damageMod - usedFireRate * target.armor;
                temp += ", TOT:" + damageResult;
                if (damageResult > 0)
                {
                    target.health -= damageResult;

                }
            }
        }
        Debug.Log(temp);
    }


}

