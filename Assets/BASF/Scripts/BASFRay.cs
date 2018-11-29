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

        // Check for a Wall.
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {

            Curser.SetActive(true);
            Curser.transform.position = hit.point;
        }
    }
}
