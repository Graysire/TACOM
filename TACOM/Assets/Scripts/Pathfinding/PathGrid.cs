﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A pathfinding grid used for A* Pathfinding calculations
public class PathGrid : MonoBehaviour
{
    //the grid of all pathing nodes
    PathNode[,] nodeGrid;
    //the size of the grid(x will expand up-left, y will expand up-right
    //0,0 is the bottomost point
    [SerializeField]
    Vector2Int gridSize;
    Pathfinder pathfinder;

    List<GameObject> debugObstructions = new List<GameObject>();
    public GameObject debugObstruction;

    //the grid of tilemaps this pathing grid is attached to
    [SerializeField]
    Grid tileGrid;

    //list of nodes forming a path from one node to another
    public List<PathNode> finalPath = new List<PathNode>();

    private void Awake()
    {
        //on start create the pathfinding grid
        CreateGrid();
        //get the grid
        tileGrid = GameObject.Find("Grid").GetComponent<Grid>();
        //get the local pathfinder
        pathfinder = new Pathfinder(this);
    }

    // Update is called once per frame
    void Update()
    {
        ////debug that sets the start location of a path
        if (Input.GetMouseButtonDown(0))
        {
            PathNode c = WorldToNode(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(c.posX + " " + c.posY);
        }
        ////debug that sets the target location of a path
        //if (Input.GetMouseButtonDown(1))
        //{ 
        //    debugTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        //debug to find a path between two points
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    FindPath(debugStart, debugTarget);
        //}
        //debug to toggle point obstruction
        if (Input.GetKeyDown(KeyCode.O))
        {
            PathNode p = WorldToNode(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (p != null)
            {
                p.isMoveObstructed = !p.isMoveObstructed;
                p.isSightObstructed = !p.isSightObstructed;
                if (p.isMoveObstructed)
                {
                    //if the tile is now obstructed add the debug object to show its obstruction
                    GameObject ob = Instantiate(debugObstruction, NodeToWorld(p), new Quaternion());
                    debugObstructions.Add(ob);
                    //reset cover bonus
                    p.coverBonus = Random.Range(1, 5);
                    Debug.Log(p.coverBonus);
                }
                else
                {
                    //otherwise remove the obstruction at that point
                    Vector3 loc = NodeToWorld(p);
                    for (int i = 0; i < debugObstructions.Count; i++)
                    {
                        if (debugObstructions[i].transform.position == loc)
                        {

                            Destroy(debugObstructions[i]);
                            debugObstructions.RemoveAt(i);
                            p.coverBonus = 0;
                            return;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Target Point does not exist");
            }
        }

    }

    //creates the pathfinding grid
    void CreateGrid()
    {
        //instantiates the array of nodes
        nodeGrid = new PathNode[gridSize.x, gridSize.y];
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                //fills every space on the node grid with a node, defaulting to non-obstructed and with a simple x and y value
                nodeGrid[x, y] = new PathNode(false, x, y);
            }
        }
    }

    //gets the pathfinding node that corresponds to a given point in world space
    public PathNode WorldToNode(Vector3 worldPos)
    {
        Vector3Int cellLocation = tileGrid.WorldToCell(worldPos);
        if (cellLocation.x >= gridSize.x || cellLocation.x < 0 || cellLocation.y >= gridSize.y || cellLocation.y < 0)
        {
            return null;
        }
        else
        {
            return nodeGrid[cellLocation.x, cellLocation.y];
        }
    }

    public Vector3 NodeToWorld(PathNode node)
    {
        return tileGrid.CellToWorld(new Vector3Int(node.posX, node.posY, 0)) + new Vector3(0, tileGrid.cellSize.y /2,0);
    }

    //returns a list of pathing nodes creating the path between two points with a max length
    public List<PathNode> getFinalPath(Vector3 startPos, Vector3 targetPos, int maxLength)
    {
        pathfinder.FindPath(startPos, targetPos, maxLength);
        return finalPath;
    }

    //returns whether or not a line of sight path can be created between two targets
    public bool checkLineOfSight(Vector3 startPos, Vector3 targetPos, int maxLength)
    {
        return pathfinder.FindSightPath(startPos, targetPos, maxLength);
    }


    //returns a list of all nodes cardinally adjacent to this one
    //public List<PathNode> GetAdjacentNodes(PathNode center)
    //{
    //    //create the list of nodes to be returned
    //    List<PathNode> adjacentNodes = new List<PathNode>();

    //    //check if the top left node is out of bounds, if not, add it
    //    if (center.posX + 1 >= 0 && center.posX + 1 < gridSize.x)
    //    {
    //        adjacentNodes.Add(nodeGrid[center.posX + 1, center.posY]);
    //    }
    //    //check if the bottom left node is out of bounds, if not, add it
    //    if (center.posX - 1 >= 0 && center.posX - 1 < gridSize.x)
    //    {
    //        adjacentNodes.Add(nodeGrid[center.posX - 1, center.posY]);
    //    }
    //    //check if the top right node is out of bounds, if not, add it
    //    if (center.posY + 1 >= 0 && center.posY + 1 < gridSize.y)
    //    {
    //        adjacentNodes.Add(nodeGrid[center.posX, center.posY + 1]);
    //    }
    //    //check if the bottom left node is out of bounds, if not, add it
    //    if (center.posY - 1 >= 0 && center.posY - 1 < gridSize.y)
    //    {
    //        adjacentNodes.Add(nodeGrid[center.posX, center.posY - 1]);
    //    }

    //    return adjacentNodes;
    //}

    //returns a list of all nodes adjacent to this one, if includeDiagonals is true then include diagonals
    public List<PathNode> GetAdjacentNodes(PathNode center, bool includeDiagnols = false)
    {
        //create the list of nodes to be returned
        List<PathNode> adjacentNodes = new List<PathNode>();

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (center.posX + x >= 0 && center.posX + x < gridSize.x && center.posY + y >= 0 && center.posY + y < gridSize.y)
                {
                    if (!includeDiagnols && x != y && x != y * -1)
                    {
                        adjacentNodes.Add(nodeGrid[center.posX + x, center.posY + y]);
                    }
                    else if (includeDiagnols && !(x == 0) && !(y == 0))
                    {
                        Debug.Log((center.posX + x) + " " + (center.posY + y));
                        adjacentNodes.Add(nodeGrid[center.posX + x, center.posY + y]);
                    }
                }
            }
        }
        return adjacentNodes;
    }

    //returns the cover value adjacent to the target that is closest to the source
    public int GetCoverValue(Vector3 source, Vector3 target)
    {
        //get the node forms of the given locations
        PathNode sourceNode = WorldToNode(source);
        PathNode targetNode = WorldToNode(target);

        //set the distance from the target to source
        pathfinder.setHCost(targetNode, sourceNode, false);

        //initialize the lowest H vlaue so far
        float lowH = Mathf.Infinity;
        //the node with the lowest h cost so far
        PathNode lowNode = targetNode;

        //check each adjacent node
        foreach (PathNode node in GetAdjacentNodes(targetNode))
        {
            //set the distance between the adjacent node and the source
            pathfinder.setHCost(node, sourceNode, false);
            //if the hCost is the lowest so far
            if (node.hCost < lowH)
            {
                //set the lowest values
                lowNode = node;
                lowH = node.hCost;
            }
            else if (node.hCost == lowH) //if it is equidistant take the higher of the two cover values
            {
                if (node.coverBonus > lowNode.coverBonus)
                {
                    return node.coverBonus;
                }
                else
                {
                    return lowNode.coverBonus;
                }
            }
        }
        //return the cover value of the closest node
        return lowNode.coverBonus;
    }

    void OnDrawGizmos()
    {
        //draws small spheres representing each tile
        if (nodeGrid != null)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    if (finalPath.Count > 0 && finalPath[finalPath.Count - 1] == nodeGrid[x, y])
                    {
                        Gizmos.color = Color.blue;
                    }
                    else if (finalPath.Count > 0 && finalPath[0] == nodeGrid[x, y])
                    {
                        Gizmos.color = Color.black;
                    }
                    else if (finalPath.Contains(nodeGrid[x, y]))
                    {
                        Gizmos.color = Color.red;
                    }
                    else if (nodeGrid[x, y].isMoveObstructed)
                    {
                        Gizmos.color = Color.magenta;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawSphere(tileGrid.GetCellCenterWorld(new Vector3Int(x, y, 0)), 0.25f);
                }
            }
        }
    }
}
