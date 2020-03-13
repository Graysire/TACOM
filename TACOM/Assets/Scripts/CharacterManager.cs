using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used for controlling a character
public class CharacterManager : MonoBehaviour
{
    //the character this script controls
    [SerializeField]
    Character character;
    //the pathfinding grid used for this character
    PathGrid pathGrid;

    public int[] attributes = new int[System.Enum.GetValues(typeof(CharacterAttributes)).Length - 1];

    //name of the character this controller contains
    public string cName = "World";

    //time between each movement when moving tile by tile
    public float timeDelay = 0.25f;

    //whether or not this character's turn is active
    bool turnIsActive = false;
    //whether or not the character is ending their turn
    bool isEndingTurn = false;

    //whether the character has moved or attacked this turn
    bool hasMoved = false;
    bool hasAttacked = false;

    static CharacterManager target;

    private void Start()
    {
        //set starting position to obstructed
        pathGrid.WorldToNode(transform.position).isObstructed = true;
    }

    private void Awake()
    {
        //gets the scene's pathgrid
        pathGrid = GameObject.Find("PathManager").GetComponent<PathGrid>();
        //adds this character to the turn list
        GameObject.Find("TurnManager").GetComponent<TurnManager>().AddTurn(this);


        character = new Character(cName, attributes);
        attributes = character.GetAttributes();
        
    }


    // Update is called once per frame
    private void Update()
    {
        //if it is this character's turn, they have not attack, they have a target and the 1 key is pressed
        if (turnIsActive && target != null && !hasAttacked && Input.GetKeyDown(KeyCode.Alpha1))
        {
            //attack with ability 1
            character.UseAbility(character.abilities[0], new CharacterTargetInfo(character, target.character));
            hasAttacked = true;
        }
        else if (turnIsActive && target != null && !hasAttacked && Input.GetKeyDown(KeyCode.Alpha2))
        {
            //if the 2 key is pressed attack with ability two
            character.UseAbility(character.abilities[1], new CharacterTargetInfo(character, target.character));
            hasAttacked = true;
        }
        //else if (turnIsActive && target != null && !hasAttacked && Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    //if the 3 key is pressed attack with ability three
        //    character.UseAbility(character.abilities[2], new CharacterTargetInfo(character, target.character));
        //    hasAttacked = true;
        //}
        //if the character has not mvoed yet and the right mouse buttonis clicked, move
        else if (turnIsActive && !hasMoved && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }

        //if the character is slain, destroy this object
        if (character.GetAttribute(CharacterAttributes.Health) <= 0)
        {
            //remove turn from the turn list
            GameObject.Find("TurnManager").GetComponent<TurnManager>().RemoveTurn(this);
            //unobstruct the pathing node at the current position
            pathGrid.WorldToNode(transform.position).isObstructed = false;
            //destroy self
            GameObject.Destroy(this.gameObject);
        }
    }

    //move from the current point ot the target point
    IEnumerator MoveToPoint(Vector3 target)
    {
        //find a path
        pathGrid.getFinalPath(transform.position, target);
        //if a path exists move to it
        if (pathGrid.finalPath.Count != 0)
        {
            hasMoved = true;
            //store the final node in case the coroutine needs to end early
            PathNode finalNode = pathGrid.finalPath[pathGrid.finalPath.Count - 1];
            for (int i = 1; i < pathGrid.finalPath.Count; i++)
            {
                //if we are not ending turn
                if (!isEndingTurn)
                {
                    //move to next point and wait for the timeDelay
                    transform.position = pathGrid.NodeToWorld(pathGrid.finalPath[i]);
                    yield return new WaitForSeconds(timeDelay);
                }
                else
                {
                    //otherwise break
                    break;
                }
            }
            //set the position to that of the final node in case movement was skipped by ending turn
            transform.position = pathGrid.NodeToWorld(finalNode);
        }
        else
        {
            Debug.Log("No path exists to target point");
        }
    }


    //changes whether this character's turn is active
    public void ToggleActiveTurn()
    {
        if (turnIsActive)
        {
            isEndingTurn = true;
            character.TickCharacter();
            hasMoved = false;
            hasAttacked = false;
            pathGrid.finalPath = new List<PathNode>();
            Debug.Log(cName + "'s turn has ended");
        }
        else
        {
            isEndingTurn = false;
            Debug.Log(cName + "'s turn has begun");
        }
        turnIsActive = !turnIsActive;
    }

    private void OnMouseOver()
    {
        target = this;
    }

    private void OnMouseExit()
    {
        target = null;
    }
}
