using System.Collections;
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
        pathfinder = GetComponent<Pathfinder>();
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
                nodeGrid[y, x] = new PathNode(false, x, y, false);
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
            return nodeGrid[cellLocation.y, cellLocation.x];
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

    //returns whether the target position is in line of sight from the start position
    public bool CheckLineOfSight(Vector3 startPos, Vector3 targetPos, int maxLength)
    {
        List<PathNode> sightPath = pathfinder.FindSightPath(startPos, targetPos, maxLength);
        //finalPath = sightPath;
        if (sightPath != null)
        {
            return true;
        }
        else
        {
            return false;
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
                    Gizmos.DrawSphere(tileGrid.GetCellCenterWorld(new Vector3Int(y, x, 0)), 0.25f);
                }
            }
        }
    }
}
