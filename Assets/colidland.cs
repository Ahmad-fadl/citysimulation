using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colidland : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
            //Debug.Log("fuck from land");

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
            //    rb.velocity = Vector3.zero;
            }
        
    }
}
