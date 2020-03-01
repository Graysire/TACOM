using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A pathfinding grid used for A* Pathfinding calculations
public class PathGrid : MonoBehaviour
{
    //the grid of all pathing nodes
    [SerializeField]
    PathNode[,] nodeGrid;
    //the size of the grid(x will expand up-left, y will expand up-right
    //0,0 is the bottomost point
    [SerializeField]
    Vector2Int gridSize;

    //the grid of tilemaps this pathing grid is attached to
    [SerializeField]
    Grid tileGrid;

    //list of nodes forming a path from one node to another
    public List<PathNode> finalPath = new List<PathNode>();

    void Start()
    {
        //on start create the pathfinding grid
        CreateGrid();
    }

    //creates the pathfinding grid
    void CreateGrid()
    {
        //instantiates the array of nodes
        nodeGrid = new PathNode[gridSize.y, gridSize.x];
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                //fills every space on the node grid with a node, defaulting to non-obstructed and with a simple x and y value
                nodeGrid[y, x] = new PathNode(false, x, y);
            }
        }
    }

    void Update()
    {
        
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
            return nodeGrid[cellLocation.y, cellLocation.x];
        }
    }
    //returns a list of all nodes adjacent to this one
    public List<PathNode> GetAdjacentNodes(PathNode center)
    {
        //create the list of nodes to be returned
        List<PathNode> adjacentNodes = new List<PathNode>();

        //check if the top left node is out of bounds, if not, add it
        if (center.posX + 1 >= 0 && center.posX + 1 < gridSize.x)
        {
            adjacentNodes.Add(nodeGrid[center.posY, center.posX + 1]);
        }
        //check if the bottom left node is out of bounds, if not, add it
        if (center.posX - 1 >= 0 && center.posX - 1 < gridSize.x)
        {
            adjacentNodes.Add(nodeGrid[center.posY, center.posX - 1]);
        }
        //check if the top right node is out of bounds, if not, add it
        if (center.posY + 1 >= 0 && center.posY + 1 < gridSize.y)
        {
            adjacentNodes.Add(nodeGrid[center.posY + 1, center.posX]);
        }
        //check if the bottom left node is out of bounds, if not, add it
        if (center.posY - 1 >= 0 && center.posY - 1 < gridSize.y)
        {
            adjacentNodes.Add(nodeGrid[center.posY - 1, center.posX]);
        }

        return adjacentNodes;
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
                    if (finalPath.Contains(nodeGrid[x, y]))
                    {
                        Gizmos.color = Color.red;
                    }
                    else 
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawSphere(tileGrid.GetCellCenterWorld(new Vector3Int(y, x, 0)), 0.25f);
                }
            }
        }
    }
}
