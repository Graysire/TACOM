using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A pathfinding node used by our A*pathfinding
public class PathNode
{
    //the x and y positions of this node in the grid (discounting grid size and actual world space coordinates)
    public int gridX;
    public int gridY;
    //whether this node is obstructed
    public bool isObstructed;

    public int gCost;
    public int hCost;
    //the total cost of moving to this node
    public int FCost { get { return gCost + hCost; } }

    //Constructor for the pathfinding node
    public PathNode(bool isObstructed, int gridX, int gridY)
    {
        this.isObstructed = isObstructed;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
