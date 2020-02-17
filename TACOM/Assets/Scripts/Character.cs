using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a character in the game
public class Character : MonoBehaviour
{
    //dictionary that maps attributes represented as strings to their integer values i.e. "health" -> 100
    Dictionary<string, int> attributes = new Dictionary<string, int>();
    //list of all abilities a character can use
    List<Ability> abilities = new List<Ability>();
    //list of all effects affecting a character
    List<RemovableEffect> activeEffects = new List<RemovableEffect>();

    //delegate that takes in a Character
    public delegate void Tick(Character target);
    //event called every end of turn
    public event Tick OnTick;

    public Character()
    {
        attributes.Add("health", 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        //attributes.Add("health", 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (attributes["health"] < 100)
        {
            Debug.Log(100 - attributes["health"] + " damage taken");
            attributes["health"] = 100;
        }
    }

    //uses abl on target, applying the effects of abl to the target
    public void UseAbility(Ability abl, Character target)
    {
        //Debug.Log("use");
        abl.ApplyEffects(target);
    }

    //increases an attribute att by str
    public void ChangeAttribute(string att, int str)
    {
        attributes[att] += str;
        Debug.Log((attributes[att] - str) + " -> " + attributes[att]);
    }

    //invokes the OnTick event
    public void TickCharacter()
    {
        OnTick?.Invoke(this);
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
        eff.RemoveEffect(this);
    }

}
