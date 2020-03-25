﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class used for Pathfinding following the A* algorithm
public class Pathfinder : MonoBehaviour
{
    PathGrid grid;

    //Vector3 used to debug pathfinding
    //Vector3 debugStart;
    //Vector3 debugTarget;
    List<GameObject> debugObstructions = new List<GameObject>();
    public GameObject debugObstruction;

    //On Awake gets the Pathfinding Grid it will be using
    void Awake()
    {
        grid = GetComponent<PathGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        ////debug that sets the start location of a path
        //if (Input.GetMouseButtonDown(0))
        //{
        //    debugStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
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
            PathNode p = grid.WorldToNode(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (p != null)
            {
                p.isMoveObstructed = !p.isMoveObstructed;
                p.isSightObstructed = !p.isSightObstructed;
                if (p.isMoveObstructed)
                {
                    //if the tile is now obstructed add the debug object to show its obstruction
                    GameObject ob = Instantiate(debugObstruction, grid.NodeToWorld(p), new Quaternion());
                    debugObstructions.Add(ob);
                }
                else
                {
                    //otherwise remove the obstruction at that point
                    Vector3 loc = grid.NodeToWorld(p);
                    for (int i = 0; i < debugObstructions.Count; i++)
                    {
                        if (debugObstructions[i].transform.position == loc)
                        {
                           
                            Destroy(debugObstructions[i]);
                            debugObstructions.RemoveAt(i);
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
    //Finds the shortest path between two points, if one exists and puts the pathh into the grid
    public void FindPath(Vector3 startPos, Vector3 targetPos, int maxLength)
    {
        //convert the given positions into pathfinding nodes
        PathNode startNode = grid.WorldToNode(startPos);
        PathNode targetNode = grid.WorldToNode(targetPos);

        if (startNode == null)
        {
            Debug.Log("Starting Tile Out of Bounds");
            return;
        }
        else if (targetNode == null)
        {
            Debug.Log("Target Tile Out of Bounds");
            return;
        }
        else if (startNode == targetNode)
        {
            Debug.Log("Starting Tile and Target Tile are identical");
            return;
        }
        //checks if the target tile is farther than the max length of the path
        else if ((int)Mathf.Sqrt(Mathf.Pow(startNode.posX - targetNode.posX, 2f) + Mathf.Pow(startNode.posY - targetNode.posY, 2f)) > maxLength)
        {
            Debug.Log("Target Tile out of movement range");
            return;
        }

        //list of pathing nodes that have not been checked yet
        List<PathNode> OpenList = new List<PathNode>();
        //set of pathing nodes that have been checked
        HashSet<PathNode> ClosedList = new HashSet<PathNode>();

        //reset gCost of the starting node
        startNode.gCost = 0;

        //add the first node to the unchecked nodes
        OpenList.Add(startNode);

        //while there are unchecked nodes, keep checking
        while (OpenList.Count > 0)
        {
            //start by looking at the first node
            PathNode currentNode = OpenList[0];
            Debug.Log(currentNode.posX + " " + currentNode.posY);
            //compare to every other node
            for (int i = 0; i < OpenList.Count; i++)
            {
                //if the total cost of a node is lower, or the total cost is equal but node is closer to the target
                if (OpenList[i].FCost < currentNode.FCost || currentNode.FCost == OpenList[i].FCost && OpenList[i].hCost < currentNode.hCost)
                {
                    //that node becomes the new current node
                    currentNode = OpenList[i];
                }
            }
            //remove node from the unchecked nodes
            OpenList.Remove(currentNode);
            //add the node to the checked nodes
            ClosedList.Add(currentNode);

            //if the current node is the target
            if (currentNode == targetNode)
            {
                startNode.isMoveObstructed = false;
                targetNode.isMoveObstructed = true;
                //create a list to contain the final path
                List<PathNode> finalPath = new List<PathNode>();
                //go backwards from the current node until reaching the starting node
                while (currentNode != startNode)
                {
                    //add each node to the final path and look at the previous node
                    finalPath.Add(currentNode);
                    currentNode = currentNode.prevNode;
                }
                //add the starting node for debug purposes
                finalPath.Add(currentNode);

                //reverse the final path so that it goes from start to end, rather than end to start
                finalPath.Reverse();
                //send the final apth to the grid
                grid.finalPath = finalPath;

                return;
            }

            //look at each adjacent node
            foreach (PathNode adjacentNode in grid.GetAdjacentNodes(currentNode))
            {
                //if it has already been checked or is obstructed, skip it
                if (adjacentNode.isMoveObstructed || ClosedList.Contains(adjacentNode))
                {
                    continue;
                }
                else
                {
                    //if the adjacent node is more than 1 tile farther from the start than the current node
                    //or if the unchecked nodes list does not contain the adjacent node
                    if (!OpenList.Contains(adjacentNode) || currentNode.gCost + 1 < adjacentNode.gCost)
                    {
                        //the adjacent node's distance from the start node along this path is this node's distance + 1
                        adjacentNode.gCost = currentNode.gCost + 1;
                        Debug.Log("gPing: " + adjacentNode.gCost);
                        //calculate the adjacent node's distance from the target using manhatten distance
                        //adjacentNode.hCost = Mathf.Abs(adjacentNode.posX - targetNode.posX) + Mathf.Abs(adjacentNode.posY - targetNode.posY);
                        //calculate the adjacent node's distance from the target using pythagorean theorem rounding down
                        adjacentNode.hCost = (int)Mathf.Sqrt(Mathf.Pow(adjacentNode.posX - targetNode.posX, 2f) + Mathf.Pow(adjacentNode.posY - targetNode.posY, 2f));
                        //set the current node as the predecessor of the adjacent node
                        adjacentNode.prevNode = currentNode;
                        //if the unchecked nodes list does not contain the adjacent node and the adjacent node is not too far from the start, add it
                        if (!OpenList.Contains(adjacentNode) && adjacentNode.gCost <= maxLength)
                        {
                            OpenList.Add(adjacentNode);
                        }
                    }
                }
            }
        }
        return;
    }

    //Finds the shortest path between two points, if one exists and puts the pathh into the grid
    public List<PathNode> FindSightPath(Vector3 startPos, Vector3 targetPos, int maxLength)
    {
        //convert the given positions into pathfinding nodes
        PathNode startNode = grid.WorldToNode(startPos);
        PathNode targetNode = grid.WorldToNode(targetPos);

        if (startNode == null)
        {
            Debug.Log("Starting Tile Out of Bounds");
            return null;
        }
        else if (targetNode == null)
        {
            Debug.Log("Target Tile Out of Bounds");
            return null;
        }
        else if (startNode == targetNode)
        {
            Debug.Log("Starting Tile and Target Tile are identical");
            return null;
        }
        //checks if the target tile is farther than the max length of the path
        else if ((int)Mathf.Sqrt(Mathf.Pow(startNode.posX - targetNode.posX, 2f) + Mathf.Pow(startNode.posY - targetNode.posY, 2f)) > maxLength)
        {
            Debug.Log("Target Tile out of movement range");
            return null;
        }

        //list of pathing nodes that have not been checked yet
        List<PathNode> OpenList = new List<PathNode>();
        //set of pathing nodes that have been checked
        HashSet<PathNode> ClosedList = new HashSet<PathNode>();

        //reset gCost of the starting node
        startNode.gCost = 0;

        //add the first node to the unchecked nodes
        OpenList.Add(startNode);
   
        //while there are unchecked nodes, keep checking
        while (OpenList.Count > 0)
        {
            
            //start by looking at the first node
            PathNode currentNode = OpenList[0];
            //compare to every other node
            for (int i = 0; i < OpenList.Count; i++)
            {
                //if the total cost of a node is lower, or the total cost is equal but node is closer to the target
                if (OpenList[i].FCost < currentNode.FCost || currentNode.FCost == OpenList[i].FCost && OpenList[i].hCost < currentNode.hCost)
                {
                    //that node becomes the new current node
                    currentNode = OpenList[i];
                }
            }
            //remove node from the unchecked nodes
            OpenList.Remove(currentNode);
            //add the node to the checked nodes
            ClosedList.Add(currentNode);

            //if the current node is the target
            if (currentNode == targetNode)
            {
                //create a list to contain the final path
                List<PathNode> finalPath = new List<PathNode>();
                //go backwards from the current node until reaching the starting node
                while (currentNode != startNode)
                {
                    //add each node to the final path and look at the previous node
                    finalPath.Add(currentNode);
                    currentNode = currentNode.prevNode;
                }
                //add the starting node for debug purposes
                finalPath.Add(currentNode);

                //reverse the final path so that it goes from start to end, rather than end to start
                finalPath.Reverse();
                Debug.Log("FSight");
                return finalPath;
            }

            //look at each adjacent node
            foreach (PathNode adjacentNode in grid.GetAdjacentNodes(currentNode))
            {
                //if it has already been checked, skip it
                if (ClosedList.Contains(adjacentNode))
                {
                    continue;
                }
                else
                {
                    //if the adjacent node is more than 1 tile farther from the start than the current node
                    //or if the unchecked nodes list does not contain the adjacent node
                    if (!OpenList.Contains(adjacentNode) || currentNode.gCost + 1 < adjacentNode.gCost)
                    {
                        //the adjacent node's distance from the start node along this path is this node's distance + 1
                        adjacentNode.gCost = currentNode.gCost + 1;
                        //calculate the adjacent node's distance from the target using manhatten distance
                        //adjacentNode.hCost = Mathf.Abs(adjacentNode.posX - targetNode.posX) + Mathf.Abs(adjacentNode.posY - targetNode.posY);
                        //calculate the adjacent node's distance from the target using pythagorean theorem rounding down
                        adjacentNode.hCost = (int)Mathf.Sqrt(Mathf.Pow(adjacentNode.posX - targetNode.posX, 2f) + Mathf.Pow(adjacentNode.posY - targetNode.posY, 2f));
                        //set the current node as the predecessor of the adjacent node
                        adjacentNode.prevNode = currentNode;
                        //if the unchecked nodes list does not contain the adjacent node and the adjacent node is not too far from the start, add it
                        if (!OpenList.Contains(adjacentNode) && adjacentNode.gCost <= maxLength /*&& adjacentNode.hCost < currentNode.hCost*/)
                        {
                            //if not sight obstructed add the node to the open list
                            if (!adjacentNode.isSightObstructed)
                            {
                                OpenList.Add(adjacentNode);
                            }
                            //otherwise return because sight is blocked
                            else
                            {
                                Debug.Log("Line of Sight blocked");
                                return null;
                            }
                        }
                    }
                }
            }
        }
        return null;
    }
}
