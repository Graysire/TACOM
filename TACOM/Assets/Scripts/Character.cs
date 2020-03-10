using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a character in the game
public class Character
{
    //Array of all attribute values for a character accessible by the attributes' enum
    int[] attributes = new int[System.Enum.GetValues(typeof(CharacterAttributes)).Length];
    //list of all abilities a character can use
    public List<Ability> abilities = new List<Ability>();
    //list of all effects affecting a character
    List<RemovableEffect> activeEffects = new List<RemovableEffect>();

    //the display name of the character
    string charName = "World";
    //the next ID assigned to uniquely track a character
    static int nextID = 1;
    //the unique ID used to track this character
    int charID;

    //delegate that takes in a Character
    public delegate void Tick(CharacterTargetInfo target);
    //event called every end of turn
    public event Tick OnTick;

    //default constructor
    public Character()
    {
        charID = nextID;
        nextID++;

        

    }

    //constructor with custom name
    public Character(string name)
    {
        this.charName = name;
        attributes[(int)CharacterAttributes.Health] = 100;
        abilities.Add(new Ability("Default Attack", new ImmediateEffect("Default Effect",CharacterAttributes.Health, -30)));
        ImmediateEffect[] arr = { new ImmediateEffect("Default Poison Tick Effect",CharacterAttributes.Health, -20) };
        abilities.Add(new Ability("Default Poison Attack", new PeriodicTemporaryEffect("Default Poison Effect", CharacterAttributes.Health, -20, 2, 1, arr, false)));
        charID = nextID;
        nextID++;
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //attributes.Add("health", 100);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (attributes["health"] < 100)
    //    {
    //        Debug.Log(100 - attributes["health"] + " damage taken");
    //        attributes["health"] = 100;
    //    }
    //}

    //uses abl on target, applying the effects of abl to the target
    public void UseAbility(Ability abl, CharacterTargetInfo targetInfo)
    {
        //Debug.Log("use");
        abl.ApplyEffects(targetInfo);
    }

    //increases an attribute att by str
    public void ChangeAttribute(CharacterAttributes att, int str)
    {
        attributes[(int) att] += str;
        //Debug.Log(charName + " " + (attributes[(int)att] - str) + " -> " + attributes[(int)att]);
        if (attributes[(int)CharacterAttributes.Health] <= 0)
        {
            Debug.Log(charName + " has been slain");
        }
    }

    //invokes the OnTick event
    public void TickCharacter()
    {
        OnTick?.Invoke(new CharacterTargetInfo(this,this));
    }

    //adds an effect to the list of active effects
    public void AddEffect(RemovableEffect eff)
    {
        activeEffects.Add(eff);
    }

    //removes an effect from the list of active effects
    public void RemoveEffect(RemovableEffect eff)
    {
        activeEffects.Remove(eff);
        eff.RemoveEffect(new CharacterTargetInfo(this,this));
    }

    //returns the value of a given attribute
    public int GetAttribute(CharacterAttributes att)
    {
        //casts the given attribute to an int and uses that 
        //as the location of the value in the attributes array
        return attributes[(int) att];
    }

    //reyturns the character's name
    public string GetName()
    {
        return charName;
    }

}

//enumeration of all attributes a character has
public enum CharacterAttributes
{
    Strength, Agility, Toughness, Perception, Willpower, Presence,
    Health
}

//struct containing data relating to Characters targeting other Characters
public struct CharacterTargetInfo
{
    public Character source;
    public Character target;
    public string logMessage;

    public CharacterTargetInfo(Character src, Character targ)
    {
        source = src;
        target = targ;
        logMessage = "";
    }
}
