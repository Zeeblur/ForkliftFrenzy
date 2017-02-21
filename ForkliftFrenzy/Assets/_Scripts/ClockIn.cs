using UnityEngine;
using System.Collections;

// ClockIn should be attached to trigger area of clock in model. Deals with camera transition when entering this area.
public class ClockIn : MonoBehaviour {

    // Assign cameras in inspector
    public Camera mainCam;
    public Camera clockCam;

    public Transform camEndPos;
    public Transform clockViewTarget;
    public float speed = 5.0f;
    public float targetFOV = 30.0f;

    private Vector3 targetDir;
    private Vector3 newDir;
    private bool camSetup = false;

    // Disable clock cam at start
    void Awake()
    {
        // Disable clock cam on start
        clockCam.enabled = false;
    }

    // Whilst inside trigger area
    void OnTriggerStay(Collider col)
    {
        // Target player - might need to change later if tags differ 
        //(fork not the best tag for player, would prefer a main body tag or something, so it's not triggered by forks)
        if (col.gameObject.tag == "Fork")
        {
            // Flag in place, only want to do this once
            if (camSetup == false)
            {
                // Start clockCam setup routine
                StartCoroutine("SetupClockCam");
            }

            // Disable normal camera
            mainCam.enabled = false;

            // Enable clock in camera
            clockCam.enabled = true;

            // Gradually lower field of view, for zoom effect
            clockCam.fieldOfView = Mathf.Lerp(clockCam.fieldOfView, targetFOV, Time.deltaTime  / speed  );
            // Gradually move closer, to avoid staring at back of forklift
            clockCam.transform.position = Vector3.MoveTowards(clockCam.transform.position, camEndPos.position, Time.deltaTime / speed);
            // Gradually rotate to face towards clock in
            // NEEDS IMPROVING - CAMERA STUTTERS
            //targetDir = clockViewTarget.position - clockCam.transform.position;
            //newDir = Vector3.RotateTowards(clockCam.transform.forward, targetDir, Time.deltaTime / speed, 0.0f);
            //clockCam.transform.rotation = Quaternion.LookRotation(newDir);
        }
    }


    // This just sets clockCam to mirror mainCam properties
    // Have done this as coroutine so that it's not being called over again
    // in OnTriggerStay
    IEnumerator SetupClockCam()
    {
        // Set clock cam to same position & rotation as mainCam
        // This allows for smooth camera changes without visual glitch
        clockCam.transform.position = mainCam.transform.position;
        clockCam.transform.rotation = mainCam.transform.rotation;
        clockCam.transform.LookAt(clockViewTarget);

        camSetup = true;

        Debug.Log("clock cam setup");

        yield return null;
    }


    // Once left trigger area
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fork")
        {
            // Disable clock cam
            clockCam.enabled = false;
            // Enable normal cam
            mainCam.enabled = true;
            // Reset cam setup flag
            camSetup = false;
        }
    }
  
}
