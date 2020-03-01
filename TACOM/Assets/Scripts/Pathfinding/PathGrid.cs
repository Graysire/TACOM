using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A pathfinding grid used for A* Pathfinding calculations
public class PathGrid : MonoBehaviour
{
    //the grid of all pathing nodes
    public PathNode[,] nodeGrid;
    //the size of the grid(x will expand up-left, y will expand up-right
    //0,0 is the bottomost point
    public Vector2Int gridSize;

    //the grid of tilemaps this pathing grid is attached to
    public Grid tileGrid;

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
        //debug that displays tile coordinates of left click
        if (Input.GetMouseButtonDown(0))
        {//converts the location of the mouse to a tile's cell position
            Vector3Int mouseLocation = tileGrid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //if this position exceeds the bounds of the grid, not the tile is out of bounds
            if (mouseLocation.x >= gridSize.x || mouseLocation.y >= gridSize.y || mouseLocation.x < 0 || mouseLocation.y < 0)
            {
                Debug.Log("Tile Out of Bounds");
            }
            //else print the location of the tile
            else
            {
                Debug.Log("Tile At: " + nodeGrid[mouseLocation.x, mouseLocation.y].gridX + "," + nodeGrid[mouseLocation.x, mouseLocation.y].gridY);
            }
        }
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
                    Gizmos.DrawSphere(tileGrid.GetCellCenterWorld(new Vector3Int(y, x, 0)), 0.25f);
                }
            }
        }
    }
}
