using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Temporary Effect that periodically applies one or more effects
public class PeriodicTemporaryEffect : TemporaryEffect
{
    //how often the effect triggers, ex. period = 1 triggers every tick
    protected int period;
    //how many times the effect has triggered (used for reversing the effect)
    protected int timesApplied = 0;
    //whether the effect reverses itself when removing
    //USE WITH CAUTION, may cause issues when used for Non-Immediate Effects
    protected bool reverseOnRemove = false;
    //array of the effects this effect applies periodically
    protected ImmediateEffect[] effects;

    //Default Constructor
    public PeriodicTemporaryEffect() : base()
    {
        period = 1;
    }

    //Constructor with inputs, att for attribute, value for strength, durat for duration, period for period, eff for effects, reverse for reverseOnRemove
    public PeriodicTemporaryEffect(CharacterAttributes att, int value, int durat, int period, ImmediateEffect[] eff, bool reverse) : base(att, value, durat)
    {
        this.period = period;
        effects = eff;
        reverseOnRemove = reverse;
    }

    //override, applies all subeffects and adds TickEffect
    public override void ApplyEffect(CharacterTargetInfo targetInfo)
    {
        foreach (ImmediateEffect eff in effects)
        {
            eff.ApplyEffect(targetInfo);
        }
        timesApplied++;
        targetInfo.target.OnTick += TickEffect;
    }

    //override, only reverses effect is reverseOnRemove is true
    public override void RemoveEffect(CharacterTargetInfo targetInfo)
    {
        targetInfo.target.OnTick -= TickEffect;
    }

    //every tick countsdown the duration, at 0 removes the effect, at period applies Effects
    public override void TickEffect(CharacterTargetInfo targetInfo)
    {
        duration--;
        //check if effects should be applied ,if so apply each effect
        if (duration % period == 0)
        {
            foreach (ImmediateEffect eff in effects)
            {
                eff.ApplyEffect(targetInfo);
            }
            timesApplied++;
        }
        if (duration <= 0)
        {
            targetInfo.target.RemoveEffect(this);
            if (reverseOnRemove) //if it should be reversed on removal
            {
                for (int i = 0; i < timesApplied; i++) //for each time applied
                {
                    foreach (ImmediateEffect eff in effects) //for each effect
                    {
                        //apply the reverse of the effect
                        ImmediateEffect efTemp = new ImmediateEffect(eff.GetAttribute(), eff.GetStrength() * -1);
                        efTemp.ApplyEffect(targetInfo);
                    }
                }
            }
        }
    }
}
