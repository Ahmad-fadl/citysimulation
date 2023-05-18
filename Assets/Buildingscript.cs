using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildingscript : MonoBehaviour
{
    [SerializeField] public bool visited;
    // Start is called before the first frame update
    void Start()
    {
        visited=false;
    }

    // Update is called once per frame
    void Update()
    {
 /*   GameObject player=  GameObject.Find("BasicMotionsDummy");

     float distance =    Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2));
     if (distance<3f){
        visited=true;
     }*/
    }
}
