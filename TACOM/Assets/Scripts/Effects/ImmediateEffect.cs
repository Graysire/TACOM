using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents an event that applies an immediate change to a character
[System.Serializable]
public class ImmediateEffect
{
    //the name of the attribute to be affected
    [SerializeField]
    protected CharacterAttributes attribute;
    //the based numerical value the attribute will be changed by
    [SerializeField]
    protected int power;

    //Source character's attribute that provide a bonus to the power
    [SerializeField]
    CharacterAttributes powerBonus;
    //multiplier to powerBonus
    [SerializeField]
    float powerBonusMultiplier;

    //number of dice rolled to determine the final power ofthe Effect
    [SerializeField]
    protected int numDice;
    //number ofsides each die has
    [SerializeField]
    protected int diceSides;

    //if true, power is subtracted from attribute, otherwise damage is added
    [SerializeField]
    protected bool isDamage;
    //if true, armor is subtracted from power before damage is added
    [SerializeField]
    protected bool isAffectedByArmor;

    //the name of the effect
    [SerializeField]
    protected string name;

    //Default Constructor, defaults to affecting health with a strength of 0
    public ImmediateEffect()
    {
        name = "WorldEffect";
        attribute = CharacterAttributes.Health;
        power = 0;
    }

    //Constructor with inputs, att for attribute and value for strength
    public ImmediateEffect(string name, CharacterAttributes att, int value, CharacterAttributes powBonus, float powBonusMultiplier = 0.5f, bool isDmg = true, bool affectedByArmor = true, int num = 1, int sides = 10)
    {
        this.name = name;
        attribute = att;
        power = value;
        powerBonus = powBonus;
        powerBonusMultiplier = powBonusMultiplier;
        numDice = num;
        diceSides = sides;
        isDamage = isDmg;
        isAffectedByArmor = affectedByArmor;
    }

    //returns the attribute this Effect affects
    public CharacterAttributes GetAttribute()
    {
        return attribute;
    }

    //returns the strength of this Effect
    public int GetPower()
    {
        return power;
    }

    //returns whether this effect is a damaging effect
    public bool GetIsDamage()
    {
        return isDamage;
    }

    //applies this effect to the target, modifying target attribute by power 
    public virtual int ApplyEffect(ref CharacterTargetInfo targetInfo)
    {
        //initial finalPower is the base power - the target's armor if this effect is affected by armor
        int finalPower = power + (int) (targetInfo.source.GetAttribute(powerBonus) * powerBonusMultiplier) - (isAffectedByArmor?targetInfo.target.GetAttribute(CharacterAttributes.Armor):0);
        for (int i = 0; i < numDice; i++)
        {
            finalPower += Random.Range(1, diceSides + 1);
        }
        //ensures finalPower is never negative
        if (finalPower < 0)
        {
            finalPower = 0;
        }
        int rolledPower = finalPower;

        //doubles all damage dealt in excess of Endurance, assuming isDamage and target attribute is Health
        if (isDamage && attribute == CharacterAttributes.Health && finalPower > targetInfo.target.GetAttribute(CharacterAttributes.Endurance))
        {
            finalPower += finalPower - targetInfo.target.GetAttribute(CharacterAttributes.Endurance);
        }

        targetInfo.logMessage += "\n\t" + name + " applied, " + targetInfo.target.GetName() + "'s " + attribute + " changes by " + (isDamage?-1 * finalPower:finalPower) + 
            "(rolled " + rolledPower + " on " + numDice + "d" + diceSides + "+" + (power + (int) (targetInfo.source.GetAttribute(powerBonus) * 0.5f)) + (isAffectedByArmor?"-" + targetInfo.target.GetAttribute(CharacterAttributes.Armor):"") + ")";
        
        //if isDamage, change the target attribute by -1* finalPower, otherwise change it by finalPower
        targetInfo.target.ChangeAttribute(attribute, isDamage?-1 *finalPower:finalPower);
        targetInfo.logMessage += "(now: " + targetInfo.target.GetAttribute(attribute) + ")";
        //Debug.Log(targetInfo.logMessage);

        return finalPower;
    }

}
