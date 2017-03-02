using UnityEngine;
using System.Collections;
// Matt N 22/02
// ClockCam is attached to trigger box collider. Swaps between main and clock cameras in scene to allow view of clipboard UI. 
public class ClockCam : MonoBehaviour {

    // Assign cameras in inspector
    public Camera mainCam;
    public Camera clockCam;

    // Disable clock cam at start
	 void Awake()
    {
        clockCam.enabled = false;
    }

    // Whilst player inside trigger area
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Fork") // Might need to change this tag check
        {
            // Turn off main cam
            mainCam.enabled = false;
            // Enable clock cam
            clockCam.enabled = true;
        }     
    }

    // After player leaves trigger area
    void OnTriggerExit(Collider col)
    {
        // Disable clock cam
        clockCam.enabled = false;
        // Re-enable main cam
        mainCam.enabled = true;
    }
}
