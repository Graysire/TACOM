using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Edited: 12/7/19
*/
//represents a combat where an attacker is attacking an attackable
public class Combat
{
    IAttacker attacker; //the attacker
    IAttackable defender; //the defender

    //default constructor
    Combat()
    {
        attacker = new Character();
        defender = new Character();
    }

    //constructor that sets the attacker and defender
    Combat(IAttacker atk, IAttackable dfnd)
    {
        attacker = atk;
        defender = dfnd;
    }

    public void FightRound()
    {
        attacker.Attack(defender);
        if(defender.GetType() == typeof(IAttacker))
        {
            ((IAttacker)defender).Attack(attacker);
        }


    }

}
