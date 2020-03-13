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
    public TemporaryEffect(string name, CharacterAttributes att, int value, int durat) : base(name, att, value)
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
        //copied from ImmediateEffect's ApplyEffect, base cannot be used due to need to copy this Effect
        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + power;
        targetInfo.target.ChangeAttribute(attribute, power);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";

        //copy this effect
        TemporaryEffect temp = new TemporaryEffect(this);

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
