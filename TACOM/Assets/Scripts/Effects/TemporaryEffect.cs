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
    public TemporaryEffect(string name, CharacterAttributes att, int value, CharacterAttributes powBonus, int durat, float powBonusMultiplier = 0.5f, bool isDmg = true, bool affectedByArmor = true,int num = 0, int sides = 0) 
        : base(name, att, value, powBonus, powBonusMultiplier, isDmg, affectedByArmor, num, sides)
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
    }

    //adds a copy of this effect to the target with a powerApplied of finalPower
    protected override void AddCopy(ref CharacterTargetInfo targetInfo, int finalPower)
    {
        TemporaryEffect temp = new TemporaryEffect(this)
        {
            powerApplied = finalPower
        };
        targetInfo.target.AddEffect(temp);
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
