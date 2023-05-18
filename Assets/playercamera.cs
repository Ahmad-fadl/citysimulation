using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playercamera : MonoBehaviour
{
    public Transform target; // The character to follow
    public float smoothSpeed = 0.125f; // The speed at which the camera follows the character
    public Vector3 offset; // The offset of the camera from the character

    private void LateUpdate()
    {
        GameObject target=GameObject.FindGameObjectsWithTag("Player")[0];
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.transform.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the position of the camera to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the character
        transform.LookAt(target.transform);
        
    }
}
