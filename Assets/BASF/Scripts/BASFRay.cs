using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASFRay : MonoBehaviour
{

    public GameObject Curser;

    // Use this for initialization
    void Start()
    {

    }




    // Update is called once per frame
    void Update()
    {
        Curser.SetActive(false);

        Debug.DrawRay(transform.position, transform.up, Color.yellow);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            Curser.SetActive(true);
            Curser.transform.position = hit.transform.position;
        }
    }
}
