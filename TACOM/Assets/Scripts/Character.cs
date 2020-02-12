using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents a character in the game
public class Character : MonoBehaviour
{
    //dictionary that maps attributes represented as strings to their integer values i.e. "health" -> 100
    Dictionary<string, int> attributes = new Dictionary<string, int>();
    //list of all abilities a character can use
    [SerializeField]
    List<Ability> abilities = new List<Ability>();

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

    public void useAbility(Ability abl, Character target)
    {
        Debug.Log("use");
        abl.applyEffects(target);
    }

    public void handleEffect(ImmediateEffect eff)
    {
        Debug.Log("handle");
        attributes[eff.getAttribute()] += eff.getStrength();
        if (attributes["health"] < 100)
        {
            Debug.Log(100 - attributes["health"] + " damage taken");
            attributes["health"] = 100;
        }
    }
}
