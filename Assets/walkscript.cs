using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicMotionsDummyModel : MonoBehaviour
{
    public float speed = 5f; // Geschwindigkeit der Figur

    private void Start()
    {
        // Starten der Laufanimation
        GetComponent<Animator>().SetBool("isWalking", true);
    }

    private void Update()
    {
        // Bewegung der Figur in Richtung des nächsten GameObjects mit "road" im Namen
        GameObject nextRoad = GetNextRoad();
        if (nextRoad != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextRoad.transform.position, speed * Time.deltaTime);
            transform.LookAt(nextRoad.transform.position);
        }
    }

    private GameObject GetNextRoad()
    {
        // Suchen des nächsten GameObjects mit "road" im Namen
        GameObject[] roads = GameObject.FindGameObjectsWithTag("road");
        GameObject closestRoad = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject road in roads)
        {
            float distance = Vector3.Distance(transform.position, road.transform.position);
            if (distance < closestDistance)
            {
                closestRoad = road;
                closestDistance = distance;
            }
        }

        return closestRoad;
    }
}

