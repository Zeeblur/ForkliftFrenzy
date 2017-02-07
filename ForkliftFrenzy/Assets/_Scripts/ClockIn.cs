using UnityEngine;
using System.Collections;

public class ClockIn : MonoBehaviour {

    // Assign cameras in inspector
    public Camera mainCam;
    public Camera clockCam;
    
    void Awake()
    {
        clockCam.enabled = false;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Fork")
        {
            // Disable normal camera
            mainCam.enabled = false;
            // Enable clock in camera
            clockCam.enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fork")
        {
            // Disable clock cam
            clockCam.enabled = false;
            // Enable normal cam
            mainCam.enabled = true;
        }
    }
  
}
