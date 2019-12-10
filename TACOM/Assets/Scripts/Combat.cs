using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Grayson Hill
 * Last Edited: 12/9/19
*/
//represents a combat where an attacker is attacking an attackable
public class Combat
{
    IAttacker attacker; //the attacker
    IAttackable defender; //the defender

    //default constructor
    public Combat()
    {
        attacker = new Character();
        defender = new Character();
        TickManager.onTick += FightRound;
    }

    //constructor that sets the attacker and defender
    public Combat(IAttacker atk, IAttackable dfnd)
    {
        attacker = atk;
        defender = dfnd;
        TickManager.onTick += FightRound;
        Debug.Log("COMBAT ATTACKER:" + attacker.ToString());
        Debug.Log("COMBAT DEFENDER:" + defender.ToString());
    }

    //goes through combat of a single round
    public void FightRound()
    {
        attacker.Attack(defender); //the attacker attacks
        if(defender.GetType() == typeof(IAttacker)) //if the defender is capable of attacking it attacks back
        {
            ((IAttacker)defender).Attack(attacker);
        }
        //check if either attack or defender is dead
        attacker.CheckIsAlive();
        defender.CheckIsAlive();

        if (!attacker.GetIsAlive() || !defender.GetIsAlive()) //if either is dead, disengage
        {
            attacker.SetInCombat(false);
            defender.SetInCombat(false);
            TickManager.onTick -= FightRound;
            Debug.Log("Combat Ended");
        }
        

    }

}
