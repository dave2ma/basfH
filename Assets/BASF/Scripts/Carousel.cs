using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{

    UnityEngine.Object[] prefabs;


    // Use this for initialization
    void Start()
    {
        if (Camera.main == null) { Debug.Log("No camera found"); return; };
        gameObject.transform.position = Camera.main.transform.position;

        prefabs = Resources.LoadAll("Prefabs/Tiles", typeof(GameObject));
        if (prefabs == null || prefabs.Length == 0) { Debug.Log("Tiles could not be loaded"); };
        BuildElements();
        gameObject.transform.Translate(new Vector3(0, 2, 0));



    }



    private void BuildElements()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefabInstance = Instantiate((GameObject)prefabs[i]);
            prefabInstance.transform.parent = transform;


            Debug.Log("i: " + i + " prefabs.Length / 2: " + (int)prefabs.Length / 2 + " i == ((int)prefabs.Length / 2): " + (i == ((int)prefabs.Length / 2)));


            float angleOfRotation = 0f;
            if (i == 0) angleOfRotation = 180f;

            else if (i <= ((int)prefabs.Length / 2)) angleOfRotation = 180f - (i * 34f);
            else if (i > ((int)prefabs.Length / 2)) angleOfRotation = 180f + (i - (int)prefabs.Length / 2) * 34f;

            Debug.Log("i: " + i + "prefabs.Length / 2: " + prefabs.Length / 2 + "angleofRotation: " + angleOfRotation);

            prefabInstance.transform.RotateAround(Camera.main.transform.position, Vector3.up, angleOfRotation);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
