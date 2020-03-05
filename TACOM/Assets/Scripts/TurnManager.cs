using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages which character controllers are selected and can act by taking turns
public class TurnManager : MonoBehaviour
{
    //the list of all character controllers in the scene
    List<CharacterManager> turnList = new List<CharacterManager>();
    int currentTurn = -1; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if hitting enter, end turn, deselect current character, select next character
        if (Input.GetKeyDown(KeyCode.Return))
        {
            turnList[currentTurn].ToggleActiveTurn();
            if (currentTurn + 1 == turnList.Count)
            {
                currentTurn = 0;
            }
            else
            {
                currentTurn++;
            }
            turnList[currentTurn].ToggleActiveTurn();
        }

        if (currentTurn < 0)
        {
            currentTurn = 0;
            turnList[0].ToggleActiveTurn();
        }
    }

    //adds a CharacterController to the end of the turn list
    public void AddTurn(CharacterManager character)
    {
        Debug.Log(character.cName + " Turn Added");
        turnList.Add(character);
    }
    public void RemoveTurn(CharacterManager character)
    {
        Debug.Log(character.cName + " Turn Removed");
        for (int i = 0; i < turnList.Count; i++)
        {
            if (turnList[i] == character)
            {
                turnList.RemoveAt(i);
                if (i < currentTurn)
                {
                    currentTurn--;
                }
            }
        }
    }
}
