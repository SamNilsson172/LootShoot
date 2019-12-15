using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour
{
    Gridy grid;

    void Awake()
    {
        grid = GetComponent<Gridy>();
    }


    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Stopwatch sw = new Stopwatch(); //check how long the method takes
        sw.Start();

        Vector3[] waypoints = new Vector3[0]; //contains the path
        bool pathSuccess = false; //if path could be found

        Node startNode = grid.NodeFromWorldPoint(request.pathStart); //get start and end in node
        Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize); //create heap for all nodes that can be moved to
        HashSet<Node> closedSet = new HashSet<Node>(); //hash set for all nodes that can't be moved to
        openSet.Add(startNode); //add the startnode for a starting point

        if (startNode.walkable && targetNode.walkable)
            while (openSet.Count > 0) //only breaks if target node is unreacheble
            {
                Node currentNode = openSet.RemoveFirst(); //set current node to the first one in openSet and remove it from the openset 
                closedSet.Add(currentNode); //so you can't move to current node again

                if (currentNode == targetNode) //if goal is reached
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode)) //if an open neighbour to the current node gets a lower gcost from using currents path (patrents), current is the neighbours parent
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) //if neighbour is not walkable or on the closed set skip it
                        continue;

                    int movenCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty; //the cost for moving to the neighbour
                    if (movenCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) //if the new cost is lower than the old or neighbour has not yet been added to openset
                    {
                        neighbour.gCost = movenCostToNeighbour; //set g cost
                        neighbour.hCost = GetDistance(neighbour, targetNode); //set h cost
                        neighbour.parent = currentNode; //set parent to backtrack to start from goal

                        if (!openSet.Contains(neighbour)) //if node was not in openset, add it
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour); //else, just update heap
                    }
                }
            }
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode); //if path was found, save it in the array
            pathSuccess = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, pathSuccess, request.callback)); //tell request manager that the path is found
        //UnityEngine.Debug.Log(GC.GetTotalMemory(true) + " Bytes");
    }
    Vector3[] RetracePath(Node firstNode, Node endNode) //gets the path by backtracking with the nodes parents 
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != firstNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] waypoints = SimplyfyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplyfyPath(List<Node> path) //only use nodes that change the direction in the path
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;
        if (path.Count > 0) waypoints.Add(path[0].worldPosition);

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY); //only use noes that change dir
            if (newDir != dirOld)
                waypoints.Add(path[i - 1].worldPosition);
            float hight = path[i].worldPosition.y - path[i - 1].worldPosition.y;
            if (hight < -1f && hight > -20) //before reverse, so check for falls bigger than 1 or smaller than 20
            {
                waypoints.Add(path[i - 1].worldPosition); //jump tp
                int extraNodes = Mathf.RoundToInt((hight * -1) / 5);
                waypoints.Add(path[i + extraNodes].worldPosition); //jump from
                i += extraNodes;
            }
            dirOld = newDir;
        }
        return waypoints.ToArray();
    }
    public int heightPenalty = 30;
    int GetDistance(Node nodeA, Node nodeB)
    {
        if (nodeA != null && nodeB != null)
        {

            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstZ)
            {
                return heightPenalty * dstY + 14 * dstZ + 10 * (dstX - dstZ);
            }
            return heightPenalty * dstY + 14 * dstX + 10 * (dstZ - dstX);
        }
        else
        {
            print("error, node not found");
            return 0;
        }
    }
    /*
    int getDistance(Node nodeA, Node nodeB) //gets the distance between two nodes by checking how many linear and diagonal steps are required
    {
        int x = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int y = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (x > y)
            return (x - y) * 10 + y * 14;
        return (y - x) * 10 + x * 14;
    }*/
}
