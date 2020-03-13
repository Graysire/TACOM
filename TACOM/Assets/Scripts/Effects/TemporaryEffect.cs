using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an effect that lasts for a number of ticks before expiring
public class TemporaryEffect : RemovableEffect
{
    //how many ticks the effect lasts
    protected int duration;

    //Default Constructor
    public TemporaryEffect() : base()
    {
        duration = 1;
    }

    //Constructor with inputs, att for attribute, value for strength, durat for duration
    public TemporaryEffect(string name, CharacterAttributes att, int value, int durat, bool isDmg = true, bool affectedByArmor = true,int num = 0, int sides = 0) : base(name, att, value, isDmg, affectedByArmor, num, sides)
    {
        duration = durat;
    }

    //copy constructor
    public TemporaryEffect(TemporaryEffect other)
    {
        name = other.name;
        attribute = other.attribute;
        power = other.power;
        duration = other.duration;
        Debug.Log("Copy");
    }

    //override, adds this effect to the character's active effects and to the OnTick event
    public override void ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        //initial finalPower is the base power - the target's armor if this effect is affected by armor
        int finalPower = power - (isAffectedByArmor ? targetInfo.target.GetAttribute(CharacterAttributes.Armor) : 0);
        for (int i = 0; i < numDice; i++)
        {
            finalPower += Random.Range(1, diceSides + 1);
        }
        //ensures finalPower is never negative
        if (finalPower < 0)
        {
            finalPower = 0;
        }

        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (isDamage ? -1 * finalPower : finalPower) +
            "(" + numDice + "d" + diceSides + "+" + power + (isAffectedByArmor ? "-" + targetInfo.target.GetAttribute(CharacterAttributes.Armor) : "") + ")";

        targetInfo.target.ChangeAttribute(attribute, isDamage ? -1 * finalPower : finalPower);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";

        //copy this effect
        TemporaryEffect temp = new TemporaryEffect(this)
        {
            powerApplied = finalPower
        };

        //add the effect's copy to the target's active effects
        targetInfo.target.AddEffect(temp);
        //add the copy to the target's OnTick
        targetInfo.target.OnTick += temp.TickEffect;
    }

    //override removes the effect and removes self from the OnTick event
    public override void RemoveEffect(CharacterTargetInfo targetInfo)
    {
        base.RemoveEffect(targetInfo);
        targetInfo.target.OnTick -= TickEffect;
    }

    //every tick countsdown the duration, at 0 removes the effect
    public virtual void TickEffect(CharacterTargetInfo targetInfo)
    {
        duration--;
        targetInfo.logMessage += name + " ticked, " + duration + " ticks remaining";
        Debug.Log(targetInfo.logMessage);
        if (duration <= 0)
        {
            targetInfo.target.RemoveEffect(this);
            //emoveEffect(targetInfo);
        }
        
        
    }
}
