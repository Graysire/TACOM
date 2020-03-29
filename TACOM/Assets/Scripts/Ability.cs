using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents anything a character can do to affect another character
public class Ability
{
    //list of all effects that the ability will apply when used
    List<ImmediateEffect> effects = new List<ImmediateEffect>();

    //the attribute used as a bonus to the ability's attack roll
    CharacterAttributes attackAttribute;
    //the attribute used to set the difficulty of the attack roll
    CharacterAttributes targetAttribute;
    //number of dice used for the attack roll
    int numDice;
    //number of sides each die has
    int diceSides;
    //base difficulty of the roll, added to the target attribute
    int baseDifficulty;
    //range of theability measured in number of tiles
    int range;
    //whether or not the ability requires an unobstructed path to the target
    bool requiresLineOfSight;
    //whether or not this ability is affected by cover
    bool affectedByCover;

    //the name of the ability
    string name;

    //default constructor
    public Ability()
    { }

    //Constructor for a Single Effect Ability
    public Ability(string name, ImmediateEffect eff, CharacterAttributes atk, CharacterAttributes tar, int range, int num = 2, int sides = 10, int baseDif = 11, bool lineOfSight = true, bool affectedByCover = true)
    {
        this.name = name;
        effects.Add(eff);
        attackAttribute = atk;
        targetAttribute = tar;
        numDice = num;
        diceSides = sides;
        baseDifficulty = baseDif;
        this.range = range;
        requiresLineOfSight = lineOfSight;
        this.affectedByCover = affectedByCover;
    }

    //Constructor for Multiple Effect Ability
    public Ability(string name, ImmediateEffect[] eff)
    {
        this.name = name;
        effects.AddRange(eff);
    }

    public void ApplyEffects(int coverValue, CharacterTargetInfo targetInfo)
    {
        int diceRoll = targetInfo.source.GetAttribute(attackAttribute);
        //roll the dice
        for (int i = 0; i < numDice; i++)
        {
            diceRoll += Random.Range(1, diceSides + 1);
        }
        int targetNumber = targetInfo.target.GetAttribute(targetAttribute) + baseDifficulty;
        if (affectedByCover)
        {
            targetNumber += coverValue;
        }

        //adds information to log message: source uses ability on target, rolling diceRoll (XdY+bonus) against targetNumber
        targetInfo.logMessage += targetInfo.source.GetName() + " uses " + name + " on " + targetInfo.target.GetName() +
            ", rolling " + diceRoll + "(" + numDice + "d" + diceSides + "+" + targetInfo.source.GetAttribute(attackAttribute) + ") against target number: " + targetNumber;
        if (affectedByCover)
        {
            targetInfo.logMessage += "(cover: " + coverValue + ")";
        }

        //check if the ability has hit the atrget number
        if (diceRoll >= targetNumber)
        {
            targetInfo.logMessage += ", hitting";
            foreach (ImmediateEffect eff in effects)
            {
                //Debug.Log("apply");
                eff.ApplyEffect(ref targetInfo);
            }
        }
        else
        {
            targetInfo.logMessage += ", missing";
        }
        Debug.Log(targetInfo.logMessage);
    }

    //returns the range of the ability
    public int GetRange()
    {
        return range;
    }

    //returns whether the ability requires line of sight
    public bool GetLineOfSight()
    {
        return requiresLineOfSight;
    }
}
