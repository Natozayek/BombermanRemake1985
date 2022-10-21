using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node
{
    public int F; // F=G+H
    public int G;
    public int H;

    public Vector2 pos;
    public Vector2 targetPos;
    public Node prevNode;
    

    public Node(int g, Vector2 nodePosition, Vector2 targetPosition, Node previousNode)
    {
        pos = nodePosition;
        targetPos = targetPosition;
        prevNode = previousNode;
        G = g;
        H = (int)Mathf.Abs(targetPosition.x - pos.x) + (int)Mathf.Abs(targetPosition.y - pos.y);
        F = G + H;
    }

}
public class PathFinder : MonoBehaviour
{
    public List<Node> FreeNodes = new List<Node>();
    public List<Vector2> PathToTarget;
    public List<Node> CheckedNodes = new List<Node>();
    public List<Node> NeigbourghNodes = new List<Node>();
    public Node nodeToCheck;
    public List<LayerMask>brick = new List<LayerMask>();

   
    public List<Vector2> FindAPath(Vector2 target)
    {
        PathToTarget = new List<Vector2>();
        CheckedNodes = new List<Node>();
        NeigbourghNodes = new List<Node>();
        
        Vector2 start = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        Vector2 TargetPosition = new Vector2(Mathf.Round(target.x), Mathf.Round(target.y));

        if (start == TargetPosition) return PathToTarget;

        Node startNode = new Node(0, start, TargetPosition, null);
        CheckedNodes.Add(startNode);
        NeigbourghNodes.AddRange(GetNeighbourNodes(startNode));
        while (NeigbourghNodes.Count > 0)
        {
           Node nodeToCheck = NeigbourghNodes.Where(x => x.F == NeigbourghNodes.Min(y => y.F)).FirstOrDefault();

            if (nodeToCheck.pos == TargetPosition)
            {
                return CalculatePathFromNode(nodeToCheck);
            }

            var walkable = Physics2D.OverlapCircle(nodeToCheck.pos, 1.1f, brick.Count);
            
            if (!walkable)
            {
                
                NeigbourghNodes.Remove(nodeToCheck);
                CheckedNodes.Add(nodeToCheck);
            }
            else if (walkable)
            {
               
                NeigbourghNodes.Remove(nodeToCheck);
                if (!CheckedNodes.Where(x => x.pos == nodeToCheck.pos).Any())
                {
                    CheckedNodes.Add(nodeToCheck);
                    NeigbourghNodes.AddRange(GetNeighbourNodes(nodeToCheck));
                }
            }
        }
        FreeNodes = CheckedNodes;

        return PathToTarget;
    }

    public List<Vector2> CalculatePathFromNode(Node node)
    {
        var path = new List<Vector2>();
        Node currentNode = node;

        while (currentNode.prevNode != null)
        {
            path.Add(new Vector2(currentNode.pos.x, currentNode.pos.y));
            currentNode = currentNode.prevNode;
        }

        return path;
    }

    List<Node> GetNeighbourNodes(Node node)
    {
        var Neighbours = new List<Node>();

        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.pos.x - 1, node.pos.y),
            node.targetPos,
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.pos.x + 1, node.pos.y),
            node.targetPos,
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.pos.x, node.pos.y - 1),
            node.targetPos,
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.pos.x, node.pos.y + 1),
            node.targetPos,
            node));
        return Neighbours;
    }

    void OnDrawGizmos()
    {
        if (PathToTarget != null)
            foreach (var item in PathToTarget)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.25f);
            }
        foreach (var item in CheckedNodes)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector2(item.pos.x, item.pos.y), 0.25f);
        }

        foreach (var item in NeigbourghNodes)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(new Vector3(item.pos.x, item.pos.y, 0), new Vector3(1.0f, 1.0f, 0f));
        }
    }

}


