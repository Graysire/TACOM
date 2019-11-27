using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Grayson Hill
 * Last Updated: 11/27/19
 */

public class Weapon
{
    private int damage;
    private int maxFireRate;
    private int recoil;
    private int burstNumber;

    public Weapon()
    {
        damage = 0;
        maxFireRate = 1;
        recoil = 0;
        burstNumber = 1;
    }

    public Weapon(int dmg, int maxFire, int recoil, int burst)
    {
        damage = dmg;
        maxFireRate = maxFire;
        this.recoil = recoil;
        burstNumber = burst;
    }

    public int getMaxFireRate()
    {
        return maxFireRate;
    }

    public int getDamage()
    {
        return damage;
    }

    public int getRecoil()
    {
        return recoil;
    }

    public int getBurstNumber()
    {
        return burstNumber;
    }
}
