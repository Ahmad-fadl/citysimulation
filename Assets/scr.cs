using System.ComponentModel;
using System.Security.Cryptography;
using System.ComponentModel.Design.Serialization;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Node
{
    public GameObject gameObject;
    public List<Node> edges;
    public bool visited;

    public Node(GameObject obj)
    {
        visited=false;
        gameObject = obj;
        edges = new List<Node>();
    }

    public void AddEdge(Node node)
    {
        edges.Add(node);
    }
}


public class scr : MonoBehaviour
{
    public List<Node> buildings;
    
    public bool walking;
    

    public float speed = 1f; // Geschwindigkeit der Figur
    public string[] tagsToInclude = {"road", "Buildings"}; // Tags, die in den Graphen aufgenommen werden sollen
    private Node currenttarget;
    private List<GameObject> gameObjects; // Liste aller GameObjects auf der Szene mit den angegebenen Tags
    private List<Node> nodes; // Liste von Nodes, die den GameObjects entsprechen

    private Node buildingtarget;
    private void Awake()
    {
        gameObjects = new List<GameObject>();
        nodes = new List<Node>();
        buildings= new List<Node>();
    }

private void Start()
{
    walking=false;
    create_graph();
    SaveGraph();
    currenttarget=null;

    //buildings=FindGameObjectsWithTag("Buildins");
    
    foreach (Node a in nodes)
    {


        if (a.gameObject.tag=="Buildings")

            {
                buildings.Add(a);

                if(a.gameObject.name=="big cottage 1 floor new with environment")
                    
                    {
                        buildingtarget=a;
                    }
            }
    }



    // Starten der Laufanimation
    //GetComponent<Animator>().SetBool("isWalking", true);
}

private void Update()
{
    
    Node nearestnode=GetNearestNode();



    currenttarget=bewegezu(nearestnode,buildingtarget);


    transform.position = Vector3.MoveTowards(transform.position, currenttarget.gameObject.transform.position , speed * Time.deltaTime);
    //float distance = Vector3.Distance(transform.position, currenttarget.gameObject.transform.position);



   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="land"){}

    }




void create_graph(){
            // Finde alle GameObjects auf der Szene, die die angegebenen Tags haben
        foreach (string tag in tagsToInclude)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsWithTag)
            {
                gameObjects.Add(obj);
            }
        }

        // Erstelle Nodes für alle gefundenen GameObjects
        foreach (GameObject obj in gameObjects)
        {
            Node node = new Node(obj);
            nodes.Add(node);
        }

        // Erstelle Kanten zwischen den Nodes
        foreach (Node node1 in nodes)
        {
            foreach (Node node2 in nodes)
            {
                // Überspringe Nodes, die sich selbst sind oder sich auf einem Objekt mit dem Tag "Buildings" befinden
                if (node1 == node2 || node2.gameObject.CompareTag("Buildings"))
                    continue;

                // Berechne die Distanz zwischen den Nodes
                float distance = Vector3.Distance(node1.gameObject.transform.position, node2.gameObject.transform.position);

                // Füge eine Kante hinzu, wenn die Nodes in der Nähe sind
                if (distance < 10.0f)
                {
                    node1.AddEdge(node2);
                }

                if ((node1.gameObject.tag=="Buildings" && distance< 19.0f)||(node2.gameObject.tag=="Buildings" && distance< 19.0f)){
                    node1.AddEdge(node2);
                    node2.AddEdge(node1);
                }
            }
        }

        // Optional: Speichere den Graphen in eine Datei
        
}

private void SaveGraph()
{
    string filePath = Application.dataPath + "/Graph.txt"; // Path to save the file to

    using (StreamWriter writer = new StreamWriter(filePath))
    {
        // Write out each node and its edges to the file
        foreach (Node node in nodes)
        {
            writer.Write(node.gameObject.name + ":");
            foreach (Node edge in node.edges)
            {
                writer.Write(" " + edge.gameObject.name);
            }
            writer.WriteLine();
        }
    }
}



public Node GetNearestNode()
{
    Node nearestNode = null;
    float nearestDistance = Mathf.Infinity;

    foreach (Node node in nodes)
    {
        float distance = Vector3.Distance(node.gameObject.transform.position, transform.position);
        if (distance < nearestDistance)
        {
            nearestDistance = distance;
            nearestNode = node;
        }
    }

    return nearestNode;
}


public Node GetNextBuildingNode()
{
    Node currentNode = GetNearestNode();
    Node nextBuildingNode = null;
    float nearestDistance = Mathf.Infinity;

    foreach (Node node in currentNode.edges)
    {
        if (node.gameObject.CompareTag("Buildings"))
        {
            float distance = Vector3.Distance(node.gameObject.transform.position, transform.position);
            if (distance < nearestDistance && node.visited==false)
            {
                nearestDistance = distance;
                nextBuildingNode = node;
            }
        }
    }

    return nextBuildingNode != null ? nextBuildingNode : currentNode;
}




public Node bewegezu(Node nearestnode, Node buildingtarget)
{
    float smallestDistance = float.MaxValue;
    Node closestNeighbor = null;

    // Durchlaufen Sie alle Nachbarn des nearestnode
    foreach (Node neighbor in nearestnode.edges)
    {
        // Berechnen Sie die Distanz zwischen dem gameObject des buildingtarget und dem Nachbarn
        float distance = Vector3.Distance(neighbor.gameObject.transform.position, buildingtarget.gameObject.transform.position);

        // Überprüfen Sie, ob die Distanz kleiner als die bisher kleinste Distanz ist
        if (distance < smallestDistance && neighbor.gameObject.tag=="road")
        {
            smallestDistance = distance;
            closestNeighbor = neighbor;
        }
    }

    return closestNeighbor;
}


}



