using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASFRay2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
          
        }
    }
}
