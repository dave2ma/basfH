using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASFRay : MonoBehaviour
{

    bool triggerIsOn;

    float triggerPressedTime;
    Quaternion currentRotation;
    Vector3 currentMouseX;
    public GameObject Curser;

    GameObject StartScreen;

    public GameObject _Carousel;
    private float rotateScale = 35f;
    private float pressTime = 0.20f;
    private float forwardMovementScale = 1.5f;
    private Transform oldHit;

    // Use this for initialization
    void Start()
    {
        triggerIsOn = false;


        //Load Prefabs for all AtomElements from Resources Folder
        GameObject StartScreenPrefab = Resources.Load<GameObject>("Prefabs/startscreen");
        if (StartScreenPrefab == null) { Debug.LogError("StartScreenPrefab could not be loaded"); return; }

        StartScreen = Instantiate(StartScreenPrefab);

        if (StartScreen == null) { Debug.Log("StartScreen could not be loaded"); return; }
        StartScreen.SetActive(false);

    }




    // Update is called once per frame
    void Update()
    {
        Curser.SetActive(false);

        // Check for a Wall.
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        bool rayCastHit = Physics.Raycast(transform.position, transform.forward, out hit, 5);
        if (rayCastHit)
        {
            Curser.SetActive(true);
            Curser.transform.position = hit.point;
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            triggerPressedTime += Time.deltaTime;
            if (!triggerIsOn)
            {
                triggerIsOn = true;
                currentRotation = transform.rotation;

            }
            if (triggerIsOn && oldHit == null)
            {
                Quaternion tmp = transform.rotation * Quaternion.Inverse(currentRotation);
                _Carousel.transform.RotateAround(Camera.main.transform.position, Vector3.up, tmp.y * rotateScale);

                if (_Carousel.transform.rotation.eulerAngles.y < 290f && _Carousel.transform.rotation.eulerAngles.y > 91f)
                {
                    _Carousel.transform.RotateAround(Camera.main.transform.position, Vector3.up, tmp.y * -rotateScale);
                }
                currentRotation = transform.rotation;
            }
        }
        else
        {
            if (triggerIsOn) triggerIsOn = false;

            if (triggerPressedTime != 0 && triggerPressedTime <= pressTime)
            {
                if (rayCastHit && hit.transform.tag == "appCard")
                {
                    if (!hit.transform.Equals(oldHit))
                    {
                        hit.transform.position += hit.transform.forward * forwardMovementScale;

                        //activate Startscreen
                        HandelStartScreen(true, hit.transform);

                        foreach (Transform t in _Carousel.transform)
                        {
                            if (!t.name.Equals(hit.transform.parent.name))
                            {
                                t.gameObject.SetActive(false);
                            }
                            else
                            {
                                t.gameObject.SetActive(true);
                            }
                        }
                        if (oldHit != null)
                        {

                            //deaktivate Startscreen
                            HandelStartScreen(false, oldHit.transform);


                            oldHit.position -= oldHit.forward * forwardMovementScale;
                            foreach (Transform t in _Carousel.transform)
                            {
                                t.gameObject.SetActive(true);
                            }
                        }
                        oldHit = hit.transform;
                    }
                }
                else if (oldHit != null)
                {
                    //deaktivate Startscreen
                    HandelStartScreen(false, oldHit.transform);
                    oldHit.position -= oldHit.forward * forwardMovementScale;
                    foreach (Transform t in _Carousel.transform)
                    {
                        t.gameObject.SetActive(true);
                    }
                    oldHit = null;
                }
            }
            triggerPressedTime = 0f;
        }



        //------------------------Mouse Test---------------
        //    RaycastHit hitMouse;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hitMouse))
        //    {
        //        if (Input.GetMouseButton(0))
        //        {

        //            triggerPressedTime += Time.deltaTime;

        //            if (!triggerIsOn)
        //            {
        //                triggerIsOn = true;
        //                currentRotation = transform.rotation;
        //                currentMouseX = Input.mousePosition;

        //            }
        //            if (triggerIsOn)
        //            {
        //                Vector3 tmp = currentMouseX - Input.mousePosition;

        //                _Carousel.transform.RotateAround(Camera.main.transform.position, Vector3.up, tmp.x);

        //                if (_Carousel.transform.rotation.eulerAngles.y < 290f && _Carousel.transform.rotation.eulerAngles.y > 91f)
        //                {
        //                    _Carousel.transform.RotateAround(Camera.main.transform.position, Vector3.up, tmp.x * -1);
        //                }

        //                currentRotation = transform.rotation;
        //                currentMouseX = Input.mousePosition;
        //            }
        //        }

        //        else
        //        {
        //            if (triggerIsOn) triggerIsOn = false;
        //            if (triggerPressedTime != 0 && triggerPressedTime <= pressTime)
        //            {
        //                if (hitMouse.transform.tag == "appCard")
        //                {
        //                    if(!hitMouse.Equals(oldHit))
        //                    {
        //                        hitMouse.transform.localPosition -= hitMouse.transform.forward*2;
        //                        foreach (Transform t in _Carousel.transform)
        //                        {
        //                            if (!t.name.Equals(hitMouse.transform.parent.name))
        //                            {
        //                                t.gameObject.SetActive(false);
        //                            }
        //                        }
        //                        if (oldHit != null)
        //                        {
        //                            oldHit.transform.localPosition += oldHit.transform.forward*2;
        //                            foreach (Transform t in _Carousel.transform)
        //                            {
        //                                    t.gameObject.SetActive(true);
        //                            }
        //                        }
        //                        oldHit = hitMouse.transform;
        //                    }


        //                } 
        //            }
        //            triggerPressedTime = 0f;
        //        }
        //    }
        //    else if(Input.GetMouseButton(0))
        //    {
        //        if (oldHit != null)
        //        {
        //            oldHit.transform.localPosition += oldHit.transform.forward*2;
        //            foreach (Transform t in _Carousel.transform)
        //            {
        //                t.gameObject.SetActive(true);
        //            }
        //            oldHit = null;
        //        }
        //    }
    }
    //--------------------------------------------------




    private void HandelStartScreen(bool activation, Transform destinationTransform)
    {
        StartScreen.SetActive(activation);
        StartScreen.transform.position = destinationTransform.position;
        StartScreen.transform.rotation = destinationTransform.rotation;
    }

}


