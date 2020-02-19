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
    public TemporaryEffect(string att, int value, int durat) : base(att, value)
    {
        duration = durat;
    }

    //override, adds this effect to the character's active effects and to the OnTick event
    public override void ApplyEffect(Character target)
    {
        base.ApplyEffect(target);
        target.OnTick += TickEffect;
    }

    //override removes the effect and removes self from the OnTick event
    public override void RemoveEffect(Character target)
    {
        base.RemoveEffect(target);
        target.OnTick -= TickEffect;
    }

    //every tick countsdown the duration, at 0 removes the effect
    public virtual void TickEffect(Character target)
    {
        duration--;
        if (duration <= 0)
        {
            target.RemoveEffect(this);
        }
    }
}
